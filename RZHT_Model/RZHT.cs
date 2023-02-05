using System.Xml;
using System.Linq;
using System.Globalization;
using RastrLibrary;
using System.IO;
using ASTRALib;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data;
using RZHT_Model.Forms;
using System.Runtime.CompilerServices;
using System.Collections;
using static RZHT_Model.EventForProtocolMessage;
using System.Xml.Linq;

//TODO: Реализовать возможность редактирования ГОУ
//TODO: Добавить графический интерфейс
//TODO: Провести тестирование

//TODO: Реализовать проверку УР на схеме
//TODO: Перенести модель на паттерн MVP

//TODO: Рефакторинг кода?
//TODO: Добавить пояснительные комментарии
//TODO: 

namespace RZHT_Model
{
    /// <summary>
    /// Класс, представляющий из себя модель данных РЖТ с функционалом оптимизации и осуществления тестового расчета УР на схеме RastrWin3
    /// </summary>
    public class RZHT
    {
        /// <summary>
        /// РЖТ сгруппированная по принадлежности того или иного ранка к ГОУ
        /// </summary>
        private Dictionary<int, Dictionary<string, RankList>> _sortedTable;
        /// <summary>
        /// РЖТ без группировки в исходном виде
        /// </summary>
        private Dictionary<int, RankList> _unsortedList;

        /// <summary>
        /// Часть РЖТ прошедшая редактирование
        /// </summary>
        private RankList _editedRankList;

        /// <summary>
        /// Свойство чтения и записи сгруппированной РЖТ
        /// </summary>
        public Dictionary<int, Dictionary<string, RankList>> SortedTable
        {
            get => _sortedTable;
            set => _sortedTable = value;
        }
        /// <summary>
        /// Свойство чтения и записи РЖТ без группировки
        /// </summary>
        public Dictionary<int, RankList> UnsortedTable
        {
            get => _unsortedList;
            set => _unsortedList = value;
        }

        /// <summary>
        /// Конструктор класса РЖТ
        /// </summary>
        /// <param name="table">Сгруппированная РЖТ</param>
        /// <param name="unsortedTable">РЖТ без группировки</param>
        private RZHT (Dictionary<int, Dictionary<string, RankList>> table, Dictionary<int, RankList> unsortedTable)
        {
            SortedTable = table;
            UnsortedTable = unsortedTable;
        }

        /// <summary>
        /// Обработчик сообытия сообщений в протокол
        /// </summary>
        public static event EventHandler<EventProtocolMessage> Message;

