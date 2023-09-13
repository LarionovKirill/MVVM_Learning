using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Model
{
    /// <summary>
    /// Класс контакта
    /// </summary>
    class Contact
    {
        /// <summary>
        /// Имя человека в контакте пользователя.
        /// </summary>
        private string _name;

        /// <summary>
        /// Электронный адрес человека в контакте пользователя.
        /// </summary>
        private string _email;

        /// <summary>
        /// Номер человека в контакте пользователя.
        /// </summary>
        private string _number;

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
                Services.ContactValidator.AssertName(value);
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
                Services.ContactValidator.AssertNumber(value);
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
                Services.ContactValidator.AssertEmail(value);
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
    }
}
