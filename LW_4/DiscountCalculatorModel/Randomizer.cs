using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculatorModel
{
    public static class Randomizer
    {
        public static List<double> GetRandomValuesForSales(Random random)
        {
            var values = new List<double>();

            values.Add(random.Next(0, 2));
            values.Add(Math.Round(random.NextDouble() * 10000, 2));

            if (values[0] == 0)
            {
                values.Add(Math.Round(random.NextDouble() * 100));
            }
            else
            {
                values.Add(Math.Round(random.NextDouble() * 10000, 2));
            }

            return values;
        }
    }
}
