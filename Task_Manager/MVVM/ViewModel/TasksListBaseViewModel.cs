using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class TasksListBaseViewModel : ObservableObject
    {
        private bool _needShowPicture = true;
        private bool _needShowBorder = false;
        private TaskViewModel? _selectedTask;
        private string _searchFilter;

        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        public ObservableCollection<TaskViewModel> DoneTasks { get; set; }

        public CollectionViewSource FilteredTasks { get; set; }

        public CollectionViewSource FilteredDoneTasks { get; set; }

        public RelayCommand AddToImportantCommand { get; set; }

        public RelayCommand RemoveFromImportantCommand { get; set; }

        public RelayCommand AddorRemoveToMyDayCommand { get; set; }

        public RelayCommand RemoveFromMyDayCommand { get; set; }

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

        public bool NeedShowBorder
        {
            get
            {
                return _needShowBorder;
            }

            set
            {
                _needShowBorder = value;
                OnPropertyChanged();
            }
        }

        public TaskViewModel? SelectedTask
        {
            get
            {
                return _selectedTask;
            }

            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public string SearchFilter
        {
            get
            {
                return _searchFilter;
            }
            
            set
            {
                _searchFilter = value;
                AddFilter();
                _selectedTask = null;
                OnPropertyChanged();
                FilteredTasks.View.Refresh();
                
            }
        }

        public TasksListBaseViewModel()
        {
            
            Tasks = new ObservableCollection<TaskViewModel>();
            DoneTasks = new ObservableCollection<TaskViewModel>();
            Tasks.CollectionChanged += TasksCollectionChanged;
            DoneTasks.CollectionChanged += TasksCollectionChanged;
            Sort(Tasks);
            FilteredTasks = new CollectionViewSource
            {
                Source = Tasks
            };

            FilteredDoneTasks = new CollectionViewSource
            {
                Source = DoneTasks
            };

            SearchFilter = string.Empty;
        }

        private void AddFilter()
        {
            FilteredTasks.Filter += (object sender, FilterEventArgs e) =>
            {
                TaskViewModel? search = e.Item as TaskViewModel;
                if (SearchFilter != null)
                {
                    if (search.Description.Contains(SearchFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            };

            FilteredDoneTasks.Filter += (object sender, FilterEventArgs e) =>
            {
                TaskViewModel? search = e.Item as TaskViewModel;
                if (SearchFilter != null)
                {
                    if (search.Description.Contains(SearchFilter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            };
            
        }

        private void Sort(ObservableCollection<TaskViewModel> Tasks)
        {
            List<TaskViewModel> tasks = Tasks.ToList();
            tasks.Sort();
            for (int i = 0; i < tasks.Count; i++)
            {
                Tasks[i] = tasks[i];
            }
        }

        private void TasksCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Tasks.Count > 0 || DoneTasks.Count > 0)
            {
                NeedShowPicture = false;
            }
            else
            
            {
                NeedShowPicture = true;
            }

            if (DoneTasks.Count > 0)
            {
                NeedShowBorder = true;
            }
            else
            {
                NeedShowBorder = false;
            }
        }


        public void AddTask(TaskViewModel task)
        {
            task.PropertyChanged -= ImportenceChanged;
            task.PropertyChanged += ImportenceChanged;
            if (task.IsDone)
            {
                if (!DoneTasks.Contains(task))
                {
                    DoneTasks.Add(task);
                }
            }
            else if (!Tasks.Contains(task))
            {
                Tasks.Add(task);
            }
        }

        public void RemoveTask(TaskViewModel task)
        {
            DoneTasks.Remove(task);
            Tasks.Remove(task);
        }

        private void ImportenceChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is TaskViewModel taskViewModel)
            {
                if (e.PropertyName == nameof(TaskViewModel.IsImportant))
                {
                    if (taskViewModel.IsImportant == true)
                    {
                        AddToImportantCommand.Execute(sender);
                    }
                    else
                    {
                        RemoveFromImportantCommand.Execute(sender);
                    }
                }

                if (e.PropertyName == nameof(TaskViewModel.IsDone))
                {
                    if (taskViewModel.IsDone == true)
                    {
                        Tasks.Remove(taskViewModel);
                        DoneTasks.Add(taskViewModel);
                    }
                    else
                    {
                        Tasks.Add(taskViewModel);
                        DoneTasks.Remove(taskViewModel);
                    }
                }
            }
        }
        
    }
}
