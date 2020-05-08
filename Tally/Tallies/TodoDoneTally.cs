using System;
using System.Text;

namespace Tally.Tallies
{
    public class TodoDoneTally<T> : TallyBase<T>, ITally<T>
    {
        public TodoDoneTally(Func<T,bool> isDoneFunc, string caption = null)
        {
            Definition = new TallyDefinition<T>(caption ?? "TodoDone", new[]
            {
                new TallyBin("Todo"),
                new TallyBin("Todo")
            }, item => isDoneFunc(item) ? 1 : 0);

        }
    }
}
