using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.MVVM.Model
{
    [Serializable]
    public class Task
    {
        public string Description { get; set; } = string.Empty;

        public bool IsDone { get; set; } = false;

        public bool IsImportant { get; set; } = false;

        public bool IsMyDay { get; set; } = false;

        public bool IsPlanned { get; set; } = false;

        public bool IsTask { get; set; } = false;

        public DateTime? Date { get; set; } = new DateTime();

        public DateTime? NotificationDate { get; set; } = new DateTime();

        public DateTime? Time { get; set; } = new DateTime();

        public string TheNote { get; set; } = string.Empty;

        public Task()
        {

        }

        public override bool Equals(object? obj)
        {
            if(obj is Task task)
            {
                if (Description == task.Description &&
                    IsDone == task.IsDone
                    && IsImportant == task.IsImportant
                    && IsMyDay == task.IsMyDay
                    && IsPlanned == task.IsPlanned
                    && IsTask == task.IsTask
                    && TheNote == task.TheNote)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
