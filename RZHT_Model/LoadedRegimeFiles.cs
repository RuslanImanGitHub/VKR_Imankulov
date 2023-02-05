using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    /// <summary>
    /// Класс служащий для хранения путей режимов в соответствии с расчетными часами
    /// </summary>
    public class LoadedRegimeFiles
    {
        // int [час к которому привязан файл], string [путь до файла]
        /// <summary>
        /// Словарь содержащий пути до расчетных схем с привязкой к расчетным часам
        /// </summary>
        private Dictionary<int, string> _fileHourDict;

        /// <summary>
        /// Свойство для записи в словарь путей
        /// </summary>
        public Dictionary<int, string> FileHourDict
        {
            get => _fileHourDict; set => _fileHourDict = value;
        }
    }
}
