using System;

namespace FileOpsAutomator.Core.Filters
{
    public class StartsWithFilter : Filter
    {
        public StartsWithFilter(string name, string extension)
            : base(name, extension)
        {
        }

        public override string Description => "starts with";

        public override FilterType Type => FilterType.StartsWith;

        public override bool Matches(string name, string extension)
        {
            return name.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase) &&
                Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
