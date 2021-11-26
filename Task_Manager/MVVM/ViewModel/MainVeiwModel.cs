using System;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    internal class MainVeiwModel : ObservableObject
    {
        public RelayCommand MyDayCommand { get; set; }
        public RelayCommand ImportantCommand { get; set; }
        public RelayCommand PlannedCommand { get; set; }
        public RelayCommand TasksCommand { get; set; }
        public MyDayViewModel MyDayVM { get; set; }
        public ImportantViewModel ImportantVM { get; set; }
        public PlannedViewModel PlannedVM { get; set; }
        public TasksViewModel TasksVM { get; set; }

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
        }
    }
}
