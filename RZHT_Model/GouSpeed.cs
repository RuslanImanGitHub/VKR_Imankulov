namespace RZHT_Model
{
    /// <summary>
    /// Класс служит для хранения значения скорости изменения нагрузки
    /// </summary>
    public class GouSpeed : Dictionary<string, decimal>
    {
        /// <summary>
        /// Словарь содержащий значения маневренности ГОУ
        /// Ключ - Название ГОУ
        /// Значение - Массив со значениями маневренности вверх и вниз
        /// </summary>
        private Dictionary<string, decimal[]> _speedDict;

        /// <summary>
        /// Свойство для записи в словарь маневренности
        /// </summary>
        public Dictionary<string, decimal[]> SpeedDict
        {
            get => _speedDict; set => _speedDict = value; 
        }

        /// <summary>
        /// Тестовый метод для заполнения словаря маневренности 
        /// </summary>
        /// <param name="rzht">РЖТ</param>
        /// <returns></returns>
        public void CreateDefault(RZHT rzht)
        {
            var speedGou = new Dictionary<string, decimal[]>();
            var distinctNames = new List<string>();
            foreach (var ranklist in rzht.UnsortedTable.Values)
            {
                foreach (var rank in ranklist)
                {
                    distinctNames.Add(rank.Gou);
                }
            }
            distinctNames = distinctNames.Distinct().ToList();
            foreach(var name in distinctNames)
            {
                speedGou.Add(name, new decimal[] { 10, 10 });
            }
            this.SpeedDict = speedGou;
        }

        /// <summary>
        /// Метод загрузки словаря маневренности из CSV файла
        /// </summary>
        /// <param name="csvPath">Путь до файла CSV</param>
        public void LoadFromCSV(string csvPath)
        {
            var speedDict = new Dictionary<string, decimal[]>();
            using (StreamReader sr = new StreamReader(csvPath))
            {
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] data = currentLine.Split(';');
                    speedDict.Add(data[0], new decimal[] { decimal.Parse(data[1]), decimal.Parse(data[2]) });
                }
            }
            this.SpeedDict = speedDict;
        }
    }
}
