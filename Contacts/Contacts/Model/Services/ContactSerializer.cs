using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Contacts.ViewModel;

namespace Contacts.Model.Services
{
    /// <summary>
    /// Класс сериализации контакта.
    /// </summary>
    public static class ContactSerializer
    {
        /// <summary>
        /// Метод возвращает путь для записи или чтения JSON файла.
        /// </summary>
        /// <returns>Путь к файлу.</returns>
        private static string DefaultPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\contacts.json"; 
            }
        }

        /// <summary>
        /// Метод серилиазует контакт в json файл.
        /// </summary>
        /// <param name="contacts">Переданный список контактов.</param>
        public static void SaveContact(ObservableCollection<Contact> contacts)
        {
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(DefaultPath, json);
        }

        /// <summary>
        /// Метод осуществялет загрузку контакта из Json-файла.
        /// </summary>
        /// <returns>Записанный контакт.</returns>
        public static ObservableCollection<Contact> LoadContact()
        {
            var data = new ObservableCollection<Contact>();

            if (File.Exists(DefaultPath))
            {
                var jsonContent = File.ReadAllText(DefaultPath);
                data = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(jsonContent);
            }

            return data;
        }
    }
}
