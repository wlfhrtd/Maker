using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components.Makers.Base
{
    public abstract class AbstractMaker : IMaker
    {
        private static readonly Lazy<FileManager> fileManager = new(() => new FileManager());
        protected FileManager FileManager => fileManager.Value;

        private static readonly Lazy<Generator> generator = new(() => new Generator());
        protected Generator Generator => generator.Value;

        private static readonly Lazy<Input> input = new(() => new Input());
        protected Input Input => input.Value;

        private static readonly Lazy<Output> output = new(() => new Output());
        protected Output Output => output.Value;

        private static readonly Lazy<Validator> validator = new(() => new Validator());
        protected Validator Validator => validator.Value;

        private static readonly Lazy<TypesDictionary> typesDictionary = new(() => new TypesDictionary());
        protected TypesDictionary TypesDictionary => typesDictionary.Value;

        public void Run()
        {
            Interact();
            Validate();
            Generate();
            Save();
        }

        protected abstract void Interact();
        protected abstract void Validate();
        protected abstract void Generate();
        protected abstract void Save();    
    }
}
