using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using Contacts.Model;
using Contacts.Model.Services;
using System.Collections.ObjectModel;

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
        /// Поле обработчика команды добавления.
        /// </summary>
        private RelayCommand _addCommand;

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        private RelayCommand _applyCommand;

        /// <summary>
        /// Поле видимости.
        /// </summary>
        private bool _isVisible;

        /// <summary>
        /// Поле активности кнопок.
        /// </summary>
        private bool _isEnabled;

        /// <summary>
        /// Поле активности кнопок.
        /// </summary>
        private bool _readOnly = true;

        /// <summary>
        /// Выбраный элемент
        /// </summary>
        private Contact _selectedItem;

        /// <summary>
        /// Экземпляр контакта.
        /// </summary>
        public Contact Contact { get; set; } = new Contact();

        /// <summary>
        /// Свойство массива контактов.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; }
            = ContactSerializer.LoadContact();

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
                            Contacts.Add(Contact);
                            ContactSerializer.SaveContact(Contacts);
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
                    }));
            }
        }

        /// <summary>
        /// Свойство автодобавления контакта для привязки его ко View.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(obj =>
                    {
                        var contact = ContactGenerator.GenerateContact();
                        Name = contact.Name;
                        Email = contact.Email;
                        Number = contact.Number;
                        IsVisible = true;
                        IsEnabled = false;
                        ReadOnly = false;
                        Contacts[Contacts.Count-1].Name = "Anton";
                        OnPropertyChanged("");
                    }));
            }
        }


        /// <summary>
        /// Свойство подтверждения контакта для привязки его ко View.
        /// </summary>
        public RelayCommand ApplyCommand
        {
            get
            {
                return _applyCommand ??
                    (_applyCommand = new RelayCommand(obj =>
                    {
                        IsVisible = true;
                    }));
            }
        }

        /// <summary>
        /// Свойство подтверждения контакта для привязки его ко View.
        /// </summary>
        public RelayCommand Test
        {
            get
            {
                return _applyCommand ??
                    (_applyCommand = new RelayCommand(obj =>
                    {
                        int a = 0;
                    }));
            }
        }

        /// <summary>
        /// Свойство видимости для привязки.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        /// <summary>
        /// Свойство видимости для привязки.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        /// <summary>
        /// Свойство видимости для привязки.
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        public Contact SelectedItem
        {
            get 
            { 
                return _selectedItem; 
            }
            set
            {
                _selectedItem = value;
                Name = _selectedItem.Name;
                Number = _selectedItem.Number;
                Email = _selectedItem.Email;
                OnPropertyChanged(nameof(SelectedItem));
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
