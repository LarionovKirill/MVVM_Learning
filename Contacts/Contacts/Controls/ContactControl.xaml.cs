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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var allowedCharacters = "0123456789 +-() .";
            if (!allowedCharacters.Contains(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
