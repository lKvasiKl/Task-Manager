using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class TasksListBaseViewModel : ObservableObject
    {
        private bool _needShowPicture = true;

        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        public TasksListBaseViewModel()
        {
            Tasks = new ObservableCollection<TaskViewModel>();
            Tasks.CollectionChanged += TasksCollectionChanged;
        }

        private void TasksCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (sender is ObservableCollection<TaskViewModel> tasks)
            {
                if (tasks.Count > 0)
                {
                    NeedShowPicture = false;
                }
                else
                {
                    NeedShowPicture = true;
                }

            }
        }

        public bool NeedShowPicture
        {
            get
            {
                return _needShowPicture;
            }

            set
            {
                _needShowPicture = value;
                OnPropertyChanged();
            }
        }

        public void AddTask(TaskViewModel task)
        {
            Tasks.Add(task);
        }

        public void Remove(TaskViewModel task)
        {
            Tasks.Remove(task);
        }
    }
}
