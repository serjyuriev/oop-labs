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
            Console.WriteLine("Для начала работы программы " +
                "нажмите любую клавишу...");
            Console.ReadKey();

            Console.Write("\nСоздаем списки людей... ");
            var artists = new PersonList();
            var actors = new PersonList();
            Console.WriteLine("Успешно!");
            Console.Write("Заполняем списки людей... ");

            var peopleForFirstList = new Person[]
            {
                new Person("Danzel", "Curry", 35, Genders.Male),
                new Person("Ariana", "Grande", 29, Genders.Female),
                new Person("Kendrick", "Lamar", 33, Genders.Male)
            };

            var peopleForSecondList = new Person[]
            {
                new Person("Tom", "Hanks", 63, Genders.Male),
                new Person("Jim", "Carrey", 57, Genders.Male),
                new Person("Jennifer", "Aniston", 50, Genders.Female)
            };

            artists.AddRange(peopleForFirstList);
            actors.AddRange(peopleForSecondList);

            Properties.Resources.gavno = "gen";

            PrintEndStrings();
            PrintLists(artists, actors);

            Console.Write("Добавляем нового человека в первый список... ");
            var newArtist = new Person("Kid", "Cudi", 35, Genders.Male);
            artists.AddPerson(newArtist);

            PrintEndStrings();

            Console.Write("Копируем второго человека из " +
                "первого списка во второй... ");
            actors.AddPerson(artists[1]);
            Console.WriteLine("Успешно!\n");

            PrintLists(artists, actors);

            Console.Write("Удаляем второго человека из первого списка... ");
            artists.DeletePersonByIndex(1);
            Console.WriteLine("Успешно!");

            PrintLists(artists, actors);

            Console.Write("Очищаем второй список... ");
            actors.Clear();

            PrintEndStrings();

            Console.Write(
                "Добавляем во второй список случайного человека... ");
            actors.AddPerson(RandomPerson.CreateRandomPerson());
            Console.WriteLine("Успешно!");

            Console.WriteLine(
                "Добавим человека, получая параметры от пользователя...");
            actors.AddPerson(IOConsole.CreateNewPerson());

            PrintLists(artists, actors);

            Console.Write("Для завершения работы нажмите любую кнопку...");
            Console.ReadKey();
        }

        /// <summary>
        /// Вывести конечные строки оформления
        /// </summary>
        private static void PrintEndStrings()
        {
            Console.WriteLine("Успешно!");
            Console.ReadKey();
            Console.WriteLine("\n----------\n");
        }

        /// <summary>
        /// Вывести содержимое списков на экран
        /// </summary>
        /// <param name="firstPersonList">Первый список</param>
        /// <param name="secondPersonList">Второй список</param>
        static void PrintLists(PersonList firstPersonList,
            PersonList secondPersonList)
        {
            var personLists = new PersonList[]
            {
                firstPersonList,
                secondPersonList
            };

            for (int i = 0; i < personLists.Length; i++)
            {
                Console.WriteLine($"Список #{i + 1}\n");

                for (int j = 0; j < personLists[i].Length; j++)
                {
                    Console.WriteLine(
                        personLists[i][j].FormInfoAboutPerson());
                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            Console.ReadKey();
            Console.WriteLine("----------\n");
        }
    }
}
