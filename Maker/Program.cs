using Maker.Components;
using Maker.Components.Makers;
using Maker.Factories;
using System.Text;

namespace Maker
{
    internal class Program
    {
        private static void AskForProperties(ref Dictionary<string, string> properties)
        {
            Console.WriteLine("Let's add some properties!");
            Console.WriteLine("Leave input empty and press Enter to stop");

            string name, type;

            while (true)
            {
                Console.WriteLine("Enter property name:");
                name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("STOPPED");
                    return;
                }
                // list .net types
                Console.WriteLine("Enter property type:");
                type = Console.ReadLine();

                if (string.IsNullOrEmpty(type))
                {
                    throw new ArgumentNullException(type);
                }

                properties.Add(name, type);
                Console.WriteLine();
                Console.WriteLine("Add another property");
            }
        }

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

            MakerFactory makerFactory = new();
            IMaker maker = makerFactory.GetMaker(choice);
            // Interact, Generate, Persist mmm?


            // Refactor
            //
            FileManager fileManager = new();
            fileManager.ClassName = Console.ReadLine();

            Dictionary<string, string> properties = new(20);
            AskForProperties(ref properties);

            StringBuilder output = new(1024);

            using (StreamReader sr = new(fileManager.classTemplateFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    output.AppendLine(line);
                }
            }
            
            // EntityMaker? Generator? Input(static?)? Output(ui ux blabla)?? startup command resolver?
            // names string extensions eg pascal snake case etc?
            // btw add namespaces assembly scan with default value "Models" at some point
            StringBuilder propertiesOutput = new(1024);
            foreach (var property in properties)
            {
                propertiesOutput.AppendLine($"\t\tpublic {property.Value} {property.Key} {{ get; set; }}");
            }

            output.Replace("%NAMESPACE%", fileManager.ns + ".Models");
            output.Replace("%CLASSNAME%", fileManager.ClassName);
            output.Replace("%BEGIN%", propertiesOutput.ToString().TrimEnd());

            using (StreamWriter sw = new(fileManager.EntityPath))
            {
                sw.Write(output.ToString());
            }
        }
    }
}