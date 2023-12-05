
namespace Contacts.Model.Services
{
    /// <summary>
    /// Статический класс валидации контакта.
    /// </summary>
    public static class ContactValidator
    {
        /// <summary>
        /// Проверяет имя на правильность ввода.
        /// </summary>
        /// <param name="name">Переданное имя.</param>
        /// <returns>Имя ошибки для отображения.</returns>
        public static string AssertName(string name)
        {
            if (name != null)
            {
                if (name.Length == 0)
                {
                    return "Name cannot be empty.";
                }
                else if (name.Length > 100)
                {
                    return "Name cannot be longer then 100 symbols";
                }
            }
            return null;
        }

        /// <summary>
        /// Проверяет номер на правильность ввода.
        /// </summary>
        /// <param name="number">Преданный номер.</param>
        /// <returns>Имя ошибки для отображения.</returns>
        public static string AssertNumber(string number)
        {
            if (number != null)
            {
                var allowedCharacters = "0123456789 +-() .";
                if (number.Length > 100)
                {
                    return "Number cannot be longer then 100 symbols";
                }
                else if (number.Length == 0)
                {
                    return "Number cannot be empty";
                }
                else
                {
                    foreach (var digit in number)
                    {
                        if (!allowedCharacters.Contains(digit.ToString()))
                        {
                            return "Number can contain only [0123456789 +-() .]";
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Проверяет Email на правильность ввода.
        /// </summary>
        /// <param name="email">Преданный email.</param>
        /// <returns>Имя ошибки для отображения.</returns>
        public static string AssertEmail(string email)
        {
            if (email != null)
            {
                if (!email.Contains("@"))
                {
                    return "Email must contain <@> ";
                }
                else if (email.Length > 100)
                {
                    return "Email cannot be longer then 100 symbols";
                }
                else if (email.Length == 0)
                {
                    return "Email cannot be empty";
                }
            }
            return null;
        }
    }
}
