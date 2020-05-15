using System;

namespace FileOpsAutomator.Core.Filters
{
    public class ExactMatchFilter : Filter
    {
        public ExactMatchFilter(string name, string extension)
            : base(name, extension)
        {
        }

        public override string Description => "exact match";

        public override FilterType Type => FilterType.ExactMatch;

        public override bool Matches(string name, string extension)
        {
            return Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) &&
                Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
