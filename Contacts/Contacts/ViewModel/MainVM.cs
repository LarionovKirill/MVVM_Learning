using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using Contacts.Model;
using Contacts.Model.Services;

namespace Contacts.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Поле обработчика команды загрузки.
        /// </summary>
        private LoadCommand _loadCommand;

        /// <summary>
        /// Поле обработчика команды сохранения.
        /// </summary>
        private SaveCommand _saveCommand;

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
        /// Формат вводимых данных: something@gmail.com.
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
        /// Свойство команды сохранения.
        /// </summary>
        public SaveCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new SaveCommand(obj =>
                    {
                        try
                        {
                            ContactValidator.AssertEmail(Contact.Email);
                            ContactValidator.AssertNumber(Contact.Number);
                            ContactValidator.AssertName(Contact.Name);
                            ContactSerializer.SaveContact(Contact);
                            MessageBox.Show("Данные успешно сохранены.");
                        }
                        catch
                        {
                            MessageBox.Show("Введите вверное значение.");
                        }
                    }));
            }
        }

        /// <summary>
        /// Свойство команды загрузки.
        /// </summary>
        public LoadCommand LoadCommand
        {
            get
            {
                return _loadCommand ??
                    (_loadCommand = new LoadCommand(obj =>
                    {
                        var contact = ContactSerializer.LoadContact();
                        Name = contact.Name;
                        Number = contact.Number;
                        Email = contact.Email;
                    }));
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
