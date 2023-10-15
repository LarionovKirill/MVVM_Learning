using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using Contacts.Model;
using Contacts.Model.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Contacts.ViewModel
{
    class MainVM : INotifyPropertyChanged
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
        /// Возращает и задает имя контакта.
        /// Формат вводимых данных: Ivanov Ivan Ivanovich.
        /// </summary>
        public string Name
        {
            get
            {
                return SelectedItem.Name;
            }
            set
            {
                SelectedItem.Name = value;
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
                return SelectedItem.Number;
            }
            set
            {
                SelectedItem.Number = value;
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
                return SelectedItem.Email;
            }
            set
            {
                SelectedItem.Email = value;
                OnPropertyChanged(nameof(Email));
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

        /// <summary>
        /// Свойство для выбранного контакта
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
                    Name = _selectedItem.Name;
                    Number = _selectedItem.Number;
                    Email = _selectedItem.Email;
                    IsEnabled = true;
                }
                if (EditMode)
                {
                    CloneContact = (Contact)_selectedItem.Clone();
                    SelectedIndex = GetCurrentIndex(ContactsList, value);
                }
                OnPropertyChanged(nameof(SelectedItem));
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
                                ContactsList[SelectedIndex] = SelectedItem;
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
                        EditModeOn();
                        EditMode = true;
                        CloneContact = SelectedItem;
                        SelectedItem = (Contact)CloneContact.Clone();
                    }));
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
