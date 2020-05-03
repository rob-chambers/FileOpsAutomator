using System;
using System.IO;

namespace FileOpsAutomator.Core
{
    internal class FileWatcher : IFileWatcher
    {
        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string path)
        {
            _watcher = new FileSystemWatcher(path);
            _watcher.Created += OnFileWatcherCreated;            
        }
      
        public event EventHandler<FileWatcherEventArgs> Changed;

        private void OnFileWatcherCreated(object sender, FileSystemEventArgs e)
        {
            Changed?.Invoke(this, new FileWatcherEventArgs(e.FullPath));
        }

        public FileWatcherStatus Status { get; set; }

        public void Start()
        {
            Status = FileWatcherStatus.Running;
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            Status = FileWatcherStatus.Stopped;
            _watcher.EnableRaisingEvents = false;
        }

        public void Terminate()
        {
            Stop();
        }
    }
}