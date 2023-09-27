using System;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Описывает метод загрузки контакта, реализуя стандратный 
    /// интерфейс <see cref="ICommand"/>.
    /// </summary>
    class LoadCommand : ICommand
    {

        /// <summary>
        /// Вызывается при условии, что метод может быть вополнен.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        /// <summary>
        /// Делегат описание действий метода.
        /// </summary>
        private Action<object> _action;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="LoadCommand"/>.
        /// </summary>
        /// <param name="execute">Описание логики команды.</param>
        public LoadCommand(Action<object> execute)
        {
            this._action = execute;
        }

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
