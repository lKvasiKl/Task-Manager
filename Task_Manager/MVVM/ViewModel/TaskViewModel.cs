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

        public DateOnly Date
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

        public TimeOnly Time
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
        }
    }
}
