using System;
using System.Windows.Input;

namespace MDK._01._01_CourseProject.Common
{
    /// <summary>
    /// Используется для обработки команд в MVVM. 
    /// Команда - это действие или набор действий, которые выполняются при определенном пользовательском взаимодействии, например, при нажатии кнопки.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// это действие, которое должно быть выполнено.
        /// </summary>
        private Action<object> execute;
        /// <summary>
        ///  это функция, которая определяет, может ли команда быть выполнена.
        /// </summary>
        private Func<object, bool> canExecute;
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        /// <summary>
        /// это событие, которое вызывается, когда изменяется возможность выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        /// <summary>
        /// это метод, который проверяет, может ли команда быть выполнена.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        /// <summary>
        /// это метод, который выполняет команду.
        /// </summary>
        public void Execute(object parameter) =>
            this.execute(parameter);
    }
}
