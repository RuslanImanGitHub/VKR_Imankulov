using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace RZHT_Model
{
    /// <summary>
    /// Класс служит для отображения РЖТ на расчетную схему RastrWin3
    /// </summary>
    public class RZHTtoSCHEME
    {
        /// <summary>
        /// Словарь отображения записей из РЖТ на расчетную схему RastrWin3
        /// Ключ - Название ГОУ и ранк
        /// Значение - Узлы соответствующие ГОУ в расчетной схеме
        /// </summary>
        private Dictionary<string[], int[]> _mappingDict;

        /// <summary>
        /// Свойство для записи в словарь отображения
        /// </summary>
        public Dictionary<string[], int[]> MappingDict
        {
            get => _mappingDict; set => _mappingDict = value;
        }

        /// <summary>
        /// Словарь соответствия ГОУ узлам из расчетной схеме
        /// Ключ - Название ГОУ
        /// Значение - Узлы соответствующие ГОУ в расчетной схеме
        /// </summary>
        public Dictionary<string, int[]> _mappingTemplate;

        /// <summary>
        /// Свойство записи в словарь соответствия
        /// </summary>
        public Dictionary<string, int[]> MappingTemplate
        {
            get => _mappingTemplate; set => _mappingTemplate = value;
        }

        /// <summary>
        /// Метод составления словаря соответствия из файла настройки ПАК СРПГ в формате json
        /// </summary>
        /// <param name="jsonFile">Путь до файла настройки ПАК СРПГ в формате json</param>
        public void ReadJSON(string jsonFile)
        {
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(jsonFile));
            string name = json["имя"];
            string gouNumber = json["ГОУ"];
            // Считать Генераторы построчно (Хотя если нет совпадений, то не надо)
            string schemeNode = json["генераторы"];
        }

        /// <summary>
        /// Тестовый метод, служит для демонстрации
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int[]> BuildDefaultTemplate()
        {
            var dict = new Dictionary<string, int[]>();
            dict.Add("Беловская ГРЭС (Блок 3,5)", new[] { 249, 253 });
            dict.Add("Беловская ГРЭС (Блок 1,2)", new[] { 245, 247 });
            dict.Add("Беловская ГРЭС (Блок 4)", new[] { 251 });
            dict.Add("Кемеровская ГРЭС", new[] { 127, 92, 93, 94 });
            dict.Add("Томь-Усинская ГРЭС (Блок 6,8,9)", new[] { 340, 343, 349 } );
            dict.Add("Кузнецкая ТЭЦ", new[] { 315, 280 });
            dict.Add("Беловская ГРЭС (Блок 6)", new[] { 255 });
            dict.Add("Томь-Усинская ГРЭС (Блок 1-3)", new[] { 326, 328, 330 });
            dict.Add("Томь-Усинская ГРЭС (Блок 4)", new[] { 332 });
            dict.Add("Томь-Усинская ГРЭС (Блок 5)", new[] { 334 });
            dict.Add("Кемеровская ТЭЦ", new[] { 130, 118 });
            dict.Add("Ново-Кемеровская ТЭЦ", new[] { 133, 95, 135, 96 });
            dict.Add("Томская ТЭЦ-3", new[] { 735 });
            dict.Add("Томская ГРЭС-2 (ТГ-2,3,6-8)", new[] { 361, 363, 364 });
            dict.Add("Томская ТЭЦ-1", new[] { 791, 792 });
            dict.Add("Южно-Кузбасская ГРЭС", new[] { 299, 303, 298, 306 });
            dict.Add("ТЭЦ СХК (ТГ-10,11)", new[] { 724, 725 });
            dict.Add("ТЭЦ СХК (ТГ-13)", new[] { 3239 });
            dict.Add("ТЭЦ СХК (ТГ-1,2,7,9,15)", new[] { 719, 720, 722, 723, 729 });
            dict.Add("ГТЭС Новокузнецкая (ГТУ-14)", new[] { 89 });
            dict.Add("ГТЭС Новокузнецкая (ГТУ-15)", new[] { 90 });
            return dict;
        }

        /// <summary>
        /// Метод составления словаря отображения из словаря соответствия и РЖТ
        /// </summary>
        /// <param name="rzht">РЖТ</param>
        /// <param name="mappingTemplate">Словарь соответствия</param>
        public void BuildMapping(RZHT rzht, Dictionary<string, int[]> mappingTemplate)
        {
            var table = rzht.UnsortedTable;
            var mappingDict = new Dictionary<string[], int[]>();
            foreach (var rankLists in table.Values)
            {
                foreach(var rank in rankLists)
                {
                    mappingDict.Add(new[] { rank.Gou, rank.RankGou.ToString() }, mappingTemplate[rank.Gou]);

                }
            }
            var result = mappingDict.Distinct().ToDictionary(x => x.Key, y => y.Value);
            this.MappingDict = result;
        }

        /// <summary>
        /// Метод составления словаря отображения только с помощью РЖТ, используется словарь соответствия уже загруженный в класс
        /// </summary>
        /// <param name="rzht"></param>
        public void BuildMapping(RZHT rzht)
        {
            try
            {
                var table = rzht.UnsortedTable;
                var mappingDict = new Dictionary<string[], int[]>();
                foreach (var rankLists in table.Values)
                {
                    foreach (var rank in rankLists)
                    {
                        mappingDict.Add(new[] { rank.Gou, rank.RankGou.ToString() }, this.MappingTemplate[rank.Gou]);

                    }
                }
                var result = mappingDict.Distinct().ToDictionary(x => x.Key, y => y.Value);
                this.MappingDict = result;
            }
            catch
            {
                throw new ArgumentException("Не удалось составить словарь отображения, отсутствует словарь соответствия");
            }
        }

        /// <summary>
        /// Метод загрузки словаря соответствия из CSV файла
        /// </summary>
        /// <param name="csvPath">Путь до файла CSV</param>
        public void LoadFromCSV(string csvPath)
        {
            var dictTemplate = new Dictionary<string, int[]>();
            using (StreamReader sr = new StreamReader(csvPath))
            {
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] data = currentLine.Split(';');
                    int[] intArray = new int[data[1].Length];
                    foreach(var intData in data[1])
                    {
                        intArray.Append(int.Parse(intData.ToString()));
                    }
                    dictTemplate.Add(data[0], intArray);
                }
            }
            this.MappingTemplate = dictTemplate;
        }
    }
}
