using System;

namespace FileOpsAutomator.Host
{
    internal class FileWatcher : IFileWatcher
    {
        private FileWatcherStatus _status;

        public event EventHandler StatusChanged;

        public FileWatcherStatus Status
        {
            get => _status;
            set
            {
                if (_status == value) return;
                _status = value;
                RaiseOnStatusChanged();
            }
        }

        protected void RaiseOnStatusChanged()
        {
            if (StatusChanged == null) return;
            StatusChanged(this, EventArgs.Empty);
        }        

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
        }
    }
}