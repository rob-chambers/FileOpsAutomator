using System;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using FileOpsAutomator.Core;
using FileOpsAutomator.Core.Rules;

namespace FileOpsAutomator.Host
{
    internal sealed class ViewManager : IViewManager
    {
        private const int BalloonTimeoutInMilliseconds = 1700;

        private readonly IFileManager _fileManager;

        // This allows code to be run on a GUI thread
        private Window _hiddenWindow;
        private IContainer _components;

        // The Windows system tray class
        private NotifyIcon _notifyIcon;
        
        private ToolStripMenuItem _startWatcherMenuItem;
        private ToolStripMenuItem _rulesMenuItem;
        private ToolStripMenuItem _stopWatcherMenuItem;
        private ToolStripMenuItem _exitMenuItem;

        public ViewManager(IFileManager fileManager)
        {
            _fileManager = fileManager;
            _fileManager.StatusChanged += OnStatusChanged;
            _fileManager.RuleProcessed += OnRuleProcessed;
        }

        private void OnRuleProcessed(object sender, RuleProcessedEventArgs e)
        {
            DisplayStatusMessage(e.Message);
        }

        public void Initialize()
        {
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

            _hiddenWindow = new Window();
            _hiddenWindow.Hide();

            _fileManager.ReadRulesAsync().Wait();
            _fileManager.InitWatchers();

            Start();
        }

        private ImageSource AppIcon
        {
            get
            {
                var icon = (_fileManager.Status == FileWatcherStatus.Running) 
                    ? Properties.Resources.ReadyIcon 
                    : Properties.Resources.NotReadyIcon;

                return Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void OnStatusChanged(object sender, EventArgs args)
        {
            switch (_fileManager.Status)
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
            if (_fileManager.Status == FileWatcherStatus.Running)
            {
                Stop();                
            }
            else
            {
                Start();
            }
        }

        private void Start()
        {
            _fileManager.Start();            
        }

        private void Stop()
        {
            _fileManager.Stop();
        }

        private void OnRulesClick(object sender, EventArgs e)
        {
            var form = new MainForm();
            form.FileManager = _fileManager;
            form.ShowDialog();
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

        private void ShowAboutView()
        {
            System.Windows.Forms.MessageBox.Show("About view");
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

                contextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&About", "Shows the About dialog", OnShowAboutDialogClick));
                contextMenuStrip.Items.Add(new ToolStripSeparator());
                _exitMenuItem = ToolStripMenuItemWithHandler("&Exit", "Exits System Tray App", OnExit);
                contextMenuStrip.Items.Add(_exitMenuItem);
            }

            SetMenuItemsEnabledStatus();
        }

        private void SetMenuItemsEnabledStatus()
        {
            switch (_fileManager.Status)
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
                    throw new Exception($"Unexpected status: {_fileManager.Status}");
            }
        }

        public void Terminate()
        {
            Stop();
        }
    }
}