using System;
using FileOpsAutomator.Core.Filters;

namespace FileOpsAutomator.Core.Rules
{
    public abstract class Rule
    {
        public event EventHandler<string> Processed;

        public string SourceFolder { get; set; }

        public Filter Filter { get; set; }

        public bool IsEnabled { get; set; } = true;

        public abstract void Process(string fileName, string extension);

        protected void OnProcessed(string message)
        {
            Processed?.Invoke(this, message);
        }
    }
}
