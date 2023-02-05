using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZHT_Model
{
    /// <summary>
    /// Класс служит для отображения узлов со схемы СМЗУ на схему ПБР
    /// </summary>
    public class SMZUtoPBR
    {
        /// <summary>
        /// Словарь отображения узлов схемы СМЗУ на схему ПБР
        /// Ключ - Название узла в таблицу узлов ПБР
        /// Значение - Массив целых чисел, первое число узел в ПБР, второе узел в СМЗУ
        /// </summary>
        private Dictionary<string, int[]> _mappingDict;
         
        /// <summary>
        /// Свойство для записи в словарь отображения
        /// </summary>
        public Dictionary<string, int[]> MappingDict
        {
            get => _mappingDict; set => _mappingDict = value; 
        }

        /// <summary>
        /// Метод для загрузки CSV файла для заполнения словаря отображения схемы СМЗУ на схему ПБР
        /// </summary>
        /// <param name="csvPath">Путь до файла CSV</param>
        public void LoadCSV(string csvPath)
        {
            var mappingDict = new Dictionary<string, int[]>();
            using (StreamReader sr = new StreamReader(csvPath))
            {
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] data = currentLine.Split(';');
                    mappingDict.Add(data[0], new int[] { int.Parse(data[1]), int.Parse(data[2]) });
                }
            }
            this.MappingDict = mappingDict;
        }
    }
}
