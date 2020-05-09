using System;
using System.Text;

namespace Tally.Tallies
{
    public class TodoDoneTally<T> : ITally<T>
    {
        public TodoDoneTally(Func<T,bool> isDoneFunc, string caption = null)
        {
            Definition = new TallyDefinition(caption ?? "TodoDone", new[]
            {
                new TallyBin("Todo"),
                new TallyBin("Done")
            });
            _binSelector = isDoneFunc;
        }

        public TallyDefinition Definition { get; }
        readonly Func<T, bool> _binSelector;
        public virtual int BinSelector(T item)
        {
            return _binSelector(item) ? 1 : 0;
        }
    }
}
