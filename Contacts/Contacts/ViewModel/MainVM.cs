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
                            IsVisible = false;
                            ReadOnly = true;
                            IsEnabled = true;
                        }
                        catch
                        {
                            MessageBox.Show("Введите вверное значение.");
                        }
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
                        IsEnabled = true;
                        ReadOnly = false;
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
                        IsVisible = false;
                        IsEnabled = true;
                        ReadOnly = true;
                    }));
            }
        }

        /// <summary>
        /// Свойство подтверждения контакта для привязки его ко View.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return _applyCommand ??
                    (_applyCommand = new RelayCommand(obj =>
                    {
                        var index = Contacts.IndexOf(SelectedItem);
                        if (index == 0 && Contacts.Count == 1)
                        {
                            SelectedItem = null;
                        }
                        else if (index == 0)
                        {
                            Contacts.RemoveAt(index);
                            SelectedItem = Contacts[0];
                        }
                        else if (index == Contacts.Count - 1)
                        {
                            Contacts.RemoveAt(index);
                            SelectedItem = Contacts[index - 1];
                        }
                        else
                        {
                            Contacts.RemoveAt(index);
                            SelectedItem = Contacts[index];
                        }
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
                if (_selectedItem != null)
                {
                    Name = _selectedItem.Name;
                    Number = _selectedItem.Number;
                    Email = _selectedItem.Email;
                }
                ActivateButtons();
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

        /// <summary>
        /// Активирует кнопки и работу с текстом.
        /// </summary>
        private void ActivateButtons()
        {
            IsEnabled = true;
            IsVisible = true;
            ReadOnly = false;
        }
    }
}
