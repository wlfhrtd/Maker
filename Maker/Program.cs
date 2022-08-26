using Maker.Components;
using Maker.Components.Makers;
using Maker.Factories;
using System.Text;

namespace Maker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** C# Maker *****");
            Console.WriteLine();
            Console.WriteLine("\tAvaiable commands:");
            Console.WriteLine();
            Console.WriteLine(" make:entity");
            Console.WriteLine(" make:crud");
            Console.WriteLine();

            string choice = Console.ReadLine();

            if (string.IsNullOrEmpty(choice)) throw new Exception("Wrong input.");

            IMaker maker = MakerFactory.CreateMaker(choice);
            maker.Run();
        }
    }
}

// TODO
// naming string extensions eg pascal snake case
// add namespaces assembly scan with default value "Models" at some point