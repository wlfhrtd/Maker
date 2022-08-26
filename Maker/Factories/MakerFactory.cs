using Maker.Components.Makers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Factories
{
    internal static class MakerFactory
    {     
        public static IMaker CreateMaker(string choiceInput)
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
