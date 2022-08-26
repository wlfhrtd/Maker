using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class Generator
    {
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
            output.TemplateOutput.Replace("%NAMESPACE%", fileManager.ns + ".Models");
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
