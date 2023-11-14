using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Cli;

namespace ToDo.Core
{
    public class Storage : IStorage
    {
        private readonly IStorage _storage;
        public List<TodoItem> GetAll()
        {
            return _storage.GetAll();
        }

        public TodoItem LoadTask(string title)
        {
            return _storage.LoadTask(title);
        }

        public bool SaveTask(TodoItem item)
        {
            return _storage != null ? _storage.SaveTask(item) : false;
        }
    }
}
