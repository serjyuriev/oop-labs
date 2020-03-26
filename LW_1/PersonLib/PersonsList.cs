using System;
using System.Collections.Generic;

namespace PersonLib
{
    /// <summary>
    /// Класс, описывающий список людей.
    /// </summary>
    public class PersonList
    {
        #region Fields

        /// <summary>
        /// Список людей
        /// </summary>
        private Person[] _persons;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает количество элементов в списке
        /// </summary>
        public int Length
        {
            get { return _persons.Length; }
        }

        #endregion

        #region Indexator

        /// <summary>
        /// Возвращает человека из списка по указанному индексу
        /// </summary>
        /// <param name="index">Индекс человека в списке</param>
        /// <returns></returns>
        public Person this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                {
                    throw new IndexOutOfRangeException(
                        $"Index must be non-negative and less than the " +
                        $"size of the collection.");
                }
                return _persons[index];
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор списка людей,
        /// осуществляющий инициализацию пустого массива
        /// </summary>
        public PersonList()
        {
            _persons = new Person[0];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Добавление нового человека в конец списка
        /// </summary>
        /// <param name="person">Человек</param>
        public void AddPerson(Person person)
        {
            var buffer = _persons;

            _persons = new Person[buffer.Length + 1];
            for (int i = 0; i < buffer.Length; i++)
            {
                _persons[i] = buffer[i];
            }
            _persons[buffer.Length] = person;
        }

        //TODO: AddRange (Переименовал)
        /// <summary>
        /// Добавление новых людей в конец списка
        /// </summary>
        /// <param name="persons">Массив персон</param>
        public void AddRange(Person[] persons)
        {
            foreach (Person person in persons)
            {
                AddPerson(person);
            }
        }

        /// <summary>
        /// Полная очистка списка
        /// </summary>
        public void Clear()
        {
            _persons = new Person[0];
        }

        /// <summary>
        /// Удаление человека из списка по совпадению с переданным
        /// экземпляром класса Person
        /// </summary>
        /// <param name="person">Эксземпляр класса Person</param>
        public void DeletePerson(Person person)
        {
            DeletePersonByIndex(GetPerson(person));
        }

        /// <summary>
        /// Удаление человека из списка по индексу
        /// </summary>
        /// <param name="indexOfPerson">Индекс человека в списке</param>
        public void DeletePersonByIndex(int indexOfPerson)
        {
            if (indexOfPerson < 0 || indexOfPerson > _persons.Length - 1)
            {
                throw new IndexOutOfRangeException();
            }

            var buffer = _persons;
            uint counter = 0;

            _persons = new Person[buffer.Length - 1];
            for (int i = 0; i < buffer.Length; i++)
            {
                if (i != indexOfPerson)
                {
                    _persons[counter] = buffer[i];
                    counter++;
                }
            }
        }

        /// <summary>
        /// Поиск человека в списке по переданному
        /// экземпляру объекта класса Person
        /// </summary>
        /// <param name="person">Экземпляр объекта класса Person</param>
        /// <returns>
        /// Индекс человека в списке
        /// </returns>
        public int GetPerson(Person person)
        {
            for (int i = 0; i < _persons.Length; i++)
            {
                if (_persons[i] == person)
                {
                    return i;
                }
            }

            throw new KeyNotFoundException("There is no such person in " +
                "this list.");
        }

        #endregion
    }
}
