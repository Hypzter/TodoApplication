using Todo.Cli;
using ToDo.Core;

namespace ToDo.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new Storage();
            var todoList = new TodoList(storage);
            var consoleWrapper = new ConsoleWrapper();
            var userInteraction = new UserInteraction(consoleWrapper);

            var app = new App(todoList, storage, userInteraction);
            app.Start();
        }
    }
}