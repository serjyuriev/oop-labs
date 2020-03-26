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
        /// Генерация случайной персоны (взрослый/ребенок)
        /// </summary>
        /// <returns>Сгенерированный ребенок/взрослый</returns>
        public static PersonBase CreateRandomPerson()
        {
            if (_random.Next(0, 2) != 0)
            {
                return CreateRandomAdult();
            }
            else
            {
                return CreateRandomChild();
            }
        }

        /// <summary>
        /// Сгенерировать
        /// </summary>
        /// <param name="forMarriage">Генерация партнера
        /// для человека, состоящего в браке</param>
        /// <param name="partner">Партнер</param>
        /// <returns>Сгенерированный взрослый</returns>
        public static Adult CreateRandomAdult(bool forMarriage = false,
            Adult partner = null)
        {
            var randomizedAdult = new Adult();

            GeneratePersonInfo(randomizedAdult);

            randomizedAdult.Age = _random.Next(Adult.MINAGE, Adult.MAXAGE);
            
            if (!forMarriage)
            {
                randomizedAdult.MaritalStatus =
                    (MaritalStatus)_random.Next(0, 4);

                if (randomizedAdult.MaritalStatus == MaritalStatus.Married)
                {
                    randomizedAdult.Partner = 
                        CreateRandomAdult(true, randomizedAdult);
                }
            }
            else
            {
                randomizedAdult.MaritalStatus = MaritalStatus.Married;
                randomizedAdult.Partner = partner;
            }

            var allCompanyNames =
                Properties.Resources.CompanyNames.Split('\n');
            var companyRandomIndex = 
                _random.Next(0, allCompanyNames.Length - 1);
            randomizedAdult.PlaceOfWork =
                allCompanyNames[companyRandomIndex];

            randomizedAdult.PassportNumber = GeneratePassportData(true);
            randomizedAdult.PassportSerial = GeneratePassportData(false);

            return randomizedAdult;
        }

        /// <summary>
        /// Сгенерировать нового ребенка
        /// </summary>
        /// <returns>Сгенерированный ребенок</returns>
        public static Child CreateRandomChild()
        {
            var randomizedChild = new Child();

            GeneratePersonInfo(randomizedChild);
            randomizedChild.Age = _random.Next(Child.MINAGE, Child.MAXAGE);

            for (int i = 0; i < 2; i++)
            {
                bool haveMother = _random.Next(0, 1) != 0;
                
                if (haveMother)
                {
                    randomizedChild.Mother = CreateRandomAdult();
                }

                bool haveFather = _random.Next(0, 1) != 0;

                if (haveFather)
                {
                    randomizedChild.Father = CreateRandomAdult();
                }
            }

            var allKindergardenNames =
                Properties.Resources.kindergarden.Split('\n');
            var kindergardenRandomIndex =
                _random.Next(0, allKindergardenNames.Length - 1);
            randomizedChild.KindergardenOrSchool =
                allKindergardenNames[kindergardenRandomIndex];

            return randomizedChild;
        }

        /// <summary>
        /// Заполнить нули в начале серии/номера паспорта
        /// </summary>
        /// <param name="PassportData">Серия/номер паспорта</param>
        /// <param name="amountOfRequriedNumbers">Исправленное 
        /// значение</param>
        /// <returns></returns>
        private static string FillPassportNumberWithZeros(
            string PassportData, int amountOfRequriedNumbers)
        {
            if (PassportData.Length < amountOfRequriedNumbers)
            {
                var amountOfZero = 
                    amountOfRequriedNumbers - PassportData.Length;

                for (int i = 0; i < amountOfZero; i++)
                {
                    PassportData = "0" + PassportData;
                }
            }

            return PassportData;
        }

        /// <summary>
        /// Сгенерировать номер/серию паспорта
        /// </summary>
        /// <param name="isPassportNumber">Генерировать номер</param>
        /// <returns></returns>
        private static string GeneratePassportData(bool isPassportNumber)
        {
            string data;

            if (isPassportNumber)
            {
                data = FillPassportNumberWithZeros(
                    _random.Next(0, 999999).ToString(), 6);
            }
            else
            {
                data = FillPassportNumberWithZeros(
                    _random.Next(0, 9999).ToString(), 4);
            }

            return data;
        }

        /// <summary>
        /// Заполнить базовые поля персоны
        /// </summary>
        /// <param name="person">Персона для заполнения</param>
        public static void GeneratePersonInfo(PersonBase person)
        {
            // Элемент массива представляет собой строку 
            // формата *** Имя\tПол ***
            var allFirstNames = Properties.Resources.FirstNameDB.Split('\n');

            var nameRandomIndex = _random.Next(0, allFirstNames.Length - 1);
            var randomizedElement = allFirstNames[nameRandomIndex];

            var firstNameAndGender = randomizedElement.Split('\t');
            person.Firstname = firstNameAndGender[0];
            person.Gender = (Genders)Enum.Parse(
                typeof(Genders), firstNameAndGender[1]);

            var allLastNames = Properties.Resources.LastNameDB.Split('\n');
            randomizedElement =
                allLastNames[_random.Next(0, allLastNames.Length - 1)];
            person.Lastname =
                randomizedElement.Substring(0, randomizedElement.Length - 2);
        }

        #endregion
    }
}
