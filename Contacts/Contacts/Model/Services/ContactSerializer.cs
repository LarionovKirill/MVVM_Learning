using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Contacts.Model.Services
{
    /// <summary>
    /// Класс сериализации контакта.
    /// </summary>
    public class ContactSerializer
    {

        private string DefaultPath
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
        public void SaveContact(Contact contact)
        {
            var json = JsonConvert.SerializeObject(contact, Formatting.Indented);
            File.WriteAllText(DefaultPath, json);
        }

        /// <summary>
        /// Метод осуществялет загрузку контакта из Json-файла.
        /// </summary>
        /// <returns>Записанный контакт.</returns>
        public Contact LoadContact()
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
