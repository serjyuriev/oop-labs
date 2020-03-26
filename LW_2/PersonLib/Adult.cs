using System;

namespace PersonLib
{
    /// <summary>
    /// Взрослый человек
    /// </summary>
    public class Adult : PersonBase
    {
        #region Constants

        /// <summary>
        /// Минимальный возраст взрослого
        /// </summary>
        public const int MINAGE = 18;

        /// <summary>
        /// Максимальный возраст взрослого
        /// </summary>
        public const int MAXAGE = 122;

        #endregion

        #region Fields

        /// <summary>
        /// Семейное положение
        /// </summary>
        private MaritalStatus _maritalStatus;

        /// <summary>
        /// Партнер
        /// </summary>
        private Adult _partner;

        /// <summary>
        /// Номер паспорта
        /// </summary>
        private string _passportNumber;

        /// <summary>
        /// Серия паспорта
        /// </summary>
        private string _passportSerial;

        /// <summary>
        /// Место работы
        /// </summary>
        private string _placeOfWork;

        #endregion

        #region Properties

        /// <summary>
        /// Возраст
        /// </summary>
        public override int Age 
        {
            get { return base.Age; }
            set
            {
                if (value < MINAGE || value > MAXAGE)
                {
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be from " +
                        $"{MINAGE} to {MAXAGE}.");
                }
                base.Age = value;
            }
        }

        /// <summary>
        /// Состояние брака
        /// </summary>
        public MaritalStatus MaritalStatus
        {
            get { return _maritalStatus; }
            set { _maritalStatus = value; }
        }

        /// <summary>
        /// Партнер
        /// </summary>
        public Adult Partner
        {
            get { return _partner; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        $"{nameof(Partner)}", $"{nameof(Partner)} value is" +
                        " null!");
                }

                if (MaritalStatus == MaritalStatus.Married &&
                    value.MaritalStatus == MaritalStatus.Married)
                {
                    _partner = value;
                }
                else
                {
                    throw new ArgumentException(
                        "One of adults cannot have partners! " +
                        "Please check marital statuses!");
                }
            }
        }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string PassportNumber
        {
            get { return _passportNumber; }
            set
            {
                CheckPassportDataForCorrectness(value, 6);
                _passportNumber = value;
            }
        }

        /// <summary>
        /// Серия паспорта
        /// </summary>
        public string PassportSerial
        {
            get { return _passportSerial; }
            set
            {
                CheckPassportDataForCorrectness(value, 4);
                _passportSerial = value;
            }
        }

        /// <summary>
        /// Место работы
        /// </summary>
        public string PlaceOfWork
        {
            get { return _placeOfWork; }
            set { _placeOfWork = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Проверить серию/номер паспорта на корректность
        /// </summary>
        /// <param name="value">Серия/номер паспорта</param>
        /// <param name="digitsAmount">Количество цифр в серии/номере</param>
        private void CheckPassportDataForCorrectness(string value, 
            byte digitsAmount)
        {
            if (value.Length != digitsAmount)
            {
                throw new ArgumentException(
                    $"{nameof(value)} must contain exactly 4 " +
                    "digits (6217, 0003, 0991 etc.)!");
            }

            if (!int.TryParse(value, out _))
            {
                throw new FormatException(
                    $"{nameof(value)} must be numeric!");
            }
        }

        /// <summary>
        /// Сформировать информацию о взрослом
        /// </summary>
        /// <returns>Информация о взрослом</returns>
        public override string FormInfoAboutPerson()
        {
            var infoAboutPerson = base.FormInfoAboutPerson() +
                $"\nСерия паспорта: {PassportSerial}\nНомер паспорта: " +
                $"{PassportNumber}\nСемейное положение: {MaritalStatus}";

            if (MaritalStatus == MaritalStatus.Married)
            {
                infoAboutPerson += $"\nПартнер: {Partner.Firstname} " +
                    $"{Partner.Lastname}";
            }

            infoAboutPerson += "\nМесто работы: ";

            if (string.IsNullOrEmpty(PlaceOfWork))
            {
                infoAboutPerson += "Безработный";
            }
            else
            {
                infoAboutPerson += PlaceOfWork;
            }

            return infoAboutPerson;
        }

        /// <summary>
        /// Выкурить сигарету
        /// </summary>
        /// <returns>Выкуривает сигарету</returns>
        public string Smoke()
        {
            return $"{FormShortInfoAboutPerson()} выкуривает сигарету.";
        }

        public override string WhoAmI()
        {
            return "I'm an adult.";
        }

        #endregion
    }
}
