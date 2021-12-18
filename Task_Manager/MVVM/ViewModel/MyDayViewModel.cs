using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.MVVM.Model;

namespace Task_Manager.MVVM.ViewModel
{
    public class MyDayViewModel : TasksListBaseViewModel
    {
        public MyDayViewModel() : base()
        { 

        }

        public MyDayViewModel(TaskList tasksList) : base(tasksList)
        {

        }

        public void UpdateTasks ()
        {
            for (int i = 0; i < DoneTasks.Count; i++)
            {
                DoneTasks[i].IsDone = false;
            }
        }

    }
}
