using System;
using System.ComponentModel;

namespace FileOpsAutomator.Core.Filters
{
    [Description("starts with")]
    public class StartsWithFilter : Filter
    {
        public StartsWithFilter(string name, string extension)
            : base(name, extension)
        {
        }

        public override bool Matches(string name, string extension)
        {
            return name.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase) &&
                Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
