using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationServices
{
    /// <summary>
    /// Класс, осуществляющий сериализацию/десериализацию данных
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Десериализовать данные
        /// </summary>
        /// <typeparam name="T">Тип данных, хранимых в листе</typeparam>
        /// <param name="path">Путь сохранения файла</param>
        /// <returns>Лист с данными</returns>
        public static List<T> DeserializeData<T>(string path)
        {
            var formatter = new BinaryFormatter();
            using (var file = new FileStream(path, FileMode.Open))
            {
                var buffer = formatter.Deserialize(file) as List<T>;
                return buffer;
            }
        }

        /// <summary>
        /// Сериализовать данные
        /// </summary>
        /// <typeparam name="T">Тип данных, хранимых в листе</typeparam>
        /// <param name="data">Лист с данными</param>
        /// <param name="path">Путь сохранения файла</param>
        public static void SerializeData<T>(List<T> data, string path)
        {
            var formatter = new BinaryFormatter();
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fileStream, data);
                fileStream.Close();
            }
        }
    }
}
