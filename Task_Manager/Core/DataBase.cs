using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Task_Manager.MVVM.Model;

namespace Task_Manager.Core
{
    public static class DataBase
    {
        public static List<TaskList> TasksLists { get; set; }

        static DataBase()
        {
            TasksLists = new List<TaskList>();
            Deserialize();
        }

        public static void Serialize()
        {
            XmlSerializer serializable = new(typeof(List<TaskList>));
            if(!File.Exists("Data\\data.xml"))
            {
                File.Create("Data\\data.xml");
            }
            using (FileStream fs = new("Data\\data.xml", FileMode.Truncate))
            {
                serializable.Serialize(fs, TasksLists);
            }
        }

        public static void Deserialize()
        {
            XmlSerializer deserializer = new(typeof(List<TaskList>));
            using (FileStream fs = new("Data\\data.xml", FileMode.Open, FileAccess.Read))
            {
                TasksLists = (List<TaskList>)deserializer.Deserialize(fs);
            }
        }

        public static void RemoveTask(Task task)
        {
            for (int i = 0; i < TasksLists.Count; i++)
            {
                TasksLists[i].RemoveTask(task);
            }
        }
    }
}
