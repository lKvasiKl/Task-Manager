using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class AddTaskButtonViewModel : ObservableObject
    {
        private TaskViewModel _taskVM;

        public TaskViewModel TaskVM
        {
            get 
            { 
                return _taskVM;
            }
            set 
            {
                _taskVM = value;
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(NotificationDate));
                OnPropertyChanged(nameof(Time));
            }
        }

        public RelayCommand DeselectDateCommand { get; set; }

        public string Description
        {
            get
            {
                return _taskVM.Description;
            }
            set
            {
                _taskVM.Description = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Date
        {
            get
            {
                return _taskVM.Date;
            }
            set
            {
                _taskVM.Date = value;
                OnPropertyChanged();
            }
        }

        public DateTime? NotificationDate
        {
            get
            {
                return _taskVM.NotificationDate;
            }
            set
            {
                _taskVM.NotificationDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Time
        {
            get
            {
                return _taskVM.Time;
            }
            set
            {
                _taskVM.Time = value;
                OnPropertyChanged();
            }
        }

        public AddTaskButtonViewModel()
        {
            _taskVM = new TaskViewModel();
            Date = null;
            NotificationDate = null;
            Time = null;

            DeselectDateCommand = new RelayCommand(o =>
            {
                Date = null;
                NotificationDate = null;
                Time = null;
            });
        }
    }
}
