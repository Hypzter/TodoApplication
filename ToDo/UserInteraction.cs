using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Cli
{
    public class UserInteraction : IUserInteraction
    {
        private readonly IConsoleWrapper consoleWrapper;

        public UserInteraction(IConsoleWrapper consoleWrapper)
        {
            this.consoleWrapper = consoleWrapper;
        }

        public void DisplayMessage(string s)
        {
            consoleWrapper.WriteLine(s);
            consoleWrapper.WriteLine("~~~~~~~~");
        }

        public string GetInput(string prompt)
        {
            string input = null;

            consoleWrapper.WriteLine(prompt);

            while (string.IsNullOrWhiteSpace(input))
            {
                input = consoleWrapper.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    consoleWrapper.WriteLine("Incorrect input, please try again");
                    consoleWrapper.WriteLine(prompt);
                }
            }
            return input;
        }
    }
}
