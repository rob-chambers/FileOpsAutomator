namespace FileOpsAutomator.Core.Rules
{
    public abstract class Rule
    {
        public string SourceFolder { get; set; }

        public Operation Operation { get; set; }

        public Filter Filter { get; set; }
    }
}
