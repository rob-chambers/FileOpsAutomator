using System;

namespace FileOpsAutomator.Host
{
    public enum FileWatcherStatus
    {
        Stopped,
        Running
    }

    public interface IFileWatcher
    {
        FileWatcherStatus Status { get; set; }

        event EventHandler StatusChanged;

        void Start();

        void Stop();

        void Terminate();
    }
}
