using Maker.Components.Makers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Factories
{
    internal class MakerFactory
    {
        // add dependencies to constructors: FileManager, Input, Generator
        public IMaker GetMaker(string choiceInput)
        {
            return choiceInput switch
            {
                "make:entity" => new EntityMaker(),
                "make:crud" => new CrudMaker(),
                _ => throw new ArgumentOutOfRangeException(choiceInput),
            };
        }
    }
}
