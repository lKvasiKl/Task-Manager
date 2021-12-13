using System;
using System.Collections.ObjectModel;
using Task_Manager.Core;

namespace Task_Manager.MVVM.ViewModel
{
    public class TasksListViewModel : TasksListBaseViewModel
    {
        private string _name;

        private bool _needInitFocus;

        public string Name 
        { 
            get 
            { 
                return _name; 
            }

            set
            {
                _name = value;
                if (_name == string.Empty)
                {
                    _name = "The list without name";
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

        
    }
}
