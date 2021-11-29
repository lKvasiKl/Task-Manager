using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.MVVM.Model
{
    public class Task
    {
        public string Description { get; set; } = string.Empty;

        public bool IsDone { get; set; } = false;

        public DateOnly Date { get; set; } = new DateOnly();

        public TimeOnly Time { get; set; } = new TimeOnly();

        public string TheNote { get; set; } = string.Empty;

        public Task()
        {
        }
    }
}
