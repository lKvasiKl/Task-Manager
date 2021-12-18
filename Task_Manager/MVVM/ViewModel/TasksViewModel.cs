using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.MVVM.Model;

namespace Task_Manager.MVVM.ViewModel
{
    public class TasksViewModel : TasksListBaseViewModel
    {
        public TasksViewModel() : base()
        {

        }

        public TasksViewModel(TaskList taskList) : base(taskList)
        {

        }
    }
}
