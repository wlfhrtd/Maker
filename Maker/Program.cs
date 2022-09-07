using Maker.Components;
using Maker.Components.Makers;
using Maker.Factories;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Text;

namespace Maker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // move greeting(); compose commands list
            Console.WriteLine("***** C# Maker *****");
            Console.WriteLine();
            Console.WriteLine("\tAvaiable commands:");
            Console.WriteLine();
            Console.WriteLine(" make:entity");
            Console.WriteLine(" make:crud");
            Console.WriteLine();

            string choice;
            
            Output output = new(); // TODO remove it or make static; or w/e with Errors

            IMaker maker;

            while (true)
            {
                choice = Console.ReadLine();

                if (string.IsNullOrEmpty(choice))
                {
                    output.PrintEmptyInputError();

                    continue;
                }

                try
                {
                    maker = MakerFactory.CreateMaker(choice);
                }
                catch (ArgumentOutOfRangeException)
                {
                    output.PrintCommandNotAvailableError();

                    continue;
                }

                break;
            }
   
            maker.Run();
        }
    }
}

// TODO
// naming string extensions eg pascal snake case
// add namespaces assembly scan with default value "Models" at some point

#region example
//internal class Program
//{
//    static void Main(string[] args)
//    {
//        string codeFileName = "..\\..\\..\\MyClass.cs";
//        CodeCompileUnit codeCompileUnit = CreateClass();
//        CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
//        IndentedTextWriter tw = new IndentedTextWriter(new System.IO.StreamWriter(codeFileName, false), "\t");
//        CodeGeneratorOptions options = new()
//        {
//            BracingStyle = "C",
//            // IndentString = "\t",
//            BlankLinesBetweenMembers = false,
//        };
//        codeDomProvider.GenerateCodeFromCompileUnit(codeCompileUnit, tw, options);
//        tw.Close();
//    }

//    public static CodeCompileUnit CreateClass()
//    {
//        CodeCompileUnit compileUnit = new CodeCompileUnit();

//        CodeNamespace samples = new CodeNamespace("MyNamespace");
//        compileUnit.Namespaces.Add(samples);

//        CodeTypeDeclaration class1 = new CodeTypeDeclaration("MyClass");
//        samples.Types.Add(class1);

//        CodeMemberField field1 = new CodeMemberField("System.String", "myField");
//        class1.Members.Add(field1);

//        CodeMemberProperty property1 = new CodeMemberProperty();
//        property1.Name = "MyProperty";
//        property1.Type = new CodeTypeReference("System.String");
//        property1.Attributes = MemberAttributes.Public;
//        property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "myField")));
//        property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "myField"), new CodePropertySetValueReferenceExpression()));
//        class1.Members.Add(property1);

//        CodeMemberMethod method1 = new CodeMemberMethod();
//        method1.Name = "MyMethod";
//        method1.ReturnType = new CodeTypeReference("System.String");
//        method1.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "myParameter"));
//        method1.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression("myParameter")));
//        class1.Members.Add(method1);

//        return compileUnit;
//    }
//}
#endregion