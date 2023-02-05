using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    public class EventForProtocolMessage
    {
        /// <summary>
        /// Класс, предназначенный для передачи сообщений из расчётного метода 
        /// в графический интерфейс с указанием типа сообщения
        /// </summary>
        public class EventProtocolMessage : EventArgs
        {
            /// <summary>
            /// Тип сообщения
            /// </summary>
            public MessageType MessageType { get; set; }

            /// <summary>
            /// Запись сообщения
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// Конструктор сообщения
            /// </summary>
            /// <param name="type">Тип сообщения</param>
            /// <param name="message">Комментарий</param>
            public EventProtocolMessage(MessageType type, string message)
            {
                MessageType = type;
                Message = message;
            }
        }
    }
}
