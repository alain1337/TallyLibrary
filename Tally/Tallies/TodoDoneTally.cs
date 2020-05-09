using System;
using System.Text;

namespace Tally.Tallies
{
    public class TodoDoneTally<T> : TallyBase<T>, ITally<T>
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

        readonly Func<T, bool> _binSelector;
        public override int BinSelector(T item)
        {
            return _binSelector(item) ? 1 : 0;
        }
    }
}
