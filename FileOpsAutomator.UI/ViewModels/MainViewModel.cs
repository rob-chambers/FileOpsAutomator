namespace FileOpsAutomator.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RulesViewModel RulesViewModel { get; set; } = new RulesViewModel();

        public OptionsViewModel OptionsViewModel { get; set; } = new OptionsViewModel();

        public string Title { get; set; } = "File Ops Automator";
    }
}
