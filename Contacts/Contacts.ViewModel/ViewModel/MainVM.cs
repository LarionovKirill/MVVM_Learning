using System.ComponentModel;
using System.Windows;
using Contacts.Model;
using System.Windows.Input;
using Contacts.Model.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using Contacts.Model.Services;

namespace Contacts.ViewModel
{
    public class MainVM : ObservableObject, IDataErrorInfo
    {
        public MainVM()
        {
            SaveCommand = new RelayCommand(SaveContactCommand);
            AddCommand = new RelayCommand(AddContactCommand);
            ApplyCommand = new RelayCommand(ApplyContactCommand);
            EditCommand = new RelayCommand(EditContactCommand);
            DeleteCommand = new RelayCommand(DeleteContactCommand);
        }

        /// <summary>
        /// Поле обработчика команды сохранения.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        public ICommand ApplyCommand { get; }

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        public ICommand EditCommand { get; }

        /// <summary>
        /// Поле обработчика команды добавления.
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        private Dictionary<string, string> _errorCollection = GenerateBaseDictianory();

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
                SetProperty(ref _errorCollection, value);
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
                SetProperty(ref _selectedItem, value);
                if (_selectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Number = SelectedItem.Number;
                    Email = SelectedItem.Email;
                    IsEnabled = true;
                    CloneContact = (Contact)SelectedItem.Clone();
                    SelectedIndex = GetCurrentIndex(ContactsList, CloneContact);
                }
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
                    var name = string.Empty;
                    SetProperty(ref name, value);
                    SelectedItem.Name = value;
                    CloneContact.Name = value;
                }
                else
                {
                    var name = string.Empty;
                    CloneContact.Name = value;
                    SetProperty(ref name, value);
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
                    var number = string.Empty;
                    SetProperty(ref number, value);
                    SelectedItem.Number = value;
                }
                else
                {
                    var number = string.Empty;
                    CloneContact.Number = value;
                    SetProperty(ref number, value);
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
                    var email = string.Empty;
                    SetProperty(ref email, value);
                    SelectedItem.Email = value;
                }
                else
                {
                    var email = string.Empty;
                    CloneContact.Email = value;
                    SetProperty(ref email, value);
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
                SetProperty(ref _isVisible, value);
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
                SetProperty(ref _isEnabledButton, value);
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
                SetProperty(ref _isEnabled, value);
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
                SetProperty(ref _readOnly, value);
            }
        }

        /// <summary>
        /// Свойство цвета Textbox имени.
        /// </summary>
        public Brush NameColor
        {
            get
            {
                return _nameColor;
            }
            set
            {
                SetProperty(ref _nameColor, value);
            }
        }

        /// <summary>
        /// Свойство цвета Textbox номера.
        /// </summary>
        public Brush NumberColor
        {
            get
            {
                return _numberColor;
            }
            set
            {
                SetProperty(ref _numberColor, value);
            }
        }

        /// <summary>
        /// Свойство цвета Textbox Email.
        /// </summary>
        public Brush EmailColor
        {
            get
            {
                return _emailColor;
            }
            set
            {
                SetProperty(ref _emailColor, value);
            }
        }


        /// <summary>
        /// Свойство команды сохранения.
        /// </summary>
        public void SaveContactCommand()
        {
            try
            {
                if (!EditMode)
                {
                    ContactsList.Add(SelectedItem);
                    ContactSerializer.SaveContact(ContactsList);
                    SelectedIndex = GetCurrentIndex(ContactsList, SelectedItem);
                    MessageBox.Show("Данные успешно сохранены.", "Сохранение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    EditMode = false;
                    ContactsList[SelectedIndex] = CloneContact;
                    SelectedItem = CloneContact;
                    ContactSerializer.SaveContact(ContactsList);
                    MessageBox.Show("Данные успешно изменены.", "Изменение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                EditModeOff();
            }
            catch
            {
                MessageBox.Show("Введите вверное значение.", "Сообщение об ошибке",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Свойство автодобавления контакта для привязки его ко View.
        /// </summary>
        public void AddContactCommand()
        {
            var contact = ContactGenerator.GenerateContact();
            SelectedItem = contact;
            ApplyMode();
        }


        /// <summary>
        /// Свойство подтверждения контакта для привязки его ко View.
        /// </summary>
        public void ApplyContactCommand()
        {
            EditModeOff();
        }

        /// <summary>
        /// Свойство подтверждения контакта для привязки его ко View.
        /// </summary>
        public void DeleteContactCommand()
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
        }

        /// <summary>
        /// Свойство автодобавления контакта для привязки его ко View.
        /// </summary>
        public void EditContactCommand()
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
    }
}
