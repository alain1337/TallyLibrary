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
                new TallyBin("Todo", true, false),
                new TallyBin("Todo", true, true)
            });

            BinSelector = item => isDoneFunc(item) ? 1 : 0;
        }
    }
}
