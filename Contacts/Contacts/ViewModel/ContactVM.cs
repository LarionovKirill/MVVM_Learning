using System.Collections.ObjectModel;
using Contacts.Model;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Класс для сериализации и использования в VM
    /// </summary>
    class ContactVM 
    {
        /// <summary>
        /// Свойство массива контактов.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; } 
            = new ObservableCollection<Contact>();
    }
}
