using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.CodeDom;

namespace Maker.Components
{
    public class Validator
    {
        // TODO

        // validateClassName against reserved keywords
        // check utf8 encoding
        // check names starting - underscores, numbers etc
        // constraints: blank, length etc
        // validate property/field names

        /*
         * An identifier must start with a letter or an underscore
After the first character, it may contain numbers, letters, connectors, etc
If the identifier is a keyword, it must be prepended with “@”
         */

        public const string INVALID_CLASS_NAME = "\n  An identifier must start with a letter or an underscore.\n" +
                                                   "  After the first character, it may contain numbers, letters, connectors, etc.\n" +
                                                   "  If the identifier is a keyword, it must be prepended with “@”.\n";

        public bool ValidateIdentifier(string identifier)
        {
            // for classNames; should work for variables too 
            return CodeGenerator.IsValidLanguageIndependentIdentifier(identifier);
        }

        // doesn't work, at least for something like "@$#*&$#*@#INVALIDCLASSNAME"
        public string SanitizeClassName(string className)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            string fixedName = codeProvider.CreateValidIdentifier(className);
            return fixedName;
            // CodeTypeDeclaration codeType = new CodeTypeDeclaration(sFixedName);
        }
    }
}
