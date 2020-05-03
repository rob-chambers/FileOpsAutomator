namespace FileOpsAutomator.Core
{
    public class FileWatcher : IFileWatcher
    {
        public FileWatcherStatus Status { get; set; }

        public void Start()
        {
            Status = FileWatcherStatus.Running;
        }

        public void Stop()
        {
            Status = FileWatcherStatus.Stopped;
        }

        public void Terminate()
        {
            Status = FileWatcherStatus.Stopped;
        }
    }
}