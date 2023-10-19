using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


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
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanged(nameof(Number));
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
                OnPropertyChanged(nameof(Email));
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

        /// <summary>
        ///  Метод клонирования объекта типа <see cref="Contact"/>.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public object Clone()
        {
            return new Contact(Name, Number, Email);
        }

        /// <summary>
        /// Событие срабатывает при изменении данных контакта.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод вызывает событие PropertyChanged при изменении параметров контакта.
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
