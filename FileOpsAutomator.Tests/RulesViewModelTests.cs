using FileOpsAutomator.Core;
using FileOpsAutomator.Core.Rules;
using FileOpsAutomator.UI.ViewModels;
using Xunit;

namespace FileOpsAutomator.Tests
{
    public class RulesViewModelTests
    {
        [Fact]
        public void a()
        {
            var vm = new RulesViewModel();
            vm.Rules.Add(new MoveRule
            {
                SourceFolder = "c:\temp",
                DestinationFolder = @"c:\temp\dest",
                Operation = new Operation { Name = "Move" },
                Filter = new Filter("All Files", new[] { "*.*" })
            });


        }
        
    }
}
