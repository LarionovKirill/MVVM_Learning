using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Model.Services
{
    /// <summary>
    /// Статический класс валидации контакта.
    /// </summary>
    public static class ContactValidator
    {
        /// <summary>
        /// Метод проверят, является ли имя контакта пустой строкой.
        /// </summary>
        /// <param name="name">Имя контакта.</param>
        public static void AssertName(string name)
        {
            if (name.Length == 0)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Метод проверяет, является ли переданный номер 11 значным числом.
        /// </summary>
        /// <param name="number">Переданный номер.</param>
        public static void AssertNumber(string number)
        {
            if (number.Length != 11)
            {
                throw new ArgumentException();
            }
            else
            {
                foreach (var digit in number)
                {
                    if (!char.IsDigit(digit))
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }

        /// <summary>
        /// Метод проверяет, является ли переданная строка адресом электронной почты.
        /// </summary>
        /// <param name="email">Переданный адрес.</param>
        public static void AssertEmail(string email)
        {
            if (!email.Contains("@"))
            {
                throw new ArgumentException();
            }
        }
    }
}
