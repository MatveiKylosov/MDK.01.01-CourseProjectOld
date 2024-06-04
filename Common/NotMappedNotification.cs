using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace MDK._01._01_CourseProject.Common
{
    public abstract class NotMappedNotification : Notification
    {
        [Schema.NotMapped]
        private bool isEnable;
        [Schema.NotMapped]
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [Schema.NotMapped]
        public string IsEnableText
        {
            get
            {
                if (isEnable) return "Сохранить";
                else return "Изменить";
            }
        }

        [Schema.NotMapped]
        public abstract RelayCommand OnEdit { get; }

        [Schema.NotMapped]
        public abstract RelayCommand OnDelete { get; }
    }
}
