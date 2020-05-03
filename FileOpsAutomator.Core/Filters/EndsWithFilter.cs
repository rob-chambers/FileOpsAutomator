using System;
using System.ComponentModel;

namespace FileOpsAutomator.Core.Filters
{
    [Description("ends with")]
    public class EndsWithFilter : Filter
    {
        public EndsWithFilter(string name, string extension) 
            : base(name, extension)
        {
        }

        public override bool Matches(string name, string extension)
        {
            return name.EndsWith(Name, StringComparison.InvariantCultureIgnoreCase) &&
                Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
