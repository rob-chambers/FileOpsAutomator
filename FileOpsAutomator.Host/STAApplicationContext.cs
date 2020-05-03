using System.Windows.Forms;

namespace FileOpsAutomator.Host
{
    internal sealed class STAApplicationContext : ApplicationContext
    {
        private readonly IViewManager _viewManager;

        public STAApplicationContext(IViewManager viewManager)
        {
            _viewManager = viewManager;
            _viewManager.Initialize();
        }

        // Called from the Dispose method of the base class
        protected override void Dispose(bool disposing)
        {
            if (_viewManager != null)
            {
                _viewManager.Terminate();
            }
        }
    }
}