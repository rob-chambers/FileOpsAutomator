using Newtonsoft.Json;
using System;

namespace FileOpsAutomator.Core.Filters
{
    [JsonConverter(typeof(FilterConverter))]
    public abstract class Filter
    {
        protected Filter(string name, string extension)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Extension = extension ?? throw new ArgumentNullException(nameof(extension));
        }

        public virtual FilterType Type => FilterType.Undefined;

        public string Name { get; private set; }

        [JsonIgnore]
        public abstract string Description { get; }

        public string Extension { get; private set; }

        public abstract bool Matches(string name, string extension);

        public override string ToString()
        {
            return Name;
        }
    }
}