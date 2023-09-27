using System;
using System.IO;
using Newtonsoft.Json;

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
        /// <param name="contact">Переданный контакт.</param>
        public static void SaveContact(Contact contact)
        {
            var json = JsonConvert.SerializeObject(contact, Formatting.Indented);
            File.WriteAllText(DefaultPath, json);
        }

        /// <summary>
        /// Метод осуществялет загрузку контакта из Json-файла.
        /// </summary>
        /// <returns>Записанный контакт.</returns>
        public static Contact LoadContact()
        {
            var contact = new Contact();

            if (File.Exists(DefaultPath))
            {
                var jsonContent = File.ReadAllText(DefaultPath);
                contact = JsonConvert.DeserializeObject<Contact>(jsonContent);
            }

            return contact;
        }
    }
}
