using System;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class RelayCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Делегат описание действий метода.
        /// </summary>
        private Action<object> _action;

        /// <summary>
        /// Создаёт экземпляр класса.
        /// </summary>
        /// <param name="execute">Описание логики команды.</param>
        public RelayCommand(Action<object> execute)
        {
            this._action = execute;
        }

        public void Execute(object parameter)
        {
            this._action(parameter);
        }
    }
}
