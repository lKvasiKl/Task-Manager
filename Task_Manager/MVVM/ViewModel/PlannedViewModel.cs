using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.MVVM.Model;

namespace Task_Manager.MVVM.ViewModel
{
    public class PlannedViewModel : TasksListBaseViewModel
    {
        public PlannedViewModel() : base()
        {

        }

        public PlannedViewModel(TaskList tasksList) : base(tasksList)
        {

        }
    }
}
