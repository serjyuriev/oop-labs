using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculatorModel
{
    public static class Randomizer
    {
        /// <summary>
        /// Рандом
        /// </summary>
        private static Random _random = new Random();

        /// <summary>
        /// Случайная генерация значений для заполнения параметров скидки
        /// </summary>
        /// <returns>Список сгенерированных значений
        /// (тип, исходная цена, скидка)</returns>
        public static List<double> GetRandomValuesForSales()
        {
            var values = new List<double>();

            values.Add(_random.Next(0, 2));
            values.Add(Math.Round(_random.NextDouble() * 10000, 2));

            if (values[0] == 0)
            {
                values.Add(Math.Round(_random.NextDouble() * 100));
            }
            else
            {
                values.Add(Math.Round(_random.NextDouble() * 10000, 2));
            }

            return values;
        }
    }
}
