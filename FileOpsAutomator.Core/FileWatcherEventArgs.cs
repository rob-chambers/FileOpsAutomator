using System;

namespace FileOpsAutomator.Core
{
    public class FileWatcherEventArgs : EventArgs
    {
        public FileWatcherEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
