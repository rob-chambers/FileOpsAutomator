using System;

namespace FileOpsAutomator.Core.Filters
{
    public class EndsWithFilter : Filter
    {
        public EndsWithFilter(string name, string extension) 
            : base(name, extension)
        {
        }

        public override string Description => "ends with";

        public override bool Matches(string name, string extension)
        {
            return name.EndsWith(Name, StringComparison.InvariantCultureIgnoreCase) &&
                Extension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
