using Maker.Components.Makers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components.Makers
{
    public sealed class EntityMaker : AbstractMaker, IMaker
    {
        // TODO

        // askForX/Question abstraction?
        // future/existing files
        // print/list options, types etc
        // +Repository at some point (EF) !!!

        protected override void Interact()
        {
            // prolly should introduce kinda IPresentable with Print()/Greet() for all makers to greet/show help
            // or just add method to AbstractMaker
            Console.WriteLine(">>> Entity Maker <<<");
            Console.WriteLine("Enter class name:");

            string className = Console.ReadLine();

            if (string.IsNullOrEmpty(className)) throw new ArgumentNullException(className);

            Input.ClassName = className;

            AskForProperties();
        }

        protected override void Validate()
        {
            if (!Validator.ValidateClassName(Input.ClassName))
            {
                throw new ArgumentOutOfRangeException(Input.ClassName);
            }
        }

        // more templates prolly e.g for fields, other access modifiers etc
        // or just follow symfony/maker bundle as 'true way'
        // (private fields with public get/set; for C# in would be -
        // private backing field with public property {get;set;}  )
        private const string PUBLIC_PROPERTY_TEMPLATE = "\t\tpublic {0} {1} {{ get; set; }}";

        protected override void Generate()
        {
            // currently FileManager contains template path; should be here, per Maker based
            // but not just path since Templates folder presents only for this project
            FileManager.LoadTemplate(Output);
            Generator.GenerateNamespace(FileManager, Output);
            Generator.GenerateClassname(Input, Output);
            Generator.GenerateProperties(Input, Output, PUBLIC_PROPERTY_TEMPLATE);
        }

        protected override void Save()
        {
            FileManager.Flush(Input, Output);
        }

        private void AskForProperties()
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

                while (true)
                {
                    Console.WriteLine("Enter property type:");
                    Console.WriteLine("Type ? to list available types.");
                    type = Console.ReadLine();

                    if (string.IsNullOrEmpty(type))
                    {
                        Console.WriteLine("Wrong input.");
                        continue;
                    }

                    if (type == "?")
                    {
                        ListPropertyTypes();
                        continue;
                    }

                    break;
                }

                Input.Properties.Add(name, type);
                Console.WriteLine();
                Console.WriteLine("Add another property ->");
            }
        }
        // TODO add Class type choice handling - Assembly scan for ALL types is prolly overkill
        // but if user enters class name as string it is possible just to put this string on %CLASSNAME% place
        // and as bonus generate 'using' statement after TryFind(string className) from Assembly
        private void ListPropertyTypes()
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  bool\n" +
                              "  byte\n" +
                              "  sbyte\n" +
                              "  char\n" +
                              "  decimal\n" +
                              "  double\n" +
                              "  float\n" +
                              "  int\n" +
                              "  uint\n" +
                              "  nint\n" +
                              "  nuint\n" +
                              "  long\n" +
                              "  ulong\n" +
                              "  short\n" +
                              "  ushort\n" +
                              "  object\n" +
                              "  string\n" +
                              "  dynamic\n" +
                              "  class\n");
            Console.ForegroundColor = currentColor;
        }
    }
}
