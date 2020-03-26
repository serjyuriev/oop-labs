using System;

namespace PersonLib
{
    /// <summary>
    /// Ребенок
    /// </summary>
    public class Child : PersonBase
    {
        #region Constants

        /// <summary>
        /// Минимальный возраст ребенка
        /// </summary>
        public const int MINAGE = 0;

        /// <summary>
        /// Максимальный возраст ребенка
        /// </summary>
        public const int MAXAGE = 17;

        #endregion

        #region Fields

        /// <summary>
        /// Отец
        /// </summary>
        private Adult _father;

        /// <summary>
        /// Название детского сада или школы
        /// </summary>
        private string _kindergardenOrSchool;

        /// <summary>
        /// Мать
        /// </summary>
        private Adult _mother;

        #endregion

        #region Properties

        public override int Age 
        {
            get { return base.Age; }
            set
            {
                if (value < MINAGE || value > MAXAGE)
                {
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be from 0 to 17.");
                }

                base.Age = value;
            }
        }

        /// <summary>
        /// Отец
        /// </summary>
        public Adult Father
        {
            get { return _father; }
            set { _father = value; }
        }

        /// <summary>
        /// Название детского сада или школы
        /// </summary>
        public string KindergardenOrSchool
        {
            get { return _kindergardenOrSchool; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(
                        $"{nameof(value)} is null or empty!");
                }

                _kindergardenOrSchool = value;
            }
        }

        /// <summary>
        /// Мать
        /// </summary>
        public Adult Mother
        {
            get { return _mother; }
            set { _mother = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Сформировать информацию о ребенке
        /// </summary>
        /// <returns></returns>
        public override string FormInfoAboutPerson()
        {
            var infoAboutPerson = base.FormInfoAboutPerson() + 
                $"\nНазвание детского сада или школы: " +
                $"{KindergardenOrSchool}";

            if (Mother != null)
            {
                infoAboutPerson += $"\nМать:" +
                    $" {Mother.FormShortInfoAboutPerson()}";
            }

            if (Father != null)
            {
                infoAboutPerson += $"\nОтец:" +
                    $" {Father.FormShortInfoAboutPerson()}";
            }

            return infoAboutPerson;
        }

        /// <summary>
        /// Попытаться выкурить сигарету
        /// </summary>
        /// <returns>Не получится выкурить</returns>
        public string TryToSmoke()
        {
            return $"{FormShortInfoAboutPerson()} не может выкурить " +
                $"сигарету, так как ещё слишком молод.";
        }

        #endregion
    }
}
