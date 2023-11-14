using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Cli
{
    public interface IStorage
    {
        bool SaveTask(TodoItem item);

        TodoItem LoadTask(string title);

        List<TodoItem> GetAll();

    }
}
