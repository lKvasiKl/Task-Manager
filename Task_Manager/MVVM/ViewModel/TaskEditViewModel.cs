using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class TaskEditViewModel : ObservableObject
    {
        private TaskViewModel? _taskViewModel;
        private bool _isVisible;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public string MyDayButtonContent
        {
            get
            {
                if (_taskViewModel != null && !_taskViewModel.IsMyDay)
                {
                    return "☼ Add to My Day view.";
                }
                else if (_taskViewModel != null && _taskViewModel.IsMyDay)
                {
                    return "☼ Delete from My Day view.";
                }

                return string.Empty;
            }
        }
        public TaskViewModel? Task
        {
            get
            {
                return _taskViewModel;
            }

            set
            {
                _taskViewModel = value;
                if (_taskViewModel != null)
                {
                    IsVisible = true;
                    _taskViewModel.PropertyChanged += TasksViewChandes;
                    OnPropertyChanged(nameof(Decorations));
                    OnPropertyChanged(nameof(MyDayButtonContent));
                }
                else
                {
                    IsVisible = false;
                }
                OnPropertyChanged();
            }
        }

        private void TasksViewChandes(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(TaskViewModel.IsDone):
                    {
                        OnPropertyChanged(nameof(Decorations));
                        break;
                    }

                case nameof(TaskViewModel.IsMyDay):
                    {
                        OnPropertyChanged(nameof(MyDayButtonContent));
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public System.Windows.TextDecorationCollection Decorations
        {
            get
            {
                if (Task != null && Task.IsDone == true)
                {
                    return TextDecorations.Strikethrough;
                }

                return null;
            }
        }


        public RelayCommand CloseEditWindow { get; set; }

        public RelayCommand AddOrRemoveMyDayCommand { get; set; }

        public RelayCommand DeleteTaskFromList { get; set; }

        public TaskEditViewModel()
        {
            AddOrRemoveMyDayCommand = new RelayCommand(o =>
            {

            });

            CloseEditWindow = new RelayCommand(o =>
            {
                Task = null;
            });

            DeleteTaskFromList = new RelayCommand(o =>
            {
                OnPropertyChanged("TaskRemoved");
                CloseEditWindow.Execute(o);
            });
        }
    }
}
