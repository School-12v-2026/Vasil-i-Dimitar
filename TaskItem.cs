using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskManager
{
    class TaskItem
    {
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public TaskItem() { }

        public TaskItem(string name, bool isDone = false)
        {
            Name = name;
            IsDone = isDone;
        }
    }
}
