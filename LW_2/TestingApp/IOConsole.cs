using PersonLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingApp
{

    /// <summary>
    /// Ввод/вывод данных в консоль
    /// </summary>
    public static class IOConsole
    {
        /// <summary>
        /// Создать новый экземпляр класса Person
        /// с указанием необходимых полей с клавиатуры
        /// </summary>
        /// <returns></returns>
        public static Person CreateNewPerson()
        {
            var createdPerson = new Person();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    Console.Write($"{nameof(createdPerson.Firstname)}: ");
                    createdPerson.Firstname = Console.ReadLine();
                }),
                new Action(() =>
                {
                    Console.Write($"{nameof(createdPerson.Lastname)}: ");
                    createdPerson.Lastname = Console.ReadLine();
                }),
                new Action(() =>
                {
                    Console.Write($"{nameof(createdPerson.Age)} (0-122): ");
                    createdPerson.Age = int.Parse(Console.ReadLine());
                }),
                new Action(() =>
                {
                    Console.Write(
                        $"{nameof(createdPerson.Gender)} (Male/Female): ");
                    var buffer = Console.ReadLine();
                    buffer = buffer.ToLower();
                    buffer = buffer.First().ToString().ToUpper() +
                        buffer.Substring(1);
                    createdPerson.Gender = (Genders)Enum.Parse(
                        typeof(Genders), buffer);
                }),
            };
            actions.ForEach(SetValue);
            return createdPerson;
        }


        /// <summary>
        /// Получить пользовательский ввод и задать параметр
        /// </summary>
        /// <param name="action">Делегат с заданием параметра</param>
        public static void SetValue(Action action)
        {
            while (true)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine($"\n{argumentException.Message}\n");
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine($"\n{formatException.Message}\n");
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    Console.WriteLine(
                        $"\n{invalidOperationException.Message}\n");
                }
            }
        }
    }
}
