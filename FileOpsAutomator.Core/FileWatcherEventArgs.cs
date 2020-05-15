using System;

namespace FileOpsAutomator.Core
{
    public class FileWatcherEventArgs : EventArgs
    {
        public FileWatcherEventArgs(string fullPath)
        {
            FullPath = fullPath;
        }

        public string FullPath { get; }
    }
}
