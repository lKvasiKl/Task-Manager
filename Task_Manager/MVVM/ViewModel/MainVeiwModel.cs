using System;
using System.Collections.ObjectModel;
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

        public MyDayViewModel MyDayVM { get; set; }

        public ImportantViewModel ImportantVM { get; set; }

        public PlannedViewModel PlannedVM { get; set; }

        public TasksViewModel TasksVM { get; set; }

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
            TasksLists = new ObservableCollection<TasksListViewModel>();
            TasksLists.Add(new TasksListViewModel());
            TasksLists.Add(new TasksListViewModel());
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
                TasksLists.Add(new TasksListViewModel());
            });
        }
    }
}
