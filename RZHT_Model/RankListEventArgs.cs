using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    /// <summary>
    /// Класс, предназначеный для передачи отредактированной части РЖТ с формы в класс RZHT
    /// </summary>
    public class RankListEventArgs : EventArgs
    {
        /// <summary>
        /// Отредактированная часть РЖТ
        /// </summary>
        public RankList rankList { get; set; }
    }
}
