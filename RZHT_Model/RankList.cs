using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    /// <summary>
    /// Класс содержащий в себе список ранков ГОУ
    /// </summary>
    public class RankList : List<Rank>
    {
        /// <summary>
        /// Список ранков
        /// </summary>
        public List<Rank> _ranks = new List<Rank>();

        /// <summary>
        /// Свойство для записи в список ранков
        /// </summary>
        public List<Rank> Ranks
        {
            get => _ranks;
            set => _ranks = value;
        }

        /// <summary>
        /// Сортирует весь объект по наименьшей стоимости рангов, используется для разгрузки вниз
        /// </summary>
        public RankList SortMoreLoad
        {
            get
            {
                this.Ranks = this._ranks.OrderBy(rank => rank.MinCost).ToList();
                return this;
            }
        }
        /// <summary>
        /// Сортирует весь объект по наибольшей стоимости рангов, используется для загрузки верх
        /// </summary>
        public RankList SortLessLoad
        {
            get
            {
                this.Ranks = this._ranks.OrderBy(rank => rank.MaxCost).ToList();
                return this;
            }
        }
        /// <summary>
        /// Возвращает список из рангов с заполненным полем loadVolume
        /// </summary>
        public List<Rank> LoadedRanks
        {
            get
            {
                List<Rank> loadedRanks = new List<Rank>();
                foreach (var rank in this)
                {
                    if (rank.LoadVolume != 0)
                    {
                        loadedRanks.Add(rank);
                    }
                }
                return loadedRanks;
            }
        }

        /// <summary>
        /// Показывает актуальную сумму резервов ГОУ, поделенных по названиям с учетом текущей загрузки поля loadVolume
        /// </summary>
        public decimal[] GouReserve
        {
            get
            {
                decimal[] reserve = new decimal[2];
                decimal reserveUP = 0;
                decimal reserveDOWN = 0;
                foreach(var rank in this)
                {
                    reserveUP += rank.ReserveUP;
                    reserveDOWN += rank.ReserveDOWN;
                }

                reserve[0] = reserveUP;
                reserve[1] = reserveDOWN;

                return reserve;
            }
        }

        /// <summary>
        /// Показывает совокупную сумму резервов в списке ранков
        /// </summary>
        /*public decimal[] TotalReseve
        {
            get
            {
                var sumReserve = new decimal[2];
                foreach (var dict in this.GouReserve)
                {
                    int i = 0;
                    foreach (var entry in dict)
                    {
                        sumReserve[i] += entry.Value;
                    }
                    i++;
                }
                return sumReserve;
            }
        }*/

        /// <summary>
        /// Показывает общую загрузку в списке ранков
        /// </summary>
        public Dictionary<string, decimal> TotalLoad
        {
            get
            {
                return this.Ranks.GroupBy(x => x.Gou).ToDictionary(g => g.Key, g => g.Sum(x => x.LoadVolume));
            }
        }

        /// <summary>
        /// Свойство показывающее сколько раз тот или иной ГОУ присутствовал в списке ранков на загрузку и разгрузку
        /// </summary>
        public Dictionary<string, int[,]> GouRankCount
        {
            get
            {
                var result = new Dictionary<string, int[,]>();
                foreach (var rank in this)
                {
                    var rankCount = RankCount(rank.Gou);
                    result[rank.Gou] = rankCount;
                }
                return result;
            }
        }

        /// <summary>
        /// Возвращает совокупную стоимость резервов на загрузку или разгрузку
        /// </summary>
        /// <param name="up">Показывает направление изменения нагрузки true - загрузка вверх, false - разгрузка вниз</param>
        /// <param name="rankList">Список рангов</param>
        /// <returns></returns>
        public decimal TotalReserveCost(bool up, RankList rankList)
        {
            decimal sum = 0;
            if (up == true)
            {
                foreach (var rank in rankList)
                {
                    sum += rank.MaxCost * rank.ReserveUP;
                }
                return sum;
            }
            else
            {
                foreach (var rank in rankList)
                {
                    sum += rank.MinCost * rank.ReserveUP;
                }
                return sum;
            }
        }
        /// <summary>
        /// Возвращает наименьшую цену ранга для загрузки
        /// </summary>
        /// <param name="rankList">Список ранков</param>
        /// <returns></returns>
        public static decimal MoreLoadLowestCost(RankList rankList)
        {
            decimal lowestCost = rankList[0].MinCost;
            foreach (var rank in rankList)
            {
                if (rank.MinCost < lowestCost)
                {
                    lowestCost = rank.MinCost;
                }
            }
            return lowestCost;
        }
        /// <summary>
        /// Возвращает наибольшую цену ранга для разгрузки
        /// </summary>
        /// <param name="rankList">Список ранков</param>
        /// <returns></returns>
        public static decimal LessLoadMaxCost(RankList rankList)
        {
            decimal lowestCost = rankList[0].MaxCost;
            foreach (var rank in rankList)
            {
                if (rank.MaxCost < lowestCost)
                {
                    lowestCost = rank.MaxCost;
                }
            }
            return lowestCost;
        }


        /// <summary>
        /// Метод, возвращающий массив из количества упоминаний определенного 
        /// ГОУ по рангам для загрузки и разгрузки в данном списке ранков.
        /// </summary>
        /// <param name="gouName">Название ГОУ</param>
        /// <returns></returns>
        private int[,] RankCount(string gouName)
        {
            
            var rankCount = new int[2, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
            foreach(var rank in this)
            {
                if (gouName == rank.Gou && rank.ReserveUP > 0)
                {
                    rankCount[0,(rank.RankGou - 1)] += 1;
                }
                else if(gouName == rank.Gou && rank.ReserveDOWN > 0)
                {
                    rankCount[1,(rank.RankGou - 1)] += 1;
                }
            }
            return rankCount;

        }
    }
    /// <summary>
    /// Модификация класса массивов, для добавления функционала выбора одного ряда из многомерного массива
    /// </summary>
    public static class ArrayExt
    {
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];

            int size;

            if (typeof(T) == typeof(bool))
                size = 1;
            else if (typeof(T) == typeof(char))
                size = 2;
            else
                size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }
    }
}
