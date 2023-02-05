using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    /// <summary>
    /// Класс-перечисление, предназначенный для указания типа
    /// сообщения в протоколе (Инфо, Предупреждение, Ошибка)
    /// </summary>
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
}
