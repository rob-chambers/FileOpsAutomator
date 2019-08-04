using System;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace FileOpsAutomator.Host
{
    internal sealed class ViewManager
    {
        private const int BalloonTimeoutInMilliseconds = 3000;

        // This allows code to be run on a GUI thread
        private Window _hiddenWindow;
        private IContainer _components;

        // The Windows system tray class
        private NotifyIcon _notifyIcon;
        private IFileWatcher _fileWatcher;

        //private WpfFormLibrary.View.AboutView _aboutView;
        //private WpfFormLibrary.ViewModel.AboutViewModel _aboutViewModel;
        //private WpfFormLibrary.View.StatusView _statusView;
        //private WpfFormLibrary.ViewModel.StatusViewModel _statusViewModel;

        private ToolStripMenuItem _startWatcherMenuItem;
        private ToolStripMenuItem _rulesMenuItem;
        private ToolStripMenuItem _optionsMenuItem;
        private ToolStripMenuItem _stopWatcherMenuItem;
        private ToolStripMenuItem _exitMenuItem;

        public ViewManager(IFileWatcher fileWatcher)
        {
            _fileWatcher = fileWatcher;

            _components = new Container();
            _notifyIcon = new NotifyIcon(_components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                //Icon = FileOpsAutomator.Host.Properties.Resources.NotReadyIcon,
                Text = "System Tray App: Device Not Present",
                Visible = true,
            };

            _notifyIcon.ContextMenuStrip.Opening += OnContextMenuStripOpening;
            _notifyIcon.DoubleClick += OnNotifyIconDoubleClick;
            _notifyIcon.MouseUp += OnNotifyIconMouseUp;

            //_aboutViewModel = new WpfFormLibrary.ViewModel.AboutViewModel();
            //_statusViewModel = new WpfFormLibrary.ViewModel.StatusViewModel();

            //_statusViewModel.Icon = AppIcon;
            //_aboutViewModel.Icon = _statusViewModel.Icon;

            _hiddenWindow = new Window();
            _hiddenWindow.Hide();
        }

        private ImageSource AppIcon
        {
            get
            {
                var icon = (_fileWatcher.Status == FileWatcherStatus.Running) 
                    ? Properties.Resources.ReadyIcon 
                    : Properties.Resources.NotReadyIcon;

                return Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void UpdateStatusView()
        {
            //if ((_statusViewModel != null) && (_fileWatcher != null))
            //{
            //    List<KeyValuePair<string, bool>> flags = _fileWatcher.StatusFlags;
            //    var statusItems = flags.Select(n => new KeyValuePair<string, string>(n.Key, n.Value.ToString())).ToList();
            //    statusItems.Insert(0, new KeyValuePair<string, string>("Device", _fileWatcher.DeviceName));
            //    statusItems.Insert(1, new KeyValuePair<string, string>("Status", _fileWatcher.Status.ToString()));
            //    _statusViewModel.SetStatusFlags(statusItems);
            //}
        }

        public void OnStatusChange(object sender, EventArgs args)
        {
            UpdateStatusView();

            switch (_fileWatcher.Status)
            {
                case FileWatcherStatus.Running:
                    _notifyIcon.Text = "Running";
                    _notifyIcon.Icon = Properties.Resources.ReadyIcon;
                    DisplayStatusMessage("Running");
                    break;

                case FileWatcherStatus.Stopped:
                    _notifyIcon.Text = "Stopped";
                    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                    DisplayStatusMessage("Stopped");
                    break;

                default:
                    _notifyIcon.Text = "Stopped"; //_fileWatcher.DeviceName + ": -";
                    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                    break;
            }
        }

        private void DisplayStatusMessage(string text)
        {
            _hiddenWindow.Dispatcher.Invoke(delegate
            {
                _notifyIcon.BalloonTipText = text;
                
                // The timeout is ignored on recent Windows
                _notifyIcon.ShowBalloonTip(BalloonTimeoutInMilliseconds);
            });
        }

        private void OnStartStopClick(object sender, EventArgs e)
        {
            if (_fileWatcher.Status == FileWatcherStatus.Running)
            {
                _fileWatcher.Stop();
            }
            else
            {
                _fileWatcher.Start();
            }
        }

        private void OnOptionsClick(object sender, EventArgs e)
        {

        }

        private void OnRulesClick(object sender, EventArgs e)
        {

        }

        private ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, string tooltipText, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null)
            {
                item.Click += eventHandler;
            }

            item.ToolTipText = tooltipText;
            return item;
        }

        private void OnShowStatusItemClick(object sender, EventArgs e)
        {
            //ShowStatusView();
            System.Windows.MessageBox.Show("Status view");
        }

        private void ShowAboutView()
        {
            System.Windows.MessageBox.Show("About view");
            //if (_aboutView == null)
            //{
            //    _aboutView = new WpfFormLibrary.View.AboutView();
            //    _aboutView.DataContext = _aboutViewModel;
            //    _aboutView.Closing += ((arg_1, arg_2) => _aboutView = null);
            //    _aboutView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //    _aboutView.Show();
            //}
            //else
            //{
            //    _aboutView.Activate();
            //}
            //_aboutView.Icon = AppIcon;

            //_aboutViewModel.AddVersionInfo("Hardware", _fileWatcher.DeviceName);
            //_aboutViewModel.AddVersionInfo("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            //_aboutViewModel.AddVersionInfo("Serial Number", "142573462354");
        }

        private void OnShowAboutDialogClick(object sender, EventArgs e)
        {
            ShowAboutView();
        }

        private void OnExit(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            ShowAboutView();
        }

        private void OnNotifyIconMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var methodInfo = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(_notifyIcon, null);
        }
        
        private void OnContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            var contextMenuStrip = _notifyIcon.ContextMenuStrip;
            if (contextMenuStrip.Items.Count == 0)
            {
                _startWatcherMenuItem = ToolStripMenuItemWithHandler("Start", "Start all automation rules", OnStartStopClick);
                contextMenuStrip.Items.Add(_startWatcherMenuItem);

                _stopWatcherMenuItem = ToolStripMenuItemWithHandler("Stop", "Stop all automation rules from running", OnStartStopClick);
                contextMenuStrip.Items.Add(_stopWatcherMenuItem);

                contextMenuStrip.Items.Add(new ToolStripSeparator());

                _rulesMenuItem = ToolStripMenuItemWithHandler("Rules", "Specify automation rules", OnRulesClick);
                contextMenuStrip.Items.Add(_rulesMenuItem);

                _optionsMenuItem = ToolStripMenuItemWithHandler("Options", "Edit options", OnOptionsClick);                
                contextMenuStrip.Items.Add(_optionsMenuItem);

                contextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&About", "Shows the About dialog", OnShowAboutDialogClick));
                contextMenuStrip.Items.Add(new ToolStripSeparator());
                _exitMenuItem = ToolStripMenuItemWithHandler("&Exit", "Exits System Tray App", OnExit);
                contextMenuStrip.Items.Add(_exitMenuItem);
            }

            SetMenuItemsEnabledStatus();
        }

        private void SetMenuItemsEnabledStatus()
        {
            switch (_fileWatcher.Status)
            {
                case FileWatcherStatus.Running:
                    _startWatcherMenuItem.Enabled = false;
                    _stopWatcherMenuItem.Enabled = true;
                    _exitMenuItem.Enabled = true;
                    break;

                case FileWatcherStatus.Stopped:
                    _startWatcherMenuItem.Enabled = true;
                    _stopWatcherMenuItem.Enabled = false;
                    _exitMenuItem.Enabled = true;
                    break;

                default:
                    throw new Exception($"Unexpected status: {_fileWatcher.Status}");
            }
        }
    }
}