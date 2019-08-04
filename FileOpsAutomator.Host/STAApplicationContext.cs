using System.Windows.Forms;

namespace FileOpsAutomator.Host
{
    internal sealed class STAApplicationContext : ApplicationContext
    {
        private ViewManager _viewManager;
        private IFileWatcher _fileWatcher;

        public STAApplicationContext()
        {
            _fileWatcher = new FileWatcher();
            _viewManager = new ViewManager(_fileWatcher);

            _fileWatcher.StatusChanged += _viewManager.OnStatusChange;

            _fileWatcher.Start();
        }

        // Called from the Dispose method of the base class
        protected override void Dispose(bool disposing)
        {
            if ((_fileWatcher != null) && (_viewManager != null))
            {
                _fileWatcher.StatusChanged -= _viewManager.OnStatusChange;
            }

            if (_fileWatcher != null)
            {
                _fileWatcher.Terminate();
            }

            _fileWatcher = null;
            _viewManager = null;
        }
    }
}