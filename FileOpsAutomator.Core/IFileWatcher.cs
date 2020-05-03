namespace FileOpsAutomator.Core
{
    public interface IFileWatcher
    {
        FileWatcherStatus Status { get; set; }
        void Start();
        void Stop();
        void Terminate();
    }
}
