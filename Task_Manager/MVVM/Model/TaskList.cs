using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.MVVM.Model
{
    [Serializable]
    public class TaskList
    {
        public string Name { get; set; }

        public List<Task> Tasks { get; set; }

        public TaskList()
        {
            Name = "The list without name";
            Tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            if(!Tasks.Contains(task))
            {
                Tasks.Add(task);
            }
        }

        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
        }
    }
}
