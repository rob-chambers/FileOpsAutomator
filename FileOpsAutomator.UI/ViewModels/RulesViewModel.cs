using FileOpsAutomator.Core;

namespace FileOpsAutomator.UI.ViewModels
{
    public class RulesViewModel : ViewModelBase
    {
        public RulesViewModel()
        {
            
        }

        public RulesCollection Rules { get; set; } = new RulesCollection();
    }
}