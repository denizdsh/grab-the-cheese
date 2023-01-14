using System;
using System.IO;
using System.Text.Json;

namespace grab_the_cheese
{
    internal class JsonFileOperationService<T>
    {
        private string FilePath { get; set; }
        private Func<T> GetDefaultValue { get; set; } = () => default(T);

        public JsonFileOperationService(string filePath)
        {
            FilePath = filePath;
        }

        public JsonFileOperationService(string filePath, Func<T> getDefaultValue) : this(filePath)
        {
            GetDefaultValue = getDefaultValue;
        }

        public void UpdateObject(T obj)
        {
            string jsonString = JsonSerializer.Serialize<T>(obj);
            File.WriteAllText(FilePath, jsonString);
        }

        public T GetObject()
        {
            try
            {
                string jsonString = File.ReadAllText(FilePath);

                T obj = JsonSerializer.Deserialize<T>(jsonString);

                return obj;
            }
            catch
            {
                return GetDefaultValue();
            }
        }
    }
}
