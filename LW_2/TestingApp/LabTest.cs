using PersonLib;
using System;

namespace TestingApp
{
    /// <summary>
    /// Класс для тестирования созданной библиотеки.
    /// </summary>
    public class Testing
    {
        /// <summary>
        /// Точка вхождения.
        /// </summary>
        /// <param name="args">Аргументы</param>
        public static void Main(string[] args)
        {
            Console.Write("Генерируем 7 человек...");

            var persons = new PersonList();

            for (int i = 0; i < 7; i++)
            {
                persons.AddPerson(RandomPerson.CreateRandomPerson());
            }

            Console.WriteLine(" Готово!\n\n");
            Console.WriteLine("Случайно сгенерированный список:\n");

            for (int i = 0; i < persons.Length; i++)
            {
                Console.WriteLine(persons[i].FormInfoAboutPerson());
                Console.WriteLine();
            }

            Console.Write("Четвертый человек в списке - это ");

            if (persons[3] is Adult)
            {
                Console.WriteLine("взрослый человек!");
                var person = persons[3] as Adult;
                Console.WriteLine(person.Smoke());
            }
            else if (persons[3] is Child)
            {
                Console.WriteLine("ребенок!");
                var person = persons[3] as Child;
                Console.WriteLine(person.TryToSmoke());
            }

            Console.WriteLine(persons[3].WhoAmI());

            Console.ReadKey();
        }
    }
}
