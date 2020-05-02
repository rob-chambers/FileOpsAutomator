using System;
using System.Collections.Generic;

namespace FileOpsAutomator.Core
{
    public class Filter
    {
        public Filter(string name, IEnumerable<string> extensions)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Extensions = extensions ?? throw new ArgumentNullException(nameof(extensions));
        }

        public string Name { get; private set; }

        public IEnumerable<string> Extensions { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}