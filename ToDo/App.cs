using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Cli;

namespace Todo.Cli
{
    internal class App
    {
        private readonly ITodoList _todoList;
        private readonly IStorage _storage;
        private readonly IUserInteraction _userInteraction;

        public App(
            ITodoList todoList, 
            IStorage storage,
            IUserInteraction userInteraction)
        {
            _todoList = todoList;
            _storage = storage;
            _userInteraction = userInteraction;
        }
        internal void Start()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                PrintMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'v':
                        ViewTaskMenu();
                        break;

                    case 'a':
                        string[] inputs = TakeUserInput();
                        _todoList.AddTask(inputs[0], inputs[1], false);
                        break;

                    case 'd':
                        PrintTasksForDeletion();
                        break;

                    case 'm':
                        PrintTasksForCompletion();
                        break;

                    case 'q':
                        isRunning = false;
                        break;
                }
            }
        }
        private void ViewTaskMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("--- Task menu ---");
                Console.WriteLine();
                Console.WriteLine("View [A]ll tasks");
                Console.WriteLine("View [C]ompleted tasks");
                Console.WriteLine("View [U]ncompleted tasks");
                Console.WriteLine("[B]ack to menu");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'a':
                        PrintTasks(_todoList.ViewTasks(), "all");
                        break;

                    case 'c':
                        var completed = _todoList.ViewCompletedTasks();
                        PrintTasks(completed, "completed");
                        break;

                    case 'u':
                        var unCompleted = _todoList.ViewUnCompletedTasks();
                        PrintTasks(unCompleted, "uncompleted");
                        break;

                    case 'b':
                        isRunning = false;
                        break;

                }
            }
        }
        public string[] TakeUserInput()
        {
            Console.Write("Title of new task: ");
            var title = Console.ReadLine();
            Console.Write("Description of new task: ");
            var desc = Console.ReadLine();

            string[] userInput = { title, desc };

            return userInput;
        }
        public void PrintMenu()
        {

            Console.WriteLine("--- Welcome to the Todo App! ---");
            Console.WriteLine();
            Console.WriteLine("[V]iew tasks");
            Console.WriteLine("[A]dd task");
            Console.WriteLine("[D]elete task");
            Console.WriteLine("[M]ark task as completed");
            Console.WriteLine("[Q]uit app");
        }
        public void PrintTasks(List<TodoItem> tasks, string header)
        {
            Console.Clear();
            Console.WriteLine("Viewing " + header + " tasks");
            Console.WriteLine();
            if (tasks != null)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine(i + ". " + tasks[i].Title + " " + (tasks[i].IsComplete == false ? "uncompleted" : "completed"));
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("There is no tasks at this moment.");
            }
            ContinueMethod();
        }
        public void ContinueMethod()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        public void PrintTasksForDeletion()
        {
            Console.Clear();
            Console.WriteLine("Tasks");
            Console.WriteLine();
            for (int i = 0; i < _todoList.ViewTasks().Count; i++)
            {
                Console.WriteLine(i + ". " + _todoList.ViewTasks()[i].Title);
            }
            Console.Write("Enter the index of the Task to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < _todoList.ViewTasks().Count)
            {
                _todoList.DeleteTask(_todoList.ViewTasks()[index]);
                Console.WriteLine("Task removed.");
                ContinueMethod();
            }
            else
            {
                Console.WriteLine("Invalid input. No task removed.");
                ContinueMethod();
            }
        }
        public void PrintTasksForCompletion()
        {
            Console.Clear();
            Console.WriteLine("Tasks");
            Console.WriteLine();
            for (int i = 0; i < _todoList.ViewTasks().Count; i++)
            {
                Console.WriteLine(i + ". " + _todoList.ViewTasks()[i].Title);
            }
            Console.Write("Enter the index of the Task to complete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < _todoList.ViewTasks().Count)
            {
                _todoList.MarkTaskAsComplete(_todoList.ViewTasks()[index]);
                Console.WriteLine("Task completed.");
                ContinueMethod();
            }
            else
            {
                Console.WriteLine("Invalid input. No task was changed.");
                ContinueMethod();
            }
        }
    }
}
