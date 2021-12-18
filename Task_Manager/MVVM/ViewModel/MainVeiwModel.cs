using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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

        public RelayCommand AddToImportantCommand { get; set; }

        public RelayCommand RemoveFromImportantCommand { get; set; }

        public MyDayViewModel MyDayVM { get; set; }

        public ImportantViewModel ImportantVM { get; set; }

        public PlannedViewModel PlannedVM { get; set; }

        public TasksViewModel TasksVM { get; set; }

        public AddTaskButtonViewModel AddTaskButtonVM { get; set; }

        public TaskEditViewModel TaskEditVM { get; set; }

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

        private TasksListBaseViewModel _currentView;

        

        public TasksListBaseViewModel CurrentView
        {
            get 
            { 
                return _currentView; 
            }

            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainVeiwModel()
        {
            AddToImportantCommand = new RelayCommand(o =>
            {
                if (o is TaskViewModel task)
                {
                    ImportantVM.AddTask(task);
                }

            });

            RemoveFromImportantCommand = new RelayCommand(o =>
            {
                if (o is TaskViewModel task)
                {
                    ImportantVM.RemoveTask(task);
                }

            });

            TaskEditVM = new TaskEditViewModel();
            TaskEditVM.PropertyChanged += Deselect;
            AddTaskButtonVM = new AddTaskButtonViewModel();
            TasksLists = new ObservableCollection<TasksListViewModel>();
            TasksLists.Add(new TasksListViewModel(false));
            TasksLists.Add(new TasksListViewModel(false));
            TasksLists[0].Name = "Products";
            TasksLists[0].PropertyChanged += SelectionChanged;
            TasksLists[1].Name = "Homework";
            TasksLists[1].PropertyChanged += SelectionChanged;
            MyDayVM = new MyDayViewModel();
            MyDayVM.PropertyChanged += SelectionChanged;
            MyDayVM.AddToImportantCommand = AddToImportantCommand;
            MyDayVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            ImportantVM = new ImportantViewModel();
            ImportantVM.PropertyChanged += SelectionChanged;
            ImportantVM.AddToImportantCommand = AddToImportantCommand;
            ImportantVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            PlannedVM = new PlannedViewModel();
            PlannedVM.PropertyChanged += SelectionChanged;
            PlannedVM.AddToImportantCommand = AddToImportantCommand;
            PlannedVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            TasksVM = new TasksViewModel();
            TasksVM.PropertyChanged += SelectionChanged;
            TasksVM.AddToImportantCommand = AddToImportantCommand;
            TasksVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            CurrentView = MyDayVM;

           
            MyDayCommand = new RelayCommand(o => 
            {
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = MyDayVM;
            });

            ImportantCommand = new RelayCommand(o =>
            {
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = ImportantVM;
            });

            PlannedCommand = new RelayCommand(o =>
            {
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = PlannedVM;
            });

            TasksCommand = new RelayCommand(o =>
            {
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = TasksVM;
            });

            TasksListCommand = new RelayCommand(o =>
            {
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                if (SelectedTasksList != null)
                {
                    CurrentView = SelectedTasksList;
                }
            });

            AddTaskListCommand = new RelayCommand(o =>
            {
                TasksListViewModel tasksList = new(true);
                tasksList.PropertyChanged += SelectionChanged;
                tasksList.AddToImportantCommand = AddToImportantCommand;
                tasksList.RemoveFromImportantCommand = RemoveFromImportantCommand;
                TasksLists.Add(tasksList);
                SelectedTasksList = tasksList;
            });

            RemoveTaskListCommand = new RelayCommand(o =>
            {
                if (SelectedTasksList != null)
                {
                    DialogResult result = CustomMassageBox.Show("This to-do list will be permanently deleted.", CustomMassageBox.CMessageTitle.Deleting, CustomMassageBox.CMessageButton.Delete, CustomMassageBox.CMessageButton.Cancel);
                    if (result == DialogResult.Yes)
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
                    CustomMassageBox.Show("Select the to-do list you want to delete.", CustomMassageBox.CMessageTitle.Info, CustomMassageBox.CMessageButton.Ok, CustomMassageBox.CMessageButton.Cancel);
                }
            });

            AddTaskToListCommand = new RelayCommand(o =>
            {
                if (AddTaskButtonVM.TaskVM.Description != string.Empty)
                {
                    if (CurrentView != ImportantVM && CurrentView != MyDayVM)
                    {
                        (CurrentView as TasksListBaseViewModel).AddTask(AddTaskButtonVM.TaskVM);
                        AddTaskButtonVM.TaskVM = new TaskViewModel();
                    }
                    else if (CurrentView == ImportantVM)
                    {
                        AddTaskButtonVM.TaskVM.IsImportant = true;
                        ImportantVM.AddTask(AddTaskButtonVM.TaskVM);
                        TasksVM.AddTask(AddTaskButtonVM.TaskVM);
                        AddTaskButtonVM.TaskVM = new TaskViewModel();
                    }
                    else
                    {
                        AddTaskButtonVM.TaskVM.IsMyDay = true;
                        MyDayVM.AddTask(AddTaskButtonVM.TaskVM);
                        TasksVM.AddTask(AddTaskButtonVM.TaskVM);
                        AddTaskButtonVM.TaskVM = new TaskViewModel();
                    }
                }
                else
                {
                    CustomMassageBox.Show("The tasks description cannot be empty!", CustomMassageBox.CMessageTitle.Info, CustomMassageBox.CMessageButton.Ok, CustomMassageBox.CMessageButton.Cancel);
                }
                
            });

            TaskEditVM.AddOrRemoveMyDayCommand = new RelayCommand(o =>
            {
                if (CurrentView != null && TaskEditVM.Task != null && !TaskEditVM.Task.IsMyDay)
                {
                    TaskEditVM.Task.IsMyDay = true;
                    MyDayVM.AddTask(TaskEditVM.Task);

                }
                else if (CurrentView != null && TaskEditVM.Task != null && TaskEditVM.Task.IsMyDay)
                {
                    TaskEditVM.Task.IsMyDay = false;
                    MyDayVM.RemoveTask(TaskEditVM.Task);
                }
            });

        }

        private void SelectionChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is TasksListBaseViewModel viewModel)
            {
                if (viewModel.SelectedTask != null)
                {
                    TaskEditVM.Task = viewModel.SelectedTask;
                }
            }
        }

        private void Deselect(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is TaskEditViewModel editVM)
            {
                switch (e.PropertyName)
                {
                    case "TaskRemoved":
                        {
                            if (CurrentView is TasksListBaseViewModel viewModel1)
                            {
                                viewModel1.RemoveTask(viewModel1.SelectedTask);
                            }

                            break;
                        }
                    case nameof(TaskEditViewModel.Task):
                        {
                            if (editVM.Task == null)
                            {
                                if (CurrentView is TasksListBaseViewModel viewModel2)
                                {
                                    viewModel2.SelectedTask = null;
                                }
                            }

                            break;
                        }
                        
                    default:
                        {
                            break;
                        }
                       
                }
            }
        }
    }
}
