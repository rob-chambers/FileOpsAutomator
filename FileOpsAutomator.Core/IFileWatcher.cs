using System;

namespace FileOpsAutomator.Core
{
    public interface IFileWatcher
    {
        event EventHandler<FileWatcherEventArgs> Changed;
        FileWatcherStatus Status { get; }
        void Start();
        void Stop();
        void Terminate();
    }
}
