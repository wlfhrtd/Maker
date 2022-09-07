using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class Output
    {
        private Lazy<StringBuilder> templateOutput = new(() => new StringBuilder(1024));
        private Lazy<StringBuilder> propertiesOutput = new(() => new StringBuilder(1024));

        public StringBuilder TemplateOutput => templateOutput.Value;
        public StringBuilder PropertiesOutput => propertiesOutput.Value;

        private const string EMPTY_INPUT_ERROR = "\nInput is empty.\n";
        private const string COMMAND_NOT_AVAILABLE = "\nCommand is not available\n";

        public void PrintEmptyInputError()
        {
            PrintError(EMPTY_INPUT_ERROR);
        }

        public void PrintCommandNotAvailableError()
        {
            PrintError(COMMAND_NOT_AVAILABLE);
        }

        private void PrintError(string message)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = currentColor;
        }
    }
}
