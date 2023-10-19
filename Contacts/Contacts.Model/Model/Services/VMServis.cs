using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contacts.Model.Model.Services
{
    /// <summary>
    /// Класс с вспомогательными методами MainVM
    /// </summary>
    public static class VMServis
    {
        /// <summary>
        /// Метод находит индекс в списке контактов.
        /// </summary>
        /// <param name="contactsList">Список контактов.</param>
        /// <param name="findContact">Искомый контакт.</param>
        /// <returns>Индекс в случае нахождения. В случае неудачи возвращается -1.</returns>
        public static int GetCurrentIndex(ObservableCollection<Contact> contactsList, Contact findContact)
        {
            for (var index = 0; index < contactsList.Count; index++)
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
        public static Dictionary<string, string> GenerateBaseDictianory()
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
        public static bool ContactEquals(Contact contact1, Contact contact2)
        {
            var emailEqual = contact1.Email == contact2.Email;
            var phoneEqual = contact1.Number == contact2.Number;
            var nameEqual = contact1.Name == contact2.Name;
            return emailEqual && phoneEqual && nameEqual;
        }
    }
}
