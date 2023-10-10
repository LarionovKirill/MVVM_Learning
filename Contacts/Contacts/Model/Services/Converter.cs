using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Contacts.Model.Services
{
    class Converter : IValueConverter
    {
        /// <summary>
        /// Конвертирует bool значение в значение видимости.
        /// </summary>
        /// <param name="value">Значение, которое надо преобразовать.</param>
        /// <param name="targetType">Тип, к которому надо преобразовать.</param>
        /// <param name="parameter">Доп параметр</param>
        /// <param name="culture">Культура приложения.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Обратный конвертр
        /// </summary>
        /// <param name="value">Значение, которое надо преобразовать.</param>
        /// <param name="targetType">Тип к которому надо проебразовать</param>
        /// <param name="parameter">Доп параметр</param>
        /// <param name="culture">Культура приложения.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
    }
}
