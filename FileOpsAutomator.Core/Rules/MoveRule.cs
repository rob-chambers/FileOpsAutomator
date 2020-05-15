using System.ComponentModel;
using System.IO;

namespace FileOpsAutomator.Core.Rules
{
    [DisplayName("Move")]
    [Description("Moves a file from one folder to another")]
    public class MoveRule : Rule
    {
        public string DestinationFolder { get; set; }

        public override RuleType Type => RuleType.MoveRule;

        public override void Process(string fullPath, string extension)
        {
            if (!IsEnabled) return;

            var folder = Path.GetDirectoryName(fullPath);
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);

            if (!Filter.Matches(Path.Combine(folder, fileWithoutExtension), extension))
            {
                return;
            }
            
            if (folder != SourceFolder) return;

            if (!File.Exists(fullPath)) return;

            var destination = Path.Combine(DestinationFolder, Path.GetFileName(fullPath));
            if (File.Exists(destination)) return;

            File.Move(fullPath, destination);

            RaiseProcessedEvent(fullPath);
        }

        private void RaiseProcessedEvent(string fullPath)
        {
            var fileNameOnly = Path.GetFileName(fullPath);
            var sourceFolderOnly = GetDirectoryNameWithoutFullPath(SourceFolder);
            var destFolderOnly = GetDirectoryNameWithoutFullPath(DestinationFolder);

            var message = $"{fileNameOnly} was moved from {sourceFolderOnly} to {destFolderOnly}";
            OnProcessed(message);
        }

        private string GetDirectoryNameWithoutFullPath(string value)
        {
            var index = value.LastIndexOf(Path.DirectorySeparatorChar);
            return index > -1 
                ? value.Substring(index + 1) 
                : value;
        }
    }
}
