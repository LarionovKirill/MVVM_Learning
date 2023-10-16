﻿using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using Contacts.Model;
using Contacts.Model.Services;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Collections.Generic;

namespace Contacts.ViewModel
{
    class MainVM : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Поле обработчика команды сохранения.
        /// </summary>
        private RelayCommand _saveCommand;

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        private RelayCommand _addCommand;

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        private RelayCommand _applyCommand;

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        private RelayCommand _editCommand;

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        public Dictionary<string, string> _errorCollection = GenerateBaseDictianory();

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
        /// Индекс выбранного контакта.
        /// </summary>
        private int SelectedIndex { get; set; }

        /// <summary>
        /// Выбраный элемент
        /// </summary>
        private Contact _selectedItem = new Contact();

        /// <summary>
        /// Поле списка контактов.
        /// </summary>
        private ObservableCollection<Contact> _contactsList = ContactSerializer.LoadContact();

        /// <summary>
        /// Копия экземпляра контакта.
        /// </summary>
        public Contact CloneContact { get; set; } = new Contact();

        /// <summary>
        /// Флаг редактирования контакта.
        /// </summary>
        public bool EditMode { get; set; } = false;

        /// <summary>
        /// Свойство цвета фона имени.
        /// </summary>
        private Brush _nameColor;

        /// <summary>
        /// Свойство цвета фона Email.
        /// </summary>
        private Brush _emailColor;

        /// <summary>
        /// Свойство цвета фона номера.
        /// </summary>
        private Brush _numberColor;

        /// <summary>
        /// Свойство работы кнопки.
        /// </summary>
        private bool _isEnabledButton = true;

        /// <summary>
        /// Свойство массива контактов.
        /// </summary>
        public ObservableCollection<Contact> ContactsList
        {
            get
            {
                return _contactsList;
            }
            set
            {
                _contactsList = value;
                OnPropertyChanged(nameof(ContactsList));
            }
        }
        
        /// <summary>
        /// Свойство словаря ошибок.
        /// </summary>
        public Dictionary<string, string> ErrorCollection
        {
            get
            {
                return _errorCollection;
            }
            set
            {
                _errorCollection = value;
                OnPropertyChanged("ErrorCollection");
            }
        }

        /// <summary>
        /// Свойство выбранного контакта.
        /// </summary>
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
                    Name = SelectedItem.Name;
                    Number = SelectedItem.Number;
                    Email = SelectedItem.Email;
                    IsEnabled = true;
                    CloneContact = (Contact)SelectedItem.Clone();
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private string _test;

