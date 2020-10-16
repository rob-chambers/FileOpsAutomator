using System;
using FileOpsAutomator.Core.Filters;
using Newtonsoft.Json;

namespace FileOpsAutomator.Core.Rules
{
    [JsonConverter(typeof(RuleConverter))]
    public abstract class Rule
    {
        public event EventHandler<string> Processed;

        public virtual RuleType Type => RuleType.Undefined;

        public string SourceFolder { get; set; }

        public Filter Filter { get; set; }

        public bool IsEnabled { get; set; } = true;

        public bool Open { get; set; }

        public abstract void Process(string fullPath, string extension);

        protected void OnProcessed(string message)
        {
            Processed?.Invoke(this, message);
        }
    }
}
