using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    // TODO

    // generateClass() Generate a new file for a class from a template
    // generateFile() Generate a normal file from a template
    // getFileContentsForPendingOperation
    // createClassNameDetails Creates a helper object to get data about a class name
    // addOperation
    // hasPendingOperations
    // writeChanges() Actually writes and file changes that are pending
    // getRootNamespace
    // generateController
    // generateTemplate

    public class Generator
    {
        internal CodeCompileUnit Generate(FileManager fileManager, Input input, TypesDictionary typesDictionary)
        {
            CodeCompileUnit codeCompileUnit = new();

            CodeNamespace codeNamespace = new(fileManager.Namespace);
            codeCompileUnit.Namespaces.Add(codeNamespace);

            CodeTypeDeclaration generatedClass = new(input.ClassName);
            codeNamespace.Types.Add(generatedClass);

            // 'free' id field
            CodeMemberField idField = new()
            {
                Type = new CodeTypeReference(typesDictionary.Types["integer"]),
                Name = "id",
            };
            generatedClass.Members.Add(idField);

            // 'free' Id property
            CodeMemberProperty idProperty = new()
            {
                Name = "Id",
                Type = new CodeTypeReference(typesDictionary.Types["integer"]),
                Attributes = MemberAttributes.Public,
            };
            idProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), "id")));
            generatedClass.Members.Add(idProperty);

            // process properties from Input
            foreach (var field in input.Properties)
            {
                // field.Key => Name, field.Value => type
                CodeMemberField f = new()
                {
                    Type = new CodeTypeReference(typesDictionary.Types[field.Value]),
                    Name = field.Key.LCFirst(),
                };
                generatedClass.Members.Add(f);

                CodeMemberProperty p = new()
                {
                    Name = field.Key.UCFirst(),
                    Type = new CodeTypeReference(typesDictionary.Types[field.Value]),
                    Attributes = MemberAttributes.Public,         
                };
                p.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(), field.Key.LCFirst())));
                p.SetStatements.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            field.Key.LCFirst()), new CodePropertySetValueReferenceExpression()));

                generatedClass.Members.Add(p);
            }
            return codeCompileUnit;
        }

        #region OLD
        //// array types
        //// datetime types
        //// other?
        //private string? ResolveType(string typeFromUserInput)
        //{
        //    return typeFromUserInput.ToLowerInvariant() switch
        //    {
        //        "string" => TypesDictionary.Types["string"],
        //        "boolean" => TypesDictionary.Types["boolean"],
        //        "integer" => TypesDictionary.Types["integer"],
        //        "short" => TypesDictionary.Types["short"],
        //        "long" => TypesDictionary.Types["long"],
        //        "float" => TypesDictionary.Types["float"],
        //        "double" => TypesDictionary.Types["double"],
        //        _ => throw new ArgumentOutOfRangeException(typeFromUserInput),              
        //    };
        //}

        //internal void GenerateProperties(Input input, Output output, string propertyTemplate)
        //{
        //    foreach (var property in input.Properties)
        //    {
        //        output.PropertiesOutput.AppendLine(string.Format(propertyTemplate, property.Value, property.Key));
        //    }

        //    output.TemplateOutput.Replace("%BEGIN%", output.PropertiesOutput.ToString().TrimEnd());
        //}

        //internal void GenerateNamespace(FileManager fileManager, Output output)
        //{
        //    // output.TemplateOutput.Replace("%NAMESPACE%", fileManager.ns + ".Models");

        //    CodeNamespace ns = new(fileManager.Namespace + ".Models");

        //}

        //internal void GenerateClassname(Input input, Output output)
        //{
        //    output.TemplateOutput.Replace("%CLASSNAME%", input.ClassName);
        //}
        #endregion
    }
}
