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
        // +Repository at some point (EF)

        protected override void Interact()
        {
            // prolly should introduce kinda IPresentable with Print()/Greet() for all makers to greet/show help
            Console.WriteLine(">>> Entity Maker <<<");
            Console.WriteLine("Enter class name:");
            
            string className = Console.ReadLine();

            if(string.IsNullOrEmpty(className)) throw new ArgumentNullException(className);

            Input.ClassName = className;

            AskForProperties();
        }

        protected override void Validate()
        {
            // TODO
        }

        // more templates prolly e.g for fields, other access modifiers etc
        private const string PUBLIC_PROPERTY_TEMPLATE = "\t\tpublic {0} {1} {{ get; set; }}";

        protected override void Generate()
        {
            // currently FileManager contains template path; should be here, per Maker based
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

            // list .net types

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
                
                Console.WriteLine("Enter property type:");
                type = Console.ReadLine();

                if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(type);

                Input.Properties.Add(name, type);
                Console.WriteLine();
                Console.WriteLine("Add another property");
            }
        }
    }
}
