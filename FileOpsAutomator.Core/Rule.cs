namespace FileOpsAutomator.Core
{
    public abstract class Rule
    {
        public string SourceFolder { get; set; }

        public Operation Operation { get; set; }

        public Filter Filter { get; set; }
    }

    public class MoveRule : Rule
    {
        public string DestinationFolder { get; set; }
    }
}
