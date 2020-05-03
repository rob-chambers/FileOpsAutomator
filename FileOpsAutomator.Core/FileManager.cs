using FileOpsAutomator.Core.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileOpsAutomator.Core
{
    internal class FileManager : IFileManager
    {
        private readonly IRuleRepository _ruleRepository;
        private IReadOnlyCollection<IFileWatcher> _fileWatchers;
        private FileWatcherStatus _status;

        public FileManager(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public event EventHandler StatusChanged;
        public event EventHandler<RuleProcessedEventArgs> RuleProcessed;

        public FileWatcherStatus Status 
        {
            get => _status; 
            private set
            {
                if (_status == value) return;
                _status = value;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ICollection<Rule> Rules { get; private set; }

        public async Task ReadRulesAsync()
        {
            Rules = (await _ruleRepository.GetAllAsync()).ToList();
            var list = new List<IFileWatcher>();
            foreach (var rule in Rules)
            {
                var watcher = new FileWatcher(rule.SourceFolder);
                watcher.Changed += OnWatcherChanged;
                list.Add(watcher);
                rule.Processed += OnRuleProcessed;
            }

            _fileWatchers = new ReadOnlyCollection<IFileWatcher>(list);
        }

        private void OnRuleProcessed(object sender, string message)
        {
            RuleProcessed?.Invoke(this, new RuleProcessedEventArgs((Rule)sender, message));
        }

        private void OnWatcherChanged(object sender, FileWatcherEventArgs e)
        {
            ProcessRules(e);
        }

        private void ProcessRules(FileWatcherEventArgs e)
        {
            foreach (var rule in Rules)
            {
                var extension = Path.GetExtension(e.Path);
                rule.Process(e.Path, extension);
            }
        }

        public void Start()
        {
            foreach (var watcher in _fileWatchers)
            {
                watcher.Start();
            }

            Status = FileWatcherStatus.Running;
        }

        public void Stop()
        {
            foreach (var watcher in _fileWatchers)
            {
                watcher.Stop();
            }

            Status = FileWatcherStatus.Stopped;
        }
    }
}
