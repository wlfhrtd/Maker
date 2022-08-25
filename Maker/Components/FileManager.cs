using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    internal class FileManager
    {
        // TODO

        // fileSystem
        // input?
        // parseTemplate? => Generator?
        // dumpFile
        // 'exists' methods
        // relative/absolute paths methods
        // resolving paths for current/future objects
        // paths paths paths


        public readonly string projectRootPath;
        public readonly string modelsPath;
        public readonly string ns;
        public readonly string classTemplateFile;

        public FileManager()
        {
            projectRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            modelsPath = projectRootPath + Path.DirectorySeparatorChar + "Models";
            Directory.CreateDirectory(modelsPath);
            string templatesPath = projectRootPath + Path.DirectorySeparatorChar +
                                   "Resources" + Path.DirectorySeparatorChar + "Templates";
            classTemplateFile = templatesPath + Path.DirectorySeparatorChar + "Class.txt";
            ns = Assembly.GetExecutingAssembly().GetName().Name;
        }


        public string ClassName { get; set; }
        public string EntityPath => modelsPath + Path.DirectorySeparatorChar + ClassName + ".cs";
    }
}
