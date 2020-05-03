using FileOpsAutomator.Core.Rules;
using System.Collections.ObjectModel;

namespace FileOpsAutomator.UI.ViewModels
{
    public class RulesCollection : ObservableCollection<Rule>
    {
        public RulesCollection()
        {
            //Add(new MoveRule
            //{
            //    SourceFolder = @"c:\temp",
            //    DestinationFolder = @"c:\temp\dest",
            //    Operation = new Operation { Name = "Move" },
            //    Filter = new Filter("All Files", new[] { "*.*" })
            //});
        }
    }
}