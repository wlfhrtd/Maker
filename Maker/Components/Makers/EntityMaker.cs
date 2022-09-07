using Maker.Components.Makers.Base;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
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
        // +Repository at some point (EF) !!!

        protected override void Interact()
        {
            // prolly should introduce kinda IPresentable with Print()/Greet() for all makers to greet/show help
            // or just add method to AbstractMaker
            Console.WriteLine(">>> Entity Maker <<<");
            
            AskForClassName();

            AskForProperties();
        }

        private void AskForClassName()
        {
            string className;

            while (true)
            {
                Console.WriteLine("Enter class name:");

                className = Console.ReadLine();

                if (string.IsNullOrEmpty(className))
                {
                    Output.PrintEmptyInputError();
                    
                    continue;
                }

                if (!Validator.ValidateIdentifier(className))
                {
                    var currentColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Validator.INVALID_CLASS_NAME);
                    Console.ForegroundColor = currentColor;

                    continue;
                }

                break;
            }

            Input.ClassName = className.UCFirst();
        }

        protected override void Validate()
        {
            //if (!Validator.ValidateClassName(Input.ClassName))
            //{
            //    throw new ArgumentOutOfRangeException(Input.ClassName);
            //}
        }

        // more templates prolly e.g for fields, other access modifiers etc
        // or just follow symfony/maker bundle as 'true way'
        // (private fields with public get/set; for C# in would be -
        // private backing field with public property {get;set;}  )
        private const string PUBLIC_PROPERTY_TEMPLATE = "\t\tpublic {0} {1} {{ get; set; }}";
        CodeCompileUnit codeCompileUnit;
        CodeGeneratorOptions options;
        protected override void Generate()
        {
            // currently FileManager contains template path; should be here, per Maker based
            // but not just path since Templates folder presents only for this project
            //FileManager.LoadTemplate(Output);
            //Generator.GenerateNamespace(FileManager, Output);
            //Generator.GenerateClassname(Input, Output);
            //Generator.GenerateProperties(Input, Output, PUBLIC_PROPERTY_TEMPLATE);

            codeCompileUnit = Generator.Generate(FileManager, Input, TypesDictionary);
            options = new()
            {
                BracingStyle = "C",
                // IndentString = "\t",
                BlankLinesBetweenMembers = false,
            };
        }

        

        protected override void Save()
        {
            // FileManager.Flush(Input, Output);
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            FileManager.SaveFile(Input, codeDomProvider, codeCompileUnit, options);
        }

        private void AskForProperties()
        {
            Console.WriteLine("Let's add some properties!");
            Console.WriteLine("Leave name input empty and press Enter to stop");

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
                        Output.PrintEmptyInputError();
                        continue;
                    }

                    if (type == "?")
                    {
                        ListPropertyTypes();
                        continue;
                    }

                    type = type.ToLower();

                    if (!TypesDictionary.Types.ContainsKey(type))
                    {
                        Console.WriteLine("Type is not avaiable.");
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
            StringBuilder sb = new(30 * TypesDictionary.Types.Count);

            foreach (var pair in TypesDictionary.Types)
            {
                sb.AppendLine($"  {pair.Key} ({pair.Value})");
            }

            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sb.ToString());
            Console.ForegroundColor = currentColor;
        }
    }
}
