namespace Tally
{
    public class TallyBin
    {
        public TallyBin(string caption, bool isInScope, bool isDone)
        {
            Caption = caption;
            IsInScope = isInScope;
            IsDone = isDone;
        }

        public string Caption { get; }
        public bool IsInScope { get; }
        public bool IsDone { get; }
    }
}