using FileOpsAutomator.Core;
using FileOpsAutomator.Core.Helpers;
using FileOpsAutomator.Core.Rules;
using System;
using System.Windows.Forms;

namespace FileOpsAutomator.Host
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs args)
        {
            if (FileManager == null) return;
            InitForm();
            LoadRules();
        }

        private void LoadRules()
        {
            FileManager.ReadRulesAsync().Wait();
            foreach (var rule in FileManager.Rules)
            {
                RulesListView.Items.Add(MapRuleToListItem(rule));
            }
        }

        private ListViewItem MapRuleToListItem(Rule rule)
        {
            var displayName = AttributeHelper.GetDisplayName(rule);
            var description = AttributeHelper.GetDescription(rule);

            var item = new ListViewItem(displayName);
            item.SubItems.Add(rule.SourceFolder);
            item.SubItems.Add(rule.Filter.Name);
            item.SubItems.Add(rule.Filter.Description);
            item.SubItems.Add(rule.Filter.Extension);
            
            // TODO: Make this SOLID
            if (rule is MoveRule moveRule)
            {
                item.SubItems.Add(moveRule.DestinationFolder);
                item.SubItems.Add(rule.Open.ToString());
            }

            return item;
        }

         public IFileManager FileManager { get; set; }

        private void InitForm()
        {            
            RulesListView.View = View.Details;
            RulesListView.FullRowSelect = true;
            RulesListView.GridLines = true;
            InitColumns();
        }

        private void InitColumns()
        {
            RulesListView.Columns.Add("Type", 120);
            RulesListView.Columns.Add("Source Folder", 280);
            RulesListView.Columns.Add("File Name", 180);
            RulesListView.Columns.Add("Filter Type", 150);
            RulesListView.Columns.Add("Extension", 120);
            RulesListView.Columns.Add("Destination Folder", 280);
            RulesListView.Columns.Add("Open?", 50);
        }
    }
}
