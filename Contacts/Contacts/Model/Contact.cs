using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Contacts.Model
{
    /// <summary>
    /// Класс контакта
    /// </summary>
    public class Contact : INotifyPropertyChanged, ICloneable
    {
        /// <summary>
        /// Имя человека в контакте.
        /// </summary>
        private string _name;

        /// <summary>
        /// Номер человека в контакте.
        /// </summary>
        private string _number;

        /// <summary>
        /// Электронная почта человека в контакте.
        /// </summary>
        private string _email;

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        public Dictionary<string, string> ErrorCollection { get; private set; }
            = new Dictionary<string, string>();


        /// <summary>
        /// Свойство имени в контакте пользователя.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Свойство номера в контакте пользователя.
        /// </summary>
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }
        
        /// <summary>
        /// Свойство имени в контакте пользователя.
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="Contact"/> без параметров.
        /// </summary>
        public Contact()
        {

        }

        /// <summary>
        /// Конструктор класса <see cref="Contact"/> с параметрами.
        /// </summary>
        /// <param name="_name">Имя контакта.</param>
        /// <param name="_number">Номер контакта.</param>
        /// <param name="_email">Электронная почта контакта.</param>
        public Contact(string _name, string _number, string _email)
        {
            Name = _name;
            Number = _number;
            Email = _email;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return new Contact(Name, Number, Email);
        }
    }
}
