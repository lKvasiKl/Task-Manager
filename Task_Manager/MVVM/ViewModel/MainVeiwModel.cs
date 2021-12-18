using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Task_Manager.Core;
using Task_Manager.MVVM.Model;

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
            Loading();

            MyDayCommand = new RelayCommand(o => 
            {
                MyDayVM.Update();
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = MyDayVM;
                if (Settings.Default.Date.Date != DateTime.Now.Date)
                {
                    MyDayVM.UpdateTasks();
                    Settings.Default.Date = DateTime.Now;
                }
            });

            MyDayCommand.Execute(this);

            ImportantCommand = new RelayCommand(o =>
            {
                ImportantVM.Update();
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = ImportantVM;
            });

            PlannedCommand = new RelayCommand(o =>
            {
                PlannedVM.Update();
                CurrentView.SelectedTask = null;
                TaskEditVM.CloseEditWindow.Execute(o);
                CurrentView = PlannedVM;
            });

            TasksCommand = new RelayCommand(o =>
            {
                TasksVM.Update();
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
                    SelectedTasksList.Update();
                    CurrentView = SelectedTasksList;
                }
            });

            AddTaskListCommand = new RelayCommand(o =>
            {
                TasksListViewModel tasksList = new(true);
                tasksList.PropertyChanged += SelectionChanged;
                tasksList.AddToImportantCommand = AddToImportantCommand;
                tasksList.RemoveFromImportantCommand = RemoveFromImportantCommand;
                DataBase.TasksLists.Add(tasksList.List);
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
                                Task task = viewModel1.SelectedTask.Task;
                                viewModel1.RemoveTask(viewModel1.SelectedTask);
                                DataBase.RemoveTask(task);
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

        public void Loading()
        {
            List<TasksListBaseViewModel> lists = new();
            MyDayVM = new MyDayViewModel(DataBase.TasksLists[0]);
            ImportantVM = new ImportantViewModel(DataBase.TasksLists[1]);
            PlannedVM = new PlannedViewModel(DataBase.TasksLists[2]);
            TasksVM = new TasksViewModel(DataBase.TasksLists[3]);
            MyDayVM.PropertyChanged += SelectionChanged;
            MyDayVM.AddToImportantCommand = AddToImportantCommand;
            MyDayVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            ImportantVM.PropertyChanged += SelectionChanged;
            ImportantVM.AddToImportantCommand = AddToImportantCommand;
            ImportantVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            PlannedVM.PropertyChanged += SelectionChanged;
            PlannedVM.AddToImportantCommand = AddToImportantCommand;
            PlannedVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            TasksVM.PropertyChanged += SelectionChanged;
            TasksVM.AddToImportantCommand = AddToImportantCommand;
            TasksVM.RemoveFromImportantCommand = RemoveFromImportantCommand;
            CurrentView = MyDayVM;
            for (int i = 4; i < DataBase.TasksLists.Count; i++)
            { 
                TasksListViewModel list =  new TasksListViewModel(DataBase.TasksLists[i]);
                list.PropertyChanged += SelectionChanged;
                list.AddToImportantCommand = AddToImportantCommand;
                list.RemoveFromImportantCommand = RemoveFromImportantCommand;
                TasksLists.Add(list);
            }

        }
    }
}
