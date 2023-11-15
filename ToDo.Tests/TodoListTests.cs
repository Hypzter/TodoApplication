using Moq;
using Todo.Cli;
using ToDo.Cli;

namespace ToDo.Tests
{
    public class TodoListTests
    {
        [Fact]
        public void AddTaskReturnsValidTask()
        {
            // Arrange
            var title = "Empty Dishwasher";
            var description = "Empty dishwasher and make sure everything is in place.";
            var isComplete = false;

            var mockIStorage = new Mock<IStorage>();

            var sut = new ToDo.Cli.TodoList(mockIStorage.Object);

            // Act
            var actual = sut.AddTask(title, description, isComplete);

            // Arrange
            Assert.NotEqual(Guid.Empty, actual.Id);
            Assert.Equal(title, actual.Title);
            Assert.Equal(description, actual.Description);
            Assert.False(actual.IsComplete);
        }

        [Fact]
        public void WhenAddingTaskItSavesToListStorage()
        {
            // Arrange
            var title = "Empty Dishwasher";
            var description = "Empty dishwasher and make sure everything is in place.";
            var isComplete = false;

            var mockIStorage = new Mock<IStorage>();

            var sut = new ToDo.Cli.TodoList(mockIStorage.Object);

            // Act
            var actual = sut.AddTask(title, description, isComplete);

            // Assert
            mockIStorage.Verify(x => x.SaveTask(actual), Times.Once);
        }

        [Fact]
        public void CanMarkTaskAsCompleted()
        {
            // Arrange
            var title = "Empty Dishwasher";
            var description = "Empty dishwasher and make sure everything is in place.";
            var isComplete = false;

            var mockIStorage = new Mock<IStorage>();

            var sut = new ToDo.Cli.TodoList(mockIStorage.Object);

            // Act
            var actual = sut.AddTask(title, description, isComplete);
            actual = sut.MarkTaskAsComplete(actual);

            // Assert 
            Assert.True(actual.IsComplete);
        }

        [Fact]
        public void WhenDeletingTaskRemoveItFromTodoList()
        {
            // Arrange
            List<TodoItem> todoList = new List<TodoItem>
            {
                 new TodoItem { Title = "Empty dishwasher", Description = "Get it done", IsComplete = false },
                 new TodoItem { Title = "Fix dinner", Description = "Make it taste good", IsComplete = false },
                 new TodoItem { Title = "Vacuum", Description = "I want to eat on the floor", IsComplete = false }
            };

            var mockIStorage = new Mock<IStorage>();

            var sut = new ToDo.Cli.TodoList(mockIStorage.Object);
            foreach (var item in todoList)
            {
                sut.AllTasks.Add(item);
            }

            // Act
            var itemToRemove = todoList[0];
            sut.DeleteTask(itemToRemove);

            // Assert
            Assert.DoesNotContain(itemToRemove, sut.AllTasks);
        }

        [Fact]
        public void CanLoadTasksFromTodoListStorage()
        {
            // Arrange
            var mockIStorage = new Mock<IStorage>();
            var todoList = new List<TodoItem>
                {
                new TodoItem { Title = "Empty Dishwasher", Description = "Empty dishwasher and make sure everything is in place.", IsComplete = false },
                new TodoItem { Title = "Sweep", Description = "Sweep floor", IsComplete = false },
                new TodoItem { Title = "Make lunch", Description = "Make it taste nice", IsComplete = false }
            };
            mockIStorage.Setup(x => x.GetAll()).Returns(todoList);

            var sut = new ToDo.Cli.TodoList(mockIStorage.Object);

            foreach (var item in todoList)
            {
                sut.AllTasks.Add(item);
            }

            // Act
            var actual = sut.ViewTasks();

            // Assert
            Assert.Equal(todoList.Count, actual.Count);

        }

        [Fact]
        public void GetInputWorksWhenUserInputsString()
        {
            // Arrange
            var mock = new Mock<IConsoleWrapper>();
            var expected = "Daniel";
            mock.Setup(x => x.ReadLine()).Returns(expected);
            var sut = new UserInteraction(mock.Object);

            // Act
            var actual = sut.GetInput("Vad heter du?");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetInputKeepsTryingOnEmpty()
        {
            // Arrange
            var mock = new Mock<IConsoleWrapper>();
            var expected = "Christofer";
            mock.SetupSequence(x => x.ReadLine())
                .Returns("")
                .Returns("")
                .Returns("")
                .Returns(expected);
            var sut = new UserInteraction(mock.Object);

            // Act
            var actual = sut.GetInput("Vad heter du?");

            // Assert
            Assert.Equal(expected, actual);
            mock.Verify(x => x.ReadLine(), Times.Exactly(4));
        }
    }
}