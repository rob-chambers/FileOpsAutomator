using FileOpsAutomator.Core.Rules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileOpsAutomator.Core
{
    public interface IFileManager
    {
        event EventHandler StatusChanged;
        event EventHandler<RuleProcessedEventArgs> RuleProcessed;

        ICollection<Rule> Rules { get; }
        FileWatcherStatus Status { get; }

        Task ReadRulesAsync();
        void Start();
        void Stop();        
    }
}