        /// <summary>
        /// Метод чтения РЖТ из файла XML
        /// </summary>
        /// <param name="xmlDocument">Файл XML считанный в формат XmlDocument</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static RZHT ReadXML(XmlDocument xmlDocument)
        {
            if (xmlDocument.ChildNodes.Count == 0)
            {
                throw new ArgumentException("File is not loaded or empty");
            }

            var rzhtBase = new Dictionary<int, Dictionary<string, RankList>>();
            var rawBase = new Dictionary<int, RankList>();

            XmlElement? xRoot = xmlDocument.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    if (xnode.Name == "ranks")
                    {
                        string hour = null;
                        foreach (XmlNode ranksChildren in xnode.ChildNodes)
                        {
                            var rzhtGOU = new Dictionary<string, RankList>();
                            var rawRankList = new RankList();
                            

                            if(ranksChildren.Name == "hour")
                            {
                                rzhtBase.Add(int.Parse(ranksChildren.InnerText), null);
                                rawBase.Add(int.Parse(ranksChildren.InnerText), null);
                                hour = ranksChildren.InnerText;
                            }
                            if(ranksChildren.Name == "RDUS")
                            {
                                foreach(XmlNode rdusChildren in ranksChildren.ChildNodes)
                                {
                                    var rzhtRankList = new RankList();
                                    foreach (XmlNode rduChildren in rdusChildren.ChildNodes)
                                    {
                                        var rzhtRank = new Dictionary<string, string>();
                                        if (rduChildren.Name == "RANK")
                                        {
                                            foreach (XmlNode rankChild in rduChildren.ChildNodes)
                                            {
                                                rzhtRank.Add(rankChild.Name, rankChild.InnerText);
                                            }
                                            var rank = WriteToRank(rzhtRank);
                                            rzhtRankList.Add(rank);
                                            rawRankList.Add(rank);
                                        }
                                    }
                                    var gouNames = rzhtRankList.Select(x => x.Gou).Distinct().ToList<string>();
                                    foreach (string GOU in gouNames)
                                    {
                                        var GOURankList = new RankList();
                                        rzhtGOU.Add(GOU, null);
                                        foreach (var entry in rzhtRankList)
                                        {
                                            if(GOU == entry.Gou)
                                            {
                                                GOURankList.Add(entry);
                                            }
                                        }
                                        rzhtGOU[GOU] = GOURankList;
                                    }
                                }
                            }
                            if (rzhtGOU.Count > 0)
                            {
                                rzhtBase[int.Parse(hour)] = rzhtGOU;
                                rawBase[int.Parse(hour)] = rawRankList;
                            }
                        }
                    }
                }
            }
            var rzht = new RZHT(rzhtBase, rawBase);
            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Загружен файл РЖТ"));
            return rzht;
        }

        /// <summary>
        /// Метод оптимизации на несколько часов вперед, комбинированный с проверкой на схеме RastrWin3
        /// </summary>
        /// <param name="startTime">Время начала действия команды</param>
        /// <param name="finishTime">Время конца действия команды</param>
        /// <param name="load">Значение мощности для загрузки/разгрузки</param>
        /// <param name="up">Показывает направление изменения нагрузки 
        /// true - загрузка
        /// false - разгрузка</param>
        /// <param name="gouSpeed">Значения маневренности ГОУ</param>
        /// <param name="regimes">Файлы со схемами RastrWin3 с возможностью расчета в них МДП</param>
        /// <param name="mappingDict">Словарь для составления соответствия между генераторами на схеме RastrWin3 и РЖТ</param>
        /// <param name="checkScheme">Параметр с помощью которого можно выбрать проверять на схеме оптимизационный расчет или нет</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<int, Dictionary<string, RankList>> Optimisation(TimeOnly startTime, TimeOnly finishTime,
                                                                                    decimal load, bool up, GouSpeed gouSpeed,
                                                                                    LoadedRegimeFiles regimes, RZHTtoSCHEME mappingDict, 
                                                                                    bool checkScheme, bool checkSpeed, int some)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            var minutesFirstHour = startTime.Minute;
            var minutesLastHour = finishTime.Minute;
            RankList? editedSlice = null;

            var hoursInBetween = finishTime.Hour - startTime.Hour;
            
            var optimizedTable = new Dictionary<int, Dictionary<string, RankList>>();
            for (int i = 0; i < hoursInBetween + 1; i++)
            {
                var optimizationHour = startTime.Hour + i;
                var rankListSlice = this.UnsortedTable[optimizationHour];

                // Установка ограничений маневренности
                if (checkSpeed)
                {
                    if (optimizationHour == startTime.Hour)
                    {
                        rankListSlice = SetSpeedBarier(gouSpeed, rankListSlice, startTime.Minute);
                    }
                    else if (optimizationHour == finishTime.Hour)
                    {
                        rankListSlice = SetSpeedBarier(gouSpeed, rankListSlice, finishTime.Minute);
                    }
                    else
                    {
                        rankListSlice = SetSpeedBarier(gouSpeed, rankListSlice, 0);
                    }
                }
                
                // Проверка на перерасчет редактированной части РЖТ
                if(editedSlice != null)
                {
                    rankListSlice = editedSlice;
                }

                Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Начат оптимизационный расчет на распределение" +
                        $" {load} МВт {(up == true ? "вверх" : "вниз")} на промежуток с {startTime.Hour}:{startTime.Minute}" +
                        $" по {finishTime.Hour}:{finishTime.Minute} {(checkSpeed == true ? "с":"без")} учетом скорости и {(checkScheme == true ? "с" : "без")} проверки на схеме"));
                
                //Возвращает значение резерва RankList на загрузку - 0, или на разгрузку - 1
                decimal reserve = (up == true? rankListSlice.GouReserve[0]: rankListSlice.GouReserve[1]);
                if (reserve < load)
                {
                    Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Error, $"    Недостаточно резервов в {optimizationHour} часу. Пользователь задал значение {load} МВт, общие резервы составляют {reserve} МВт"));
                    break;
                }

                var optimizationForOneHour = OneHourOptimization(rankListSlice, optimizationHour, load, up, checkSpeed);

                if (checkScheme == true)
                {
                    if (regimes.FileHourDict == null)
                    {
                        Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Warning, $"    Не обнаружены файлы режимов"));

                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Multiselect = true;
                        openFileDialog.Title = "Загрузите режимы для проврерки на схеме";
                        regimes = LoadRegimes(openFileDialog);
                    }

                    Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Проверка расчета на схеме на {optimizationHour} час"));
                    bool schemeCheck = ModyfyWithOptimisationSolution(mappingDict, optimizationForOneHour, regimes.FileHourDict[optimizationHour], up, some);
                    if (schemeCheck == true)
                    {
                        Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Проверка расчета на схеме завершена успешно"));
                        optimizedTable.Add(optimizationHour, optimizationForOneHour);
                        editedSlice = null;
                    }
                    else
                    {
                        Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Error, $"        Проверка расчета на схеме неуспешна, предлагается модификация РЖТ"));
                        ModifyRZHT(rankListSlice, up);
                        some = 100;

                        if (_editedRankList != null)
                        {
                            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"        Оптимизационный расчет будет начат заново с модифицированной РЖТ"));
                            editedSlice = _editedRankList;
                            i--;
                        }
                        else
                        {
                            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Error, $"        Редактирование РЖТ неуспешно"));
                            throw new ArgumentException("Редактирование РЖТ неуспешно");
                        }
                        Cursor.Current = Cursors.WaitCursor;
                        continue;
                    }

                }
                else if (checkScheme == false)
                {
                    Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"    Оптимизация на {optimizationHour} внесена в результаты расчета"));
                    optimizedTable.Add(optimizationHour, optimizationForOneHour);
                    editedSlice = null;
                }
                
            }

            Cursor.Current = Cursors.Default;
            return optimizedTable;
        }

        /// <summary>
        /// Метод оптимизации на один час
        /// </summary>
        /// <param name="hour">час проведения оптимизации</param>
        /// <param name="load">Значение мощности для загрузки/разгрузки</param>
        /// <param name="up">Показывает направление изменения нагрузки 
        /// true - загрузка
        /// false - разгрузка</param>
        /// <param name="checkSpeed">Параметр показывающий учитывается ли маневренность ГОУ
        /// true - загрузка
        /// false - разгрузка</param>
        /// <returns></returns>
        public Dictionary<string, RankList> OneHourOptimization(RankList table, int hour, decimal load, bool up, bool checkSpeed)
        {
            decimal distributedLoad = 0;
            var sortedTable = new Dictionary<string, decimal>();
            var unsortedTable = new RankList();
            var loadedRanks = new Dictionary<string, RankList>();
            var result = new Dictionary<string, RankList>();
            if (up == true)
            {
                unsortedTable = table.SortMoreLoad;
            }
            else
            {
                unsortedTable = table.SortLessLoad;
            }

            foreach (var rank in unsortedTable)
            {
                // Загрузка и Разгрузка без учета скорости
                if (checkSpeed == false)
                {
                    if (up == true)
                    {
                        if (distributedLoad < load)
                        {
                            if (load > rank.ReserveUP)
                            {
                                rank.LoadVolume = rank.ReserveUP;
                                load = load - rank.ReserveUP;
                                rank.ReserveUP = 0;
                            }
                            if (load < rank.ReserveUP | load == rank.ReserveUP)
                            {
                                rank.LoadVolume = load;
                                rank.ReserveUP = rank.ReserveUP - load;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (distributedLoad < load)
                        {
                            if (load > rank.ReserveDOWN)
                            {
                                rank.LoadVolume = load;
                                load = load + rank.ReserveDOWN;
                            }
                            if (load < rank.ReserveDOWN | load == rank.ReserveDOWN)
                            {
                                rank.LoadVolume = load;
                                break;
                            }
                        }
                    }
                }

                // Загрузка и Разгрузка с учетом скорости
                if (checkSpeed == true)
                {
                    if (up == true)
                    {
                        if (distributedLoad < load)
                        {
                            if (load > rank.SpeedBarierUP)
                            {
                                rank.LoadVolume = rank.SpeedBarierUP;
                                load = load - rank.SpeedBarierUP;
                                rank.SpeedBarierUP = 0;
                            }
                            if (load < rank.SpeedBarierUP | load == rank.SpeedBarierUP)
                            {
                                rank.LoadVolume = load;
                                rank.SpeedBarierUP = rank.SpeedBarierUP - load;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (distributedLoad < load)
                        {
                            if (load > rank.SpeedBarierDOWN)
                            {
                                rank.LoadVolume = rank.SpeedBarierDOWN;
                                load = load - rank.SpeedBarierDOWN;
                                rank.SpeedBarierDOWN = 0;
                            }
                            if (load < rank.SpeedBarierDOWN | load == rank.SpeedBarierDOWN)
                            {
                                rank.LoadVolume = load;
                                rank.SpeedBarierDOWN = rank.SpeedBarierDOWN - load;
                                break;
                            }
                        }
                    }
                }
                
            }
            var loaded = unsortedTable.LoadedRanks;

            var gouNames = loaded.Select(x => x.Gou).Distinct().ToList<string>();
            foreach (string GOU in gouNames)
            {
                var GOURankList = new RankList();
                loadedRanks.Add(GOU, null);
                foreach (var entry in loaded)
                {
                    if (GOU == entry.Gou)
                    {
                        GOURankList.Add(entry);
                        Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"    ГОУ {entry.Gou} {(up == true ? "загружен" : "разгружен")} на {entry.LoadVolume} МВт"));
                    }
                }
                loadedRanks[GOU] = GOURankList;
            }

            //loadedRanks = (Dictionary<string, List<Rank>>)unsortedTable.LoadedRanks.GroupBy(x => x.Gou);
            //foreach (var entry in loadedRanks)
            //{
            //    result[entry.Key] = ConvertToRankList(entry.Value);
            //}
            return loadedRanks;
        }

        /// <summary>
        /// Метод заполняющий резервы на загрузку/разгрузку ранков с учетом маневренности ГОУ
        /// </summary>
        /// <param name="gouSpeed">Содержит маневренность тех или иных ГОУ на набор или сброс нагрузки (МВт/мин)</param>
        /// <param name="table">Часть на расчетный час</param>
        /// <param name="minute">Стартовая минута расчета</param>
        /// <returns></returns>
        private RankList SetSpeedBarier(GouSpeed gouSpeed, RankList table, int minute)
        {
            var gouRankcount = table.GouRankCount;
            var speedBarierDict = new Dictionary<string, decimal[]>();

            foreach(var element in gouRankcount)
            {
                speedBarierDict.Add(element.Key, new decimal[4] { decimal.Parse(element.Value.GetRow(0).Sum().ToString()), decimal.Parse(element.Value.GetRow(1).Sum().ToString()), 0, 0});
            }
            for (int i = minute; i < 60; i++)
            {
                foreach(var rank in table)
                {
                    if (speedBarierDict.ContainsKey(rank.Gou) && gouSpeed.SpeedDict.ContainsKey(rank.Gou))
                    {
                        // Скорость набора вверх
                        if (speedBarierDict[rank.Gou][0] != 0)
                        {
                            speedBarierDict[rank.Gou][2] += gouSpeed.SpeedDict[rank.Gou][0] / speedBarierDict[rank.Gou][0];
                        }
                        // Скорость набора вниз
                        if (speedBarierDict[rank.Gou][1] != 0)
                        {
                            speedBarierDict[rank.Gou][3] += gouSpeed.SpeedDict[rank.Gou][1] / speedBarierDict[rank.Gou][1];
                        }
                    }
                }
            }

            // Удостовериться, что скорость изменения нагрузки не превышает максимум ранка
            foreach(var rank in table)
            {
                bool? hitLimit = null;
                if (rank.ReserveUP > speedBarierDict[rank.Gou][2])
                {
                    rank.SpeedBarierUP = speedBarierDict[rank.Gou][2];
                    hitLimit = true;
                }
                else
                {
                    rank.SpeedBarierUP = rank.ReserveUP;
                }
                if (rank.ReserveDOWN > speedBarierDict[rank.Gou][3])
                {
                    rank.SpeedBarierDOWN = speedBarierDict[rank.Gou][3];
                    hitLimit = true;
                }
                else
                {
                    rank.SpeedBarierDOWN = rank.ReserveDOWN;
                }

                if (speedBarierDict[rank.Gou][0] > 1 && hitLimit != null)
                {
                    if (hitLimit == true)
                    {
                        speedBarierDict[rank.Gou][2] -= rank.ReserveUP;
                    }
                }
                if (speedBarierDict[rank.Gou][1] > 1 && hitLimit != null)
                {
                    if (hitLimit == true)
                    {
                        speedBarierDict[rank.Gou][3] -= rank.ReserveDOWN;
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// Сортировка отсортированной РЖТ для набора нагрузки
        /// </summary>
        /// <param name="gouTable">РЖТ</param>
        /// <returns></returns>
        private Dictionary<string, decimal> SortByPriceMoreLoad(Dictionary<string, RankList> gouTable)
        {
            var unsortedGou = new Dictionary<string, decimal>();
            foreach(string gou in gouTable.Keys)
            {
                var cost = RankList.MoreLoadLowestCost(gouTable[gou]);
                unsortedGou.Add(gou, cost);
            }
            var ordered = unsortedGou.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }

        /// <summary>
        /// Сортировка отсортированной РЖТ для сброса нагрузки
        /// </summary>
        /// <param name="gouTable"РЖТ></param>
        /// <returns></returns>
        private Dictionary<string, decimal> SortByPriceLessLoad(Dictionary<string, RankList> gouTable)
        {
            var unsortedGou = new Dictionary<string, decimal>();
            foreach (string gou in gouTable.Keys)
            {
                var cost = RankList.LessLoadMaxCost(gouTable[gou]);
                unsortedGou.Add(gou, cost);
            }
            var ordered = unsortedGou.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }
        /// <summary>
        /// Метод переноса информации о ранке из XML в класс Rank
        /// </summary>
        /// <param name="rzhtRank">Словарь, содержащий параметр и значение параметра</param>
        /// <returns></returns>
        private static Rank WriteToRank(Dictionary<string, string> rzhtRank)
        {
            var rank = new Rank();
            foreach(var key in rzhtRank.Keys)
            {
                switch(key)
                {
                    case "gou":
                        rank.Gou = rzhtRank[key];
                        break;
                    case "gou_id":
                        rank.Id = int.Parse(rzhtRank[key]);
                        break;
                    case "parent_gou_id":
                        rank.ParentID = int.Parse(rzhtRank[key]);
                        break;
                    case "parent_gou_name":
                        rank.ParentGou = rzhtRank[key];
                        break;
                    case "control_volume":
                        rank.ControlVolume = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "price":
                        rank.Price = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "br_price":
                        rank.PriceBR = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "volume":
                        rank.Volume = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "prev_volume":
                        rank.PrevVolume = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "pmax":
                        rank.Pmax = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "pmin":
                        rank.Pmin = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "pbr":
                        rank.Pbr = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "isbid":
                        rank.IsBid = int.Parse(rzhtRank[key]);
                        break;
                    case "intval":
                        rank.RankGou = int.Parse(rzhtRank[key]);
                        break;
                    case "reserve_up":
                        rank.ReserveUP = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "reserve_down":
                        rank.ReserveDOWN = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "reserve_rampup":
                        rank.ReserveRampUP = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "reserve_rampdown":
                        rank.ReserveRampDOWN = decimal.Parse(rzhtRank[key], CultureInfo.InvariantCulture);
                        break;
                    case "type":
                        rank.Type = int.Parse(rzhtRank[key]);
                        break;
                }  
            }
            return rank;
        }

        /// <summary>
        /// Метод позволяющий записать объект List<Rank> в Ranklist
        /// </summary>
        /// <param name="listOfRanks">Список ранков в объекте List<Rank></param>
        /// <returns></returns>
        private static RankList ConvertToRankList(List<Rank> listOfRanks)
        {
            RankList rankList = new RankList();
            rankList.Ranks = listOfRanks;
            return rankList;
        }

        /// <summary>
        /// Метод, позволяющий разделить РЖТ на "РЖТ для загрузки" и "РЖТ для разгрузки"
        /// </summary>
        /// <param name="table">РЖТ</param>
        /// <returns></returns>
        public static Dictionary<int, Dictionary<string, RankList>>[] DivideUPandDOWN(Dictionary<int, Dictionary<string, RankList>> table)
        {

            var result = new Dictionary<int, Dictionary<string, RankList>>[2];
            var UP = new Dictionary<int, Dictionary<string, RankList>>();
            var DOWN = new Dictionary<int, Dictionary<string, RankList>>();

            foreach (var hourGouPair in table)
            {
                var entryUP = new Dictionary<string, RankList>();
                var entryDown = new Dictionary<string, RankList>();
                foreach(var gouTable in table[hourGouPair.Key])
                {
                    var ranksUP = new RankList();
                    var ranksDOWN = new RankList();
                    foreach(var rank in gouTable.Value)
                    {
                        if (rank.ReserveUP > 0)
                        {
                            ranksUP.Add(rank);
                        }
                        if (rank.ReserveDOWN > 0)
                        {
                            ranksDOWN.Add(rank);
                        }
                    }
                    if (ranksUP.Count > 0)
                    {
                        entryUP.Add(gouTable.Key, ranksUP);
                    }
                    if (ranksDOWN.Count > 0)
                    {
                        entryDown.Add(gouTable.Key, ranksDOWN);
                    }
                }
                UP.Add(hourGouPair.Key, entryUP);
                DOWN.Add(hourGouPair.Key, entryDown);
            }
            result[0] = UP;
            result[1] = DOWN;
            return result;

        }

        // Работа с RastrWin3
        /// <summary>
        /// Метод позволяющий вносить результаты оптимизационного расчета на расчетную схему
        /// </summary>
        /// <param name="mappingDict">Словарь отображения РЖТ на схему</param>
        /// <param name="solution">Результаты оптимизационного расчета</param>
        /// <param name="pathRegime">Путь до файла режима, соответствующего часу расчета</param>
        /// <param name="up">Показывает направление изменения нагрузки 
        /// true - загрузка
        /// false - разгрузка</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private bool ModyfyWithOptimisationSolution(RZHTtoSCHEME mappingDict, Dictionary<string, RankList> solution, string pathRegime, bool up, int some)
        {
            ASTRALib.Rastr rastrFromASTRA = new ASTRALib.Rastr();
            var rWin = new RWin();
            var table = TableRastr.Node;
            var variable = ParameterRastr.ActiveGen;
            //TODO: написать модуль выбора файла по времени расчета | Готово
            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"        Проверка расчета на схеме {pathRegime.Split(@"\").Last()}"));
            rWin.Load(pathRegime);
            int index = 0;
            if (up == true)
            {
                index = 0;
            }
            if (up == false)
            {
                index = 1;
            }

            foreach(var ranklist in solution.Values)
            {
                var rankCount = ranklist.GouRankCount;
                foreach(var rank in ranklist)
                {
                    if (mappingDict.MappingDict.Keys.Any(key => key.SequenceEqual(new[] { rank.Gou, rank.RankGou.ToString() })))
                    {
                        var mappingArray = mappingDict.MappingDict.FirstOrDefault(x => x.Key.SequenceEqual((new[] { rank.Gou, rank.RankGou.ToString() }))).Value;
                        foreach (var node in mappingArray)
                        {
                            var updatedValue = rWin.GetValue(table, variable, node);
                            var change = (double)(rank.LoadVolume / ((rankCount[rank.Gou][index, (rank.RankGou - 1)]) * mappingArray.Length));

                            int baseNodeNum = 1704;
                            var baseNodeValue = rWin.GetValue(table, variable, baseNodeNum);

                            //rWin.SetValue(table, variable, baseNodeNum, baseNodeValue - change);
                            double finishValue = updatedValue + change;
                            rWin.SetValue(table, variable, node, finishValue);
                            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info,
                                $"        Модификация схемы: Изменение генерации в {node} узле с {Math.Round(updatedValue, 1)} на {Math.Round(updatedValue + change, 1)}"));

                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }
            bool succsess = rWin.Regime();

            if (succsess)
            {
                Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"        Расчет УР прошел успешно. Начата проверка МДП"));
                return MDPCalculation(rWin, some);
            }
            else
            {
                Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"        Расчет УР неуспешен. Проверьте исходные данные"));

                throw new ApplicationException("Расчет УР в RastrWin3 не получился. Проверьте исходные данные.");
            }
        }

        /// <summary>
        /// Метод выполняющий расчет МДП внутренней библиотекой RastrWin3 - astra.dll. Доступ к библиотеке проходит через AstraLib.dll
        /// </summary>
        /// <param name="rWin">Прослойка для удобной работы с растром</param>
        /// <returns></returns>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="ApplicationException"></exception>
        public bool MDPCalculation(RWin rWin, int some)
        {
            object pVal = null;
            rWin._rastr.Emergencies(ref pVal);
            if (pVal != null)
            {
                object[,] reformed = (object[,])pVal;

                string[,] array = new string[13, 74];

                for (int i = 0; i < array.GetLength(0); ++i)
                    for (int j = 0; j < array.GetLength(1); ++j)
                        array[i, j] = reformed[i,j].ToString();

                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, 7].Contains("СЕЧЕНИЕ"))
                    {
                        double mdpAndPA = double.Parse(array[i, 57]);
                        double adp = double.Parse(array[i, 16]);
                        int sechNumber = int.Parse(array[i, 0]);

                        double currentP = rWin.GetSechenValue(TableRastr.Sechen, ParameterRastr.Psech, sechNumber);
                        currentP += some;
                        string sechenName = array[i, 4];
                        Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info,
                            $"            МДП с учетом ПА в сечении {sechenName} составляет {Math.Round(mdpAndPA, 1)}," +
                            $" текущий переток равен {Math.Round(currentP, 1)} МВт." +
                            $" МДП {(currentP > mdpAndPA == true ? "превышен" : "не превышен")}"));

                        if (currentP > mdpAndPA)
                        {
                            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Warning,
                                $"Сечение {sechenName} превышает МДП {Math.Round(mdpAndPA, 1)} на {Math.Round(currentP - mdpAndPA, 1)} МВт"));
                            return false;
                        }
                    }
                }
                return true;
            }
            throw new ApplicationException("Расчет МДП не удалось инициализировать");
        }

        /// <summary>
        /// Метод для загрузки режимов и привязке их к соответствующему расчетному часу
        /// </summary>
        /// <param name="openFileDialog">Список файлов, содержащийся в объекте OpenFileDialog в режиме multiselect</param>
        /// <returns></returns>
        /// <exception cref="FileLoadException"></exception>
        public static LoadedRegimeFiles LoadRegimes(OpenFileDialog openFileDialog)
        {
            // Файлы должны оканчиваться на _[цифра][цифра] чтобы считаться годными
            Regex reg = new Regex(@"_\d{2}$");
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadedRegimeFiles loadedRegimeFiles = new LoadedRegimeFiles();
                var dict = new Dictionary<int, string>();
                foreach(var name in openFileDialog.FileNames)
                {
                    if(reg.IsMatch(name))
                    {
                        Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"    Загружен файл режима {name.Split(@"\").Last()}"));

                        dict.Add(int.Parse(name[^Math.Min(name.Length, 2)..]), name);
                    }
                }
                loadedRegimeFiles.FileHourDict = dict;
                return loadedRegimeFiles;
            }
            else
            {
                Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Error, $"Не удалось загрузить файлы"));

                throw new FileLoadException($"Не удалось загрузить файлы");
            }
        }

        /// <summary>
        /// Метод для загрузки мегаточки
        /// </summary>
        /// <param name="rWin">Прослойка для удобной работы с растром</param>
        /// <param name="megaPointPath">Путь до файла мегаточки</param>
        public static RWin LoadMegapoint(string megaPointPath)
        {
            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Загружен файл мегаточки {megaPointPath.Split(@"\").Last()}"));

            var rWin = new RWin();
            rWin.Load(megaPointPath);
            return rWin;
        }

        /// <summary>
        /// Метод сбрасывающий текущий час мегаточки и выбирающий тот, что задан пользовтелем
        /// </summary>
        /// <param name="hour">Час, накоторый нужно переключить мегаточку</param>
        /// <param name="rWin">Объект с RWin с мегаточкой</param>
        /// <returns></returns>
        public RWin MegaPointSwitchToHour(int hour, RWin rWin)
        {
            while (rWin._rastr.GetCurrentMegapointNum() != 0)
            {
                rWin._rastr.ShiftMegapoint(-1, rWin._rastr.GetCurrentMegapointNum());
            }
            while (rWin._rastr.GetCurrentMegapointNum() != hour)
            {
                rWin._rastr.ShiftMegapoint(1, rWin._rastr.GetCurrentMegapointNum());
            }
            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Мегаточка переключена на {hour} час"));

            return rWin;
        }

        /// <summary>
        /// Метод позволяющий перенести схеморежимную ситуацию со схемы СМЗУ на схему ПБР
        /// </summary>
        /// <param name="megaPointRWin">Объект с RWin с мегаточкой</param>
        /// <param name="smzuRWIN">Объект с RWin со схемой СМЗУ</param>
        /// <param name="mapping">Словарь отображения схемы СМЗУ на схему ПБР</param>
        /// <returns></returns>
        public RWin ModifyBPRwithSMZU(RWin megaPointRWin, RWin smzuRWIN, SMZUtoPBR mapping)
        {
            foreach(var mappingPair in mapping.MappingDict)
            {
                var smzuSTA = smzuRWIN.GetBoolValue(TableRastr.Node, ParameterRastr.Condition, mappingPair.Value[1]);
                megaPointRWin.SetBoolValue(TableRastr.Node, ParameterRastr.Condition, mappingPair.Value[0], smzuSTA);
            }
            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Топология перенесена со схемы СМЗУ на схему мегаточки"));

            return megaPointRWin;
        }


        /// <summary>
        /// Метод вызывающий форму редактирования РЖТ
        /// </summary>
        /// <param name="modifiedSlice">РЖТ на расчетный час, которую необходимо отредактировать</param>
        /// <param name="up">Показывает направление изменения нагрузки 
        /// true - загрузка
        /// false - разгрузка</param>
        public void ModifyRZHT(RankList modifiedSlice, bool up)
        {
            Message?.Invoke(new object(), new EventProtocolMessage(MessageType.Info, $"Начато редактирование РЖТ"));

            var datatable = ConvertToDatatableDesignatedHour(modifiedSlice, up);
            var form1 = new EditRZHTForm(modifiedSlice, datatable);
            form1.RankListEdited += RecieveEditedRankList;
            form1.ShowDialog();
            form1.FormClosed += (o, args) =>
            {
                form1.RankListEdited -= RecieveEditedRankList;
            };

        }

        /// <summary>
        /// Событие редактирования РЖТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RecieveEditedRankList(object sender, RankListEventArgs e)
        {
            _editedRankList = e.rankList; 
        }

        /// <summary>
        /// Метод приведения РЖТ к объекту типа DataTable для отображения на графической форме
        /// </summary>
        /// <param name="table">РЖТ</param>
        /// <param name="up">Показывает направление изменения нагрузки 
        /// true - загрузка
        /// false - разгрузка</param>
        /// <returns></returns>
        public DataTable ConvertToDatatableDesignatedHour(RankList table, bool up)
        {
            var datatable = new DataTable();
            datatable.Columns.Add("Наименование", typeof(string));
            datatable.Columns.Add("Ранк", typeof(int));
            datatable.Columns.Add("Цена РСВ", typeof(decimal));
            datatable.Columns.Add("Цена БР", typeof(decimal));
            datatable.Columns.Add("Текущая загрузка", typeof(decimal));
            datatable.Columns.Add("Изменение загрузки", typeof(decimal));
            if (up)
            {
                datatable.Columns.Add("Резерв на изменение нагрузки", typeof(decimal));
                datatable.Columns.Add("Резерв с учетом скорости", typeof(decimal));
                datatable.Columns.Add("Pmax", typeof(decimal));
            }
            else
            {
                datatable.Columns.Add("Резерв на изменение нагрузки", typeof(decimal));
                datatable.Columns.Add("Резерв с учетом скорости", typeof(decimal));
                datatable.Columns.Add("Pmin", typeof(decimal));
            }

            /*
            var divided = RZHT.DivideUPandDOWN(table);
            var rzhtUP = divided[0];
            var rzhtDOWN = divided[1];
            */
            foreach (var rank in table)
            {
                if (up)
                {
                    datatable.Rows.Add(rank.Gou, rank.RankGou, rank.Price, rank.PriceBR, rank.Pbr, rank.LoadVolume, rank.ReserveUP, rank.SpeedBarierUP,  rank.Pmax);
                }
                else
                {
                    datatable.Rows.Add(rank.Gou, rank.RankGou, rank.Price, rank.PriceBR, rank.Pbr, rank.LoadVolume, rank.ReserveDOWN, rank.SpeedBarierDOWN, rank.Pmin);
                }

            }

            return datatable;
        }

    }
}