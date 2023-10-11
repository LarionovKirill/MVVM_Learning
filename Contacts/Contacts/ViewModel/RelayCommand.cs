using System;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class RelayCommand : ICommand
    {

        /// <summary>
        /// Собитие происходит когда происходит изменение возможности выполнения события.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Метод возвращается возмодность выполнения события.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <returns>True, если может выполниться, иначе false.</returns>
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

        /// <summary>
        /// Выполняет логику команды
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void Execute(object parameter)
        {
            this._action(parameter);
        }
    }
}
