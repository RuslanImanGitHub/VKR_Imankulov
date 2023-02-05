using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    /// <summary>
    /// Класс, содержащий информацию о ранке ГОУ в РЖТ
    /// </summary>
    public class Rank
    {
        /// <summary>
        /// Название ГОУ
        /// </summary>
        private string _gou;
        /// <summary>
        /// ID ГОУ
        /// </summary>
        private int _id;
        /// <summary>
        /// ID родительского объекта
        /// </summary>
        private int _parentID;
        /// <summary>
        /// Название родительского объекта
        /// </summary>
        private string _parentGou;
        /// <summary>
        /// Контрольный резерв
        /// </summary>
        private decimal _controlVolume;
        /// <summary>
        /// Цена РСВ или ОЦПЗ/ОЦПУ
        /// </summary>
        private decimal _price;
        /// <summary>
        /// Цена БР
        /// </summary>
        private decimal _priceBR;
        /// <summary>
        /// 
        /// </summary>
        private decimal _volume;
        /// <summary>
        /// 
        /// </summary>
        private decimal _prevVolume;
        /// <summary>
        /// Максимум нагрузки
        /// </summary>
        private decimal _pmax;
        /// <summary>
        /// Минимум нагрузки
        /// </summary>
        private decimal _pmin;
        /// <summary>
        /// Текущая загрузка/ загрузка ПБР
        /// </summary>
        private decimal _pbr;
        /// <summary>
        /// Участие в распределении нагрузки (В тех РЖТ, что скидывали всегда true)
        /// </summary>
        private int _isbid;
        /// <summary>
        /// Ранк ГОУ
        /// </summary>
        private int _intval;
        /// <summary>
        /// Резерв на загрузку верх
        /// </summary>
        private decimal _reserveUP;
        /// <summary>
        /// Резерв на загрузку вниз
        /// </summary>
        private decimal _reserveDOWN;
        /// <summary>
        /// Резерв на загрузку вверх с учетом маневренности (хз откуда инфа)
        /// </summary>
        private decimal _reserveRampUP;
        /// <summary>
        /// Резерв на загрузку вниз с учетом маневренности (хз откуда цифра)
        /// </summary>
        private decimal _reserveRampDOWN;
        /// <summary>
        /// Тип ГОУ, неизвестно
        /// </summary>
        private int _type;

        /// <summary>
        /// Распределенная нагрузка, в ходе оптимизационного расчета
        /// </summary>
        private decimal _loadVolume;
        /// <summary>
        /// Резерв на загрузку вверх с учетом маневренности
        /// </summary>
        private decimal _speedBarierUP;
        /// <summary>
        /// Резерв на загрузку вниз с учетом маневренности
        /// </summary>
        private decimal _speedBarierDOWN;
        /// <summary>
        /// Цена используемая в оптимизационном расчете
        /// </summary>
        private decimal _loadPrice;

        /// <summary>
        /// <inheritdoc cref="_gou"/>
        /// </summary>
        public string Gou
        {
            get => _gou; set => _gou = value;
        }
        /// <summary>
        /// <inheritdoc cref="_id"/>
        /// </summary>
        public int Id
        {
            get => _id; set => _id = value;
        }
        /// <summary>
        /// <inheritdoc cref="_parentID"/>
        /// </summary>
        public int ParentID
        {
            get => _parentID; set => _parentID = value;
        }
        /// <summary>
        /// <inheritdoc cref="_parentGou"/>
        /// </summary>
        public string ParentGou
        {
            get => _parentGou; set => _parentGou = value;
        }
        /// <summary>
        /// <inheritdoc cref="_controlVolume"/>
        /// </summary>
        public decimal ControlVolume
        {
            get => _controlVolume; set => _controlVolume = value;
        }
        /// <summary>
        /// <inheritdoc cref="_price"/>
        /// </summary>
        public decimal Price
        {
            get => _price; set => _price = value;
        }
        /// <summary>
        /// <inheritdoc cref="_priceBR"/>
        /// </summary>
        public decimal PriceBR
        {
            get => _priceBR; set => _priceBR = value;
        }
        /// <summary>
        /// <inheritdoc cref="_volume"/>
        /// </summary>
        public decimal Volume
        {
            get => _volume; set => _volume = value;
        }
        /// <summary>
        /// <inheritdoc cref="_prevVolume"/>
        /// </summary>
        public decimal PrevVolume
        {
            get => _prevVolume; set => _prevVolume = value;
        }
        /// <summary>
        /// <inheritdoc cref="_pmax"/>
        /// </summary>
        public decimal Pmax
        {
            get => _pmax; set => _pmax = value;
        }
        /// <summary>
        /// <inheritdoc cref="_pmin"/>
        /// </summary>
        public decimal Pmin
        {
            get => _pmin; set => _pmin = value;
        }
        /// <summary>
        /// <inheritdoc cref="_pbr"/>
        /// </summary>
        public decimal Pbr
        {
            get => _pbr; set => _pbr = value;
        }
        /// <summary>
        /// <inheritdoc cref="_isbid"/>
        /// </summary>
        public int IsBid
        {
            get => _isbid; set => _isbid = value;
        }
        /// <summary>
        /// <inheritdoc cref="_intval"/>
        /// </summary>
        public int RankGou
        {
            get => _intval; set => _intval = value;
        }
        /// <summary>
        /// <inheritdoc cref="_reserveUP"/>
        /// </summary>
        public decimal ReserveUP
        {
            get => _reserveUP; set => _reserveUP = value; 
        }
        /// <summary>
        /// <inheritdoc cref="_reserveDOWN"/>
        /// </summary>
        public decimal ReserveDOWN
        {
            get => _reserveDOWN; set => _reserveDOWN = value; 
        }
        /// <summary>
        /// <inheritdoc cref="_reserveRampUP"/>
        /// </summary>
        public decimal ReserveRampUP
        {
            get => _reserveRampUP; set => _reserveRampUP = value;
        }
        /// <summary>
        /// <inheritdoc cref="_reserveRampDOWN"/>
        /// </summary>
        public decimal ReserveRampDOWN
        {
            get => _reserveRampDOWN; set => _reserveRampDOWN = value; 
        }
        /// <summary>
        /// <inheritdoc cref="_type"/>
        /// </summary>
        public int Type
        {
            get => _type; set => _type = value;
        }

        // Дополнительные поля
        /// <summary>
        /// <inheritdoc cref="_loadVolume"/>
        /// </summary>
        public decimal LoadVolume
        {
            get => _loadVolume; set => _loadVolume = value;
        }
        /// <summary>
        /// <inheritdoc cref="_speedBarierUP"/>
        /// </summary>
        public decimal SpeedBarierUP
        {
            get => _speedBarierUP; set => _speedBarierUP = value;
        }
        /// <summary>
        /// <inheritdoc cref="_speedBarierDOWN"/>
        /// </summary>
        public decimal SpeedBarierDOWN
        {
            get => _speedBarierDOWN; set => _speedBarierDOWN = value;
        }
        /// <summary>
        /// <inheritdoc cref="_loadPrice"/>
        /// </summary>
        public decimal LoadPrice
        {
            get => _loadPrice; set => _loadPrice = value;
        }


        // Свойства расчетные
        /// <summary>
        /// Возвращает максимум из цен ПБР и БР
        /// </summary>
        public decimal MaxCost
        {
            get => Math.Max(this.Price, this.PriceBR);
        }
        /// <summary>
        /// Возвращает минимум из цен ПБР и БР
        /// </summary>
        public decimal MinCost
        {
            get => Math.Min(this.Price, this.PriceBR);
        }
    }
}