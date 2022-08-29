using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class Generator
    {
        internal CodeCompileUnit Generate(FileManager fileManager, Input input)
        {
            CodeCompileUnit codeCompileUnit = new();

            CodeNamespace codeNamespace = new(fileManager.Namespace);
            codeCompileUnit.Namespaces.Add(codeNamespace);

            CodeTypeDeclaration generatedClass = new(input.ClassName);
            codeNamespace.Types.Add(generatedClass);

            foreach (var field in input.Properties)
            {
                // TODO
                CodeMemberField f = new(ResolveType(field.Value), field.Key); // type => name
                generatedClass.Members.Add(f);

                CodeMemberProperty p = new()
                {
                    Name = field.Key, // check styling; prolly need to introduce Pascal-case helper
                    Type = new CodeTypeReference("System." + field.Value),
                    Attributes = MemberAttributes.Public,         
                };
                p.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Key)));
                p.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Key), new CodePropertySetValueReferenceExpression()));
                generatedClass.Members.Add(p);
            }

            return codeCompileUnit;
        }

        private string? ResolveType(string typeFromUserInput)
        {
            return typeFromUserInput.ToLowerInvariant() switch
            {
                "string" => typeof(string).FullName,
                "boolean" => typeof(bool).FullName,
                "integer" => typeof(int).FullName,
                "short" => typeof(short).FullName,
                "long" => typeof(long).FullName,
                "float" => typeof(float).FullName,
                "double" => typeof(double).FullName,
                "decimal" => typeof(decimal).FullName,
                _ => throw new ArgumentOutOfRangeException(typeFromUserInput),
                // array types
                // datetime types
                // other?
            };
        }

        internal void GenerateProperties(Input input, Output output, string propertyTemplate)
        {
            foreach (var property in input.Properties)
            {
                output.PropertiesOutput.AppendLine(string.Format(propertyTemplate, property.Value, property.Key));
            }

            output.TemplateOutput.Replace("%BEGIN%", output.PropertiesOutput.ToString().TrimEnd());
        }

        internal void GenerateNamespace(FileManager fileManager, Output output)
        {
            // output.TemplateOutput.Replace("%NAMESPACE%", fileManager.ns + ".Models");

            CodeNamespace ns = new(fileManager.Namespace + ".Models");

        }

        internal void GenerateClassname(Input input, Output output)
        {
            output.TemplateOutput.Replace("%CLASSNAME%", input.ClassName);
        }

        // TODO

        // generateClass() Generate a new file for a class from a template
        // generateFile() Generate a normal file from a template
        // getFileContentsForPendingOperation
        // createClassNameDetails Creates a helper object to get data about a class name
        // getRootDirectory
        // addOperation
        // hasPendingOperations
        // writeChanges() Actually writes and file changes that are pending
        // getRootNamespace
        // generateController
        // generateTemplate

    }
}
