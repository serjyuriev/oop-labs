using PersonLib;
using System;

namespace TestingApp
{
    /// <summary>
    /// Класс, позволяющий получить рандомного человека
    /// </summary>
    public static class RandomPerson
    {
        #region Fields

        /// <summary>
        /// Рандом
        /// </summary>
        private static Random _random = new Random();

        #endregion

        #region Methods

        /// <summary>
        /// Сгенерировать нового человека
        /// </summary>
        /// <returns>Сформированный человек</returns>
        public static Person CreateRandomPerson()
        {
            var randomizedPerson = new Person();

            // Элемент массива представляет собой строку 
            // формата *** Имя\tПол ***
            var allFirstNames = Properties.Resources.FirstNameDB.Split('\n');

            var nameRandomIndex = _random.Next(0, allFirstNames.Length - 1);
            var randomizedElement = allFirstNames[nameRandomIndex];

            var firstNameAndGender = randomizedElement.Split('\t');
            randomizedPerson.Firstname = firstNameAndGender[0];
            randomizedPerson.Gender = (Genders)Enum.Parse(
                typeof(Genders), firstNameAndGender[1]);

            randomizedPerson.Age = _random.Next(0, 122);
            var allLastNames = Properties.Resources.LastNameDB.Split('\n');
            randomizedElement =
                allLastNames[_random.Next(0, allLastNames.Length - 1)];
            randomizedPerson.Lastname =
                randomizedElement.Substring(0, randomizedElement.Length - 2);

            return randomizedPerson;
        }

        #endregion
    }
}
