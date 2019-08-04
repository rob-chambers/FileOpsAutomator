using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;

namespace FileOpsAutomator.Host
{
    public class ViewManager
    {
        // This allows code to be run on a GUI thread
        private System.Windows.Window _hiddenWindow;

        private IContainer _components;
        // The Windows system tray class
        private NotifyIcon _notifyIcon;
        private IFileWatcher _fileWatcher;

        //private WpfFormLibrary.View.AboutView _aboutView;
        //private WpfFormLibrary.ViewModel.AboutViewModel _aboutViewModel;
        //private WpfFormLibrary.View.StatusView _statusView;
        //private WpfFormLibrary.ViewModel.StatusViewModel _statusViewModel;

        private ToolStripMenuItem _startDeviceMenuItem;
        private ToolStripMenuItem _stopDeviceMenuItem;
        private ToolStripMenuItem _exitMenuItem;

        public ViewManager(IFileWatcher fileWatcher)
        {
            System.Diagnostics.Debug.Assert(fileWatcher != null);

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

        ImageSource AppIcon
        {
            get
            {
                var icon = (_fileWatcher.Status == FileWatcherStatus.Running) 
                    ? Properties.Resources.ReadyIcon 
                    : Properties.Resources.NotReadyIcon;
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
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
                //case FileWatcherStatus.Initialised:
                //    _notifyIcon.Text = _fileWatcher.DeviceName + ": Ready";
                //    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                //    DisplayStatusMessage("Idle");
                //    break;
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

                //case DeviceStatus.Starting:
                //    _notifyIcon.Text = _fileWatcher.DeviceName + ": Starting";
                //    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                //    DisplayStatusMessage("Starting");
                //    break;
                //case DeviceStatus.Uninitialised:
                //    _notifyIcon.Text = _fileWatcher.DeviceName + ": Not Ready";
                //    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                //    break;
                //case DeviceStatus.Error:
                //    _notifyIcon.Text = _fileWatcher.DeviceName + ": Error Detected";
                //    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                //    break;

                default:
                    _notifyIcon.Text = "Stopped"; //_fileWatcher.DeviceName + ": -";
                    _notifyIcon.Icon = Properties.Resources.NotReadyIcon;
                    break;
            }

            //var icon = AppIcon;
            //if (_aboutView != null)
            //{
            //    _aboutView.Icon = AppIcon;
            //}
            //if (_statusView != null)
            //{
            //    _statusView.Icon = AppIcon;
            //}
        }

        private void DisplayStatusMessage(string text)
        {
            _hiddenWindow.Dispatcher.Invoke(delegate
            {
                _notifyIcon.BalloonTipText = text;
                
                // The timeout is ignored on recent Windows
                _notifyIcon.ShowBalloonTip(3000);
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

        private void ShowStatusView()
        {
            //if (_statusView == null)
            //{
            //    _statusView = new WpfFormLibrary.View.StatusView();
            //    _statusView.DataContext = _statusViewModel;

            //    _statusView.Closing += ((arg_1, arg_2) => _statusView = null);
            //    _statusView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //    _statusView.Show();
            //    UpdateStatusView();
            //}
            //else
            //{
            //    _statusView.Activate();
            //}
            //_statusView.Icon = AppIcon;
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

        private void showHelpItem_Click(object sender, EventArgs e)
        {
            ShowAboutView();
        }

        private void showWebSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.CodeProject.com/");
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

            if (_notifyIcon.ContextMenuStrip.Items.Count == 0)
            {
                _startDeviceMenuItem = ToolStripMenuItemWithHandler("Start Device", "Starts the device", OnStartStopClick);
                _notifyIcon.ContextMenuStrip.Items.Add(_startDeviceMenuItem);
                _stopDeviceMenuItem = ToolStripMenuItemWithHandler("Stop Device", "Stops the device", OnStartStopClick);
                _notifyIcon.ContextMenuStrip.Items.Add(_stopDeviceMenuItem);
                _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Device S&tatus", "Shows the device status dialog", OnShowStatusItemClick));
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&About", "Shows the About dialog", showHelpItem_Click));
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Code Project &Web Site", "Navigates to the Code Project Web Site", showWebSite_Click));
                _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                _exitMenuItem = ToolStripMenuItemWithHandler("&Exit", "Exits System Tray App", OnExit);
                _notifyIcon.ContextMenuStrip.Items.Add(_exitMenuItem);
            }

            SetMenuItems();
        }

        private void SetMenuItems()
        {
            switch (_fileWatcher.Status)
            {
                //case DeviceStatus.Initialised:
                //    _startDeviceMenuItem.Enabled = true;
                //    _stopDeviceMenuItem.Enabled = false;
                //    _exitMenuItem.Enabled = true;
                //    break;
                //case DeviceStatus.Starting:
                //    _startDeviceMenuItem.Enabled = false;
                //    _stopDeviceMenuItem.Enabled = false;
                //    _exitMenuItem.Enabled = false;
                //    break;
                case FileWatcherStatus.Running:
                    _startDeviceMenuItem.Enabled = false;
                    _stopDeviceMenuItem.Enabled = true;
                    _exitMenuItem.Enabled = true;
                    break;

                //case DeviceStatus.Uninitialised:
                //    _startDeviceMenuItem.Enabled = false;
                //    _stopDeviceMenuItem.Enabled = false;
                //    _exitMenuItem.Enabled = true;
                //    break;
                //case DeviceStatus.Error:
                //    _startDeviceMenuItem.Enabled = false;
                //    _stopDeviceMenuItem.Enabled = false;
                //    _exitMenuItem.Enabled = true;
                //    break;

                default:
                    System.Diagnostics.Debug.Assert(false, "SetButtonStatus() => Unknown state");
                    break;
            }
        }
    }
}