using System;

namespace Contacts.Model.Services
{
    /// <summary>
    /// Класс для создания случайных контактов.
    /// </summary>
    static public class ContactGenerator
    {
        /// <summary>
        /// Список имен для генерации.
        /// </summary>
        private static string[] _names = { "Ivan", "Sergey", "Danil", "Georgy" };
       
        /// <summary>
        /// Список фамилий для генерации.
        /// </summary>
        private static string[] _surnames = { "Ivanov", "Petrov", "Sidorov", "Markov" };
        
        /// <summary>
        /// Список отчеств для генерации.
        /// </summary>
        private static string[] _patronymic = { "Ivanovich", "Petrovich", "Igorevich", "Vasilievich" };

        /// <summary>
        /// Список доменов для генерации.
        /// </summary>
        private static string[] _domains = { "gmail.com", "mail.ru", "yandex.ru" };

        /// <summary>
        /// Объект класса <see cref="Random"/> для генерации случайного числа.
        /// </summary>
        private static Random _random = new Random();

        /// <summary>
        /// Метод генерации полного имени вида: Ivanov Ivan Ivanovoch.
        /// </summary>
        /// <returns>Полное имя.</returns>
        private static string GenerateName()
        {
            string _fullName = $"{_surnames[_random.Next(_surnames.Length)]} " +
                $"{_names[_random.Next(_names.Length)]} " +
                $"{_patronymic[_random.Next(_patronymic.Length)]}";

            return _fullName;
        }

        /// <summary>
        /// Метод генерации номера телефона вида: 793265478923.
        /// </summary>
        /// <returns>Номер телефона.</returns>
        private static string GeneratePhoneNumber()
        {
            string _number = "8" + 
                _random.Next(900, 1000).ToString() + 
                _random.Next(1000000, 10000000).ToString();

            return _number;
        }

        /// <summary>
        /// Метод генерации электронного адреса вида: Ivanov@gmail.com.
        /// </summary>
        /// <returns>Готовый Email.</returns>
        private static string GenerateEmail()
        {
            string _email = $"{_names[_random.Next(_names.Length)]}" +
                $"@{_domains[_random.Next(_domains.Length)]}";

            return _email;
        }

        /// <summary>
        /// Метод генерирует готовый объект класса <see cref="Contact"/>.
        /// </summary>
        /// <returns>Объект класса <see cref="Contact"/>.</returns>
        public static Contact GenerateContact()
        {
            var _contact = new Contact();
            _contact.Email = GenerateEmail();
            _contact.Name = GenerateName();
            _contact.Number = GeneratePhoneNumber();

            return _contact;
        }
    }
}
