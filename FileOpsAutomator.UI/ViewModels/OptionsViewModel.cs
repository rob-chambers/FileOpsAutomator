namespace FileOpsAutomator.UI.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private string _name;

        public OptionsViewModel()
        {
            Name = "Options";
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
    }
}
