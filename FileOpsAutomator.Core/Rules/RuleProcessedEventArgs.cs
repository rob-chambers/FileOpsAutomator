using System;

namespace FileOpsAutomator.Core.Rules
{
    public class RuleProcessedEventArgs : EventArgs
    {
        public RuleProcessedEventArgs(Rule rule, string message)
        {
            Rule = rule;
            Message = message;
        }

        public Rule Rule { get; }

        public string Message { get; }
    }
}
