using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MDK._01._01_CourseProject.Common
{
    /// <summary>
    /// Используется для уведомления об изменении свойства. Это необходимо для обновления представления (View), когда изменяется свойство модели представления (ViewModel). 
    /// </summary>
    public class Notification : INotifyPropertyChanged
    {
        /// <summary>
        /// Это событие, которое вызывается при изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// это метод, который вызывает событие PropertyChanged.
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string Property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
        }
    }
}
