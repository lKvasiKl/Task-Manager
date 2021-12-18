using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.MVVM.ViewModel
{
    public class MyDayViewModel : TasksListBaseViewModel
    {
        public MyDayViewModel() : base()
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
