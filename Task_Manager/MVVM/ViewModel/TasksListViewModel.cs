using System;
using System.Collections.ObjectModel;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class TasksListViewModel : ObservableObject
    {
        private string _name;
        public string Name 
        { 
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        public TasksListViewModel()
        {
            Name = "The list without name"; 
            Tasks = new ObservableCollection<TaskViewModel>();
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
