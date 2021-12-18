using System;
using System.Collections.ObjectModel;
using Task_Manager.Core;
using Task_Manager.MVVM.Model;

namespace Task_Manager.MVVM.ViewModel
{
    public class TasksListViewModel : TasksListBaseViewModel
    {
        private bool _needInitFocus;

        public string Name 
        { 
            get 
            { 
                return _taskList.Name; 
            }

            set
            {
                
                if (value == string.Empty)
                {
                    CustomMassageBox.Show("The tasks list name cannot be empty!", CustomMassageBox.CMessageTitle.Info, CustomMassageBox.CMessageButton.Ok, CustomMassageBox.CMessageButton.Cancel);
                }
                else
                {
                    _taskList.Name = value;
                }
                OnPropertyChanged();
            }
        }

        public bool NeedInitFocus
        {
            get 
            {  
                return _needInitFocus; 
            }

            set 
            { 
                _needInitFocus = value; 
                OnPropertyChanged(); 
            }
        }

        public TasksListViewModel() : base()
        {
            Name = "The list without name"; 
        }

        public TasksListViewModel(bool needInitFocus = true) : this()
        {
            NeedInitFocus = needInitFocus;
        }

        public TasksListViewModel(TaskList taskList) : base(taskList)
        {
            NeedInitFocus = false; ;
        }


    }
}
