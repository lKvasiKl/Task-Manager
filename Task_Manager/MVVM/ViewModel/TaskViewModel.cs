 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Manager.Core;
using Task_Manager.MVVM.Model;

namespace Task_Manager.MVVM.ViewModel
{
    public class TaskViewModel : ObservableObject
    {
        private Task _task;

        public RelayCommand DeselectDateCommand { get; set; }

        public string Description
        {
            get
            {
                return _task.Description;
            }

            set
            {
                _task.Description = value;
                OnPropertyChanged();
            }
        }

        public bool IsDone
        {
            get
            {
                return _task.IsDone;
            }

            set
            {
                _task.IsDone = value;
                OnPropertyChanged();
            }
        }

        public bool IsImportant
        {
            get
            {
                return _task.IsImportant;
            }

            set
            {
                _task.IsImportant = value;
                OnPropertyChanged();
            }
        }

        public bool IsMyDay
        {
            get
            {
                return _task.IsMyDay;
            }

            set
            {
                _task.IsMyDay = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Date
        {
            get
            {
                return _task.Date;
            }

            set
            {
                _task.Date = value;
                OnPropertyChanged();
            }
        }

        public DateTime? NotificationDate
        {
            get
            {
                return _task.NotificationDate;
            }

            set
            {
                _task.NotificationDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Time
        {
            get
            {
                return _task.Time;
            }

            set
            {
                _task.Time = value;
                OnPropertyChanged();
            }
        }

        public string TheNote
        {
            get
            {
                return _task.TheNote;
            }

            set
            {
                _task.TheNote = value;
                OnPropertyChanged();
            }

        }

        public TaskViewModel()
        {
            _task = new Task();

            DeselectDateCommand = new RelayCommand(o =>
            {
                Date = null;
                NotificationDate = null;
                Time = null;
            });

        }

        public override bool Equals(object? obj)
        {
            if (obj is TaskViewModel taskVM)
            {
                if (_task == taskVM._task)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
