using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class MainVeiwModel : ObservableObject
    {
        private TasksListViewModel _selectedTasksList;

        public ObservableCollection<TasksListViewModel> TasksLists { get; set; }

        public RelayCommand MyDayCommand { get; set; }

        public RelayCommand ImportantCommand { get; set; }

        public RelayCommand PlannedCommand { get; set; }

        public RelayCommand TasksCommand { get; set; }

        public RelayCommand TasksListCommand { get; set; }

        public RelayCommand AddTaskListCommand { get; set; }

        public RelayCommand RemoveTaskListCommand { get; set; }

        public RelayCommand AddTaskToListCommand { get; set; }

        public MyDayViewModel MyDayVM { get; set; }

        public ImportantViewModel ImportantVM { get; set; }

        public PlannedViewModel PlannedVM { get; set; }

        public TasksViewModel TasksVM { get; set; }

        public AddTaskButtonViewModel AddTaskButtonVM { get; set; }

        public TasksListViewModel SelectedTasksList
        {
            get
            {
                return _selectedTasksList;
            }
            set
            {
                _selectedTasksList = value;
                TasksListCommand.Execute(_selectedTasksList);
                OnPropertyChanged();
            }
        }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainVeiwModel()
        {
            AddTaskButtonVM = new AddTaskButtonViewModel();
            TasksLists = new ObservableCollection<TasksListViewModel>();
            TasksLists.Add(new TasksListViewModel(false));
            TasksLists.Add(new TasksListViewModel(false));
            TasksLists[0].Name = "Products";
            TasksLists[1].Name = "Homework";
            MyDayVM = new MyDayViewModel();
            ImportantVM = new ImportantViewModel();
            PlannedVM = new PlannedViewModel();
            TasksVM = new TasksViewModel();
            CurrentView = MyDayVM;

            MyDayCommand = new RelayCommand(o => 
            {
                CurrentView = MyDayVM;
            });

            ImportantCommand = new RelayCommand(o =>
            {
                CurrentView = ImportantVM;
            });

            PlannedCommand = new RelayCommand(o =>
            {
                CurrentView = PlannedVM;
            });

            TasksCommand = new RelayCommand(o =>
            {
                CurrentView = TasksVM;
            });

            TasksListCommand = new RelayCommand(o =>
            {
                CurrentView = SelectedTasksList;
            });

            AddTaskListCommand = new RelayCommand(o =>
            {
                TasksListViewModel tasksList = new(true);
                TasksLists.Add(tasksList);
                SelectedTasksList = tasksList;
            });

            RemoveTaskListCommand = new RelayCommand(o =>
            {
                if (SelectedTasksList != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete your to-do list?", "Remove to-do list", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        TasksLists.Remove(SelectedTasksList);
                        if (TasksLists.Count > 0)
                        {
                            SelectedTasksList = TasksLists[^1];
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Select the to-do list you want to delete.", "Remove to-do list");
                }
            });

            AddTaskToListCommand = new RelayCommand(o =>
            {
                if (AddTaskButtonVM.TaskVM.Description != string.Empty)
                {
                    (CurrentView as TasksListBaseViewModel).AddTask(AddTaskButtonVM.TaskVM);
                    AddTaskButtonVM.TaskVM = new TaskViewModel();
                }
                
            });
        }
    }
}
