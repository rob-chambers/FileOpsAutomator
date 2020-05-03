using System;
using System.ComponentModel;

namespace FileOpsAutomator.Core.Filters
{
    [Description("exact match")]
    public class ExactMatchFilter : Filter
    {
        public ExactMatchFilter(string name, string extension)
            : base(name, extension)
        {
        }

        public override bool Matches(string name, string extension)
        {
            return Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) &&
                Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
