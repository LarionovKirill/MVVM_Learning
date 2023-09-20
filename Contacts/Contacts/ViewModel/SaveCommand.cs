using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Описывает метод сохранения контакта, реализуя стандратный 
    /// интерфейс <see cref="ICommand"/>.
    /// </summary>
    class SaveCommand : ICommand
    {
        /// <summary>
        /// Делегат описание действий метода.
        /// </summary>
        private Action<object> _action;

        /// <summary>
        /// Вызывается при условии, что метод может быть вополнен.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Определяет позможность выполнения метода.
        /// </summary>
        /// <param name="parametr">Параметр.</param>
        /// <returns>Only true.</returns>
        public bool CanExecute(object parametr)
        {
            return true;
        }

        /// <summary>
        /// Вызывает делегат выполнения метода.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void Execute(object parameter)
        {
            this._action(parameter);
        }
    }
}
