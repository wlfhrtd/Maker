using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class FileManager
    {
        // TODO

        // fileSystem
        // parseTemplate? => Generator?
        // dumpFile
        // 'exists' methods
        // relative/absolute paths methods
        // resolving paths for current/future objects
        // paths paths paths

        private readonly string ns;
        public readonly string projectRootPath;
        public readonly string modelsPath;          
        public readonly string classTemplateFile;

        public FileManager()
        {
            projectRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            modelsPath = projectRootPath + Path.DirectorySeparatorChar + "Models";
            Directory.CreateDirectory(modelsPath);
            // templates will be stored somewhere else inside Maker app itself
            // not in another project(using package) folders
            string templatesPath = projectRootPath + Path.DirectorySeparatorChar +
                                   "Resources" + Path.DirectorySeparatorChar + "Templates";
            classTemplateFile = templatesPath + Path.DirectorySeparatorChar + "Class.txt";

            // Original maker bundle: forces App\Entity namespace
            // Decision: force %PROJECT_NS%\Models namespace
            // Reason: simplicity, uniformity
            ns = Assembly.GetExecutingAssembly().GetName().Name + ".Models";
        }


        public string Namespace => ns;
        public void LoadTemplate(Output output)
        {
            using (StreamReader sr = new(classTemplateFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    output.TemplateOutput.AppendLine(line);
                }
            }
        }

        internal void Flush(Input input, Output output)
        {
            using (StreamWriter sw = new(GetEntityFilePath(input)))
            {
                sw.Write(output.TemplateOutput.ToString());
            }
        }

        internal void SaveFile(Input input, CodeDomProvider codeDomProvider, CodeCompileUnit codeCompileUnit, CodeGeneratorOptions options)
        {
            string codeFileName = GetEntityFilePath(input);
            IndentedTextWriter tw = new(new StreamWriter(codeFileName, false), "\t");
            codeDomProvider.GenerateCodeFromCompileUnit(codeCompileUnit, tw, options);
            tw.Close();
        }

        private string GetEntityFilePath(Input input)
        {
            return modelsPath + Path.DirectorySeparatorChar + input.ClassName + ".cs";
        }
    }
}
