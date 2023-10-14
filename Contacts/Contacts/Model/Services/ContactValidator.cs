using System;

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
        private static void AssertName(string name)
        {
            if (name.Length == 0 || name.Length>100)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Метод проверяет, является ли переданный номер 11 значным числом.
        /// </summary>
        /// <param name="number">Переданный номер.</param>
        private static void AssertNumber(string number)
        {
            var allowedCharacters = "0123456789 +-() .";
            if (number.Length > 100)
            {
                throw new ArgumentException();
            }
            else
            {
                foreach (var digit in number)
                {
                    if (!allowedCharacters.Contains(digit.ToString()))
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
        private static void AssertEmail(string email)
        {
            if (!email.Contains("@") || email.Length>100)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Метод проверяет правильность заполнения контакта.
        /// </summary>
        /// <param name="contact"></param>
        public static void AssertContact(Contact contact)
        {
            AssertEmail(contact.Email);
            AssertNumber(contact.Number);
            AssertName(contact.Name);
        }
    }
}
