using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Contacts.Controls
{
    /// <summary>
    /// Логика взаимодействия для ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Событие отрабатывает ввод допустимых символов.
        /// </summary>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var allowedCharacters = "0123456789 +-() .";
            if (!allowedCharacters.Contains(e.Text))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Событие отрабатывает вставку допустимых символов.
        /// </summary>
        private void TextBoxPaste(object sender, DataObjectPastingEventArgs e)
        {
            var allowedCharacters = "0123456789 +-() .";
            string input = (string)e.DataObject.GetData(typeof(string));
            foreach (var symbol in input)
            {
                if (!allowedCharacters.Contains(symbol.ToString()))
                {
                    e.CancelCommand();
                }
            }    
        }
    }
}