        public string Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }


        /// <summary>
        /// Свойство отображаемого имени.
        /// </summary>
        public string Name
        {
            get
            {
                if (!EditMode)
                {
                    return SelectedItem.Name;
                }
                else
                {
                    return CloneContact.Name;
                }
            }
            set
            {
                if (!EditMode)
                {
                    SelectedItem.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
                else
                {
                    CloneContact.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Свойство отображаемого номера.
        /// </summary>
        public string Number
        {
            get
            {
                if (!EditMode)
                {
                    return SelectedItem.Number;
                }
                else
                {
                    return CloneContact.Number;
                }
            }
            set
            {
                if (!EditMode)
                {
                    SelectedItem.Number = value;
                    OnPropertyChanged(nameof(Number));
                }
                else
                {
                    CloneContact.Number = value;
                    OnPropertyChanged(nameof(Number));
                }
            }
        }

        /// <summary>
        /// Свойство отображаемого Email.
        /// </summary>
        public string Email
        {
            get
            {
                if (!EditMode)
                {
                    return SelectedItem.Email;
                }
                else
                {
                    return CloneContact.Email;
                }
            }
            set
            {
                if (!EditMode)
                {
                    SelectedItem.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
                else
                {
                    CloneContact.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
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
        /// Свойство включения кнопки.
        /// </summary>
        public bool IsEnabledButton
        {
            get
            {
                return _isEnabledButton;
            }
            set
            {
                _isEnabledButton = value;
                OnPropertyChanged("IsEnabledButton");
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

        public Brush NameColor
        {
            get
            {
                return _nameColor;
            }
            set
            {
                _nameColor = value;
                OnPropertyChanged(nameof(NameColor));
            }
        }

        public Brush NumberColor
        {
            get
            {
                return _numberColor;
            }
            set
            {
                _numberColor = value;
                OnPropertyChanged(nameof(NumberColor));
            }
        }


        public Brush EmailColor
        {
            get
            {
                return _emailColor;
            }
            set
            {
                _emailColor = value;
                OnPropertyChanged(nameof(EmailColor));
            }
        }


        /// <summary>
        /// Свойство команды сохранения.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (!EditMode)
                            {
                                ContactsList.Add(SelectedItem);
                                ContactSerializer.SaveContact(ContactsList);
                                MessageBox.Show("Данные успешно сохранены.", "Сохранение",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                EditMode = false;
                                SelectedItem = (Contact)CloneContact.Clone();
                                ContactsList[SelectedIndex] = CloneContact;
                                ContactSerializer.SaveContact(ContactsList);
                                MessageBox.Show("Данные успешно изменены.", "Изменение",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            EditModeOff();
                        }
                        catch
                        {
                            MessageBox.Show("Введите вверное значение.","Сообщение об ошибке",
                                MessageBoxButton.OK, MessageBoxImage.Error);
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
                        SelectedItem = contact;
                        ApplyMode();
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
                        EditModeOff();
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
                        var index = ContactsList.IndexOf(SelectedItem);
                        if (index == 0 && ContactsList.Count == 1)
                        {
                            SelectedItem = null;
                        }
                        else if (index == 0)
                        {
                            ContactsList.RemoveAt(index);
                            SelectedItem = ContactsList[0];
                        }
                        else if (index == ContactsList.Count - 1)
                        {
                            ContactsList.RemoveAt(index);
                            SelectedItem = ContactsList[index - 1];
                        }
                        else
                        {
                            ContactsList.RemoveAt(index);
                            SelectedItem = ContactsList[index];
                        }
                        ContactSerializer.SaveContact(ContactsList);
                    }));
            }
        }

        /// <summary>
        /// Свойство автодобавления контакта для привязки его ко View.
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand(obj =>
                    {
                        if (!EditMode)
                        {
                            EditModeOn();
                            EditMode = true;
                            CloneContact = (Contact)SelectedItem.Clone();
                        }
                        else
                        {
                            EditMode = false;
                            EditModeOff();
                        }
                    }));
            }
        }
        /// <summary>
        /// Свойство ошибки.
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Событие обрабатывает изменение полей контакта.
        /// </summary>
        /// <param name="name">Имя свойства контакта.</param>
        /// <returns>Имя ошибки.</returns>
        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "Name":
                        result = ContactValidator.AssertName(Name);
                        if (result != null)
                        {
                            NameColor = Brushes.Salmon;
                        }
                        else
                        {
                            NameColor = Brushes.White;
                        }
                        break;
                    case "Number":
                        result = ContactValidator.AssertNumber(Number);
                        if (result != null)
                        {
                            NumberColor = Brushes.Salmon;
                        }
                        else
                        {
                            NumberColor = Brushes.White;
                        }
                        break;
                    case "Email":
                        result = ContactValidator.AssertEmail(Email);
                        if (result != null)
                        {
                            EmailColor = Brushes.Salmon;
                        }
                        else
                        {
                            EmailColor = Brushes.White;
                        }
                        break;
                }
                if (ErrorCollection.ContainsKey(name) && result != null)
                {
                    ErrorCollection[name] = result;
                    IsEnabledButton = false;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(name, result);
                    IsEnabledButton = false;
                }
                else if (result == null)
                {
                    IsEnabledButton = true;
                    ErrorCollection[name] = "The data is correct";
                }

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

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
        private void ApplyMode()
        {
            IsVisible = true;
            ReadOnly = false;
        }

        /// <summary>
        /// Метод отключает кнопки(свойство IsEnabled), делает невидимым кнопку apply(IsVisible) 
        /// и делает поля доступные только на чтение.
        /// </summary>
        private void EditModeOff()
        {
            IsVisible = false;
            IsEnabled = true;
            ReadOnly = true;
            EditMode = false;
        }

        /// <summary>
        /// Открывает поля для возможности редактирования.
        /// </summary>
        private void EditModeOn()
        {
            IsVisible = true;
            ReadOnly = false;
            EditMode = true;
        }

        /// <summary>
        /// Метод находит индекс в списке контактов.
        /// </summary>
        /// <param name="contactsList">Список контактов.</param>
        /// <param name="findContact">Искомый контакт.</param>
        /// <returns>Индекс в случае нахождения. В случае неудачи возвращается -1.</returns>
        private int GetCurrentIndex(ObservableCollection<Contact> contactsList, Contact findContact)
        {
            for(var index = 0; index< contactsList.Count;index++)
            {
                if (ContactEquals(contactsList[index], findContact))
                {
                    return index;
                }
            }
            return -1;
        }

        /// <summary>
        /// Метод создает базовый словарь для ошибок.
        /// </summary>
        /// <returns>Словарь для ошибок.</returns>
        static private Dictionary<string, string> GenerateBaseDictianory()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Name", "The data is correct");
            dict.Add("Number", "The data is correct");
            dict.Add("Email", "The data is correct");
            return dict;
        }

        /// <summary>
        /// Метод проверяет равенство двух контактов.
        /// </summary>
        /// <param name="contact1">Первый контакт.</param>
        /// <param name="contact2">Второй контакт.</param>
        /// <returns>True в случае равенства, иначе false.</returns>
        private bool ContactEquals(Contact contact1, Contact contact2)
        {
            var emailEqual = contact1.Email == contact2.Email;
            var phoneEqual = contact1.Number == contact2.Number;
            var nameEqual = contact1.Name == contact2.Name;
            return emailEqual && phoneEqual && nameEqual;
        }

        /// <summary>
        /// Событие срабатывает при изменении данных контакта.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
