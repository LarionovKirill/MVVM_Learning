using System.ComponentModel;
using System.Runtime.CompilerServices;
using Contacts.Model;

namespace Contacts.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Экземпляр контакта.
        /// </summary>
        public Contact Contact { get; set; } = new Contact();

        /// <summary>
        /// Возращает и задает имя контакта.
        /// Формат вводимых данных: Ivanov Ivan Ivanovich.
        /// </summary>
        public string Name
        {
            get
            {
                return Contact.Name;
            }
            set
            {
                Contact.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Возращает и задает номер контакта.
        /// Формат вводимых данных: 79224566545.
        /// </summary>
        public string Number
        {
            get
            {
                return Contact.Number;
            }
            set
            {
                Contact.Number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        /// <summary>
        /// Возращает и задает номер контакта.
        /// Формат вводимых данных: 79224566545.
        /// </summary>
        public string Email
        {
            get
            {
                return Contact.Email;
            }
            set
            {
                Contact.Email = value;
                OnPropertyChanged(nameof(Email));
            }
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
