using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Cli
{
    public class TodoList : ITodoList
    {
        private readonly IStorage? _storage;

        public List<TodoItem> AllTasks { get; set; } = new();

        public TodoList(IStorage todoListStorage)
        {
            _storage = todoListStorage;
        }

        

        public TodoItem AddTask(string title, string description, bool isComplete)
        {
            var item = new TodoItem();
            item.Title = title;
            item.Description = description;
            item.IsComplete = isComplete;

            AllTasks.Add(item);

            if (_storage != null)
            {
                _storage.SaveTask(item);

            }

            return item;
        }

        public List<TodoItem> ViewTasks()
        {
            return AllTasks;
        }

        public List<TodoItem> ViewCompletedTasks()
        {
            return AllTasks.Where(x => x.IsComplete == true).ToList();
        }
        public List<TodoItem> ViewUnCompletedTasks()
        {
            return AllTasks.Where(x => x.IsComplete == false).ToList();
        }

        public TodoItem MarkTaskAsComplete(TodoItem task)
        {
            task.IsComplete = true;
            return task;
        }

        public void DeleteTask(TodoItem task)
        {
            AllTasks.Remove(task);
        }
    }
}
