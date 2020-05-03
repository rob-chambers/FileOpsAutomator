using FileOpsAutomator.Core;
using System.Windows.Forms;

namespace FileOpsAutomator.Host
{
    internal sealed class STAApplicationContext : ApplicationContext
    {
        private readonly IViewManager _viewManager;
        private readonly IFileWatcher _fileWatcher;

        public STAApplicationContext(IViewManager viewManager, IFileWatcher fileWatcher)
        {
            _viewManager = viewManager;
            _viewManager.Initialize();
            _fileWatcher = fileWatcher;
        }

        // Called from the Dispose method of the base class
        protected override void Dispose(bool disposing)
        {
            if (_fileWatcher != null)
            {
                _fileWatcher.Terminate();
            }
        }
    }
}