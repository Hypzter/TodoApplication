namespace ToDo.Cli
{
    public interface ITodoList
    {
        public TodoItem AddTask(string title, string description, bool isComplete);

        public List<TodoItem> ViewTasks();

        public List<TodoItem> ViewCompletedTasks();

        public List<TodoItem> ViewUnCompletedTasks();


        public TodoItem MarkTaskAsComplete(TodoItem task);


        public void DeleteTask(TodoItem task);

    }
}