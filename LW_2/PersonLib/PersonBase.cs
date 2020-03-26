using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PersonLib
{
    /// <summary>
    /// Класс, описывающий человека.
    /// </summary>
    public abstract class PersonBase
    {
        #region Fields

        /// <summary>
        /// Имя
        /// </summary>
        private string _firstname;

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _lastname;

        /// <summary>
        /// Возраст
        /// </summary>
        private int _age;

        /// <summary>
        /// Пол
        /// </summary>
        private Genders _gender;

        #endregion

        #region Properties

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                CheckCorrectnessOfNames(value);
                _firstname = FirstLetterToUpper(value);
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                CheckCorrectnessOfNames(value);
                _lastname = FirstLetterToUpper(value);
            }
        }

        /// <summary>
        /// Возраст
        /// </summary>
        public virtual int Age
        {
            get { return _age; }
            set
            {
                if (value < 0 || value > 122)
                {
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be from 0 to 122.");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Пол
        /// </summary>
        public Genders Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Человек
        /// </summary>
        public PersonBase() { }

        /// <summary>
        /// Человек
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="gender">Пол</param>
        public PersonBase(string firstName, string lastName, byte age,
            Genders gender)
        {
            Firstname = firstName;
            Lastname = lastName;
            Age = age;
            Gender = gender;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Проверить имя/фамилию на корректность
        /// </summary>
        /// <param name="value">Параметр для проверки</param>
        private void CheckCorrectnessOfNames(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(
                    $"{nameof(value)} is null or empty.");
            }

            if (!IsNameCorrect(value))
            {
                throw new FormatException(
                    $"{nameof(value)} must contain only letters of " +
                    $"Russian or English alphabet.");
            }
        }

        /// <summary>
        /// Измененить первую букву в имени/фамилии на заглавную
        /// с учетом двойного имени/фамилии
        /// </summary>
        /// <param name="wordToUpdate">Строка для изменения</param>
        /// <returns>Обновленная строка</returns>
        private static string FirstLetterToUpper(string wordToUpdate)
        {
            string[] buffer = wordToUpdate.Split('-');
            wordToUpdate = null;

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = buffer[i].First().ToString().ToUpper() +
                    buffer[i].Substring(1);

                wordToUpdate += $"{buffer[i]}-";
            }

            return wordToUpdate.Substring(0, wordToUpdate.Length - 1);
        }

        /// <summary>
        /// Сформировать информацию о человеке
        /// </summary>
        /// <returns>Строка с информацией</returns>
        public virtual string FormInfoAboutPerson()
        {
            return $"Имя: {Firstname}\nФамилия: {Lastname}\n" +
                $"Возраст: {Age}\nПол: {Gender}";
        }

        /// <summary>
        /// Сформировать короткую информацию о человеке
        /// </summary>
        /// <returns>Строка с информацией</returns>
        public string FormShortInfoAboutPerson()
        {
            return $"{Firstname} {Lastname}";
        }

        /// <summary>
        /// Проверить параметр на соответствие
        /// </summary>
        /// <param name="input">Параметр для проверки</param>
        /// <returns>Параметр, приведенный к желаемому виду</returns>
        private static bool IsNameCorrect(string input)
        {
            var expressionForRegex = "(([А-Я]|[а-я]|[A-Z]|[a-z])+)";
            var regexForName = new Regex(
                $"^{expressionForRegex}((-)?){expressionForRegex}$");

            if (!regexForName.IsMatch(input))
            {
                return false;
            }

            return true;
        }

        public virtual string WhoAmI()
        {
            return "I'm a person.";
        }

        #endregion
    }
}