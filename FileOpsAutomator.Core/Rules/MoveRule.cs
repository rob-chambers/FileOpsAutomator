using System.ComponentModel;
using System.IO;

namespace FileOpsAutomator.Core.Rules
{
    [DisplayName("Move")]
    [Description("Moves a file from one folder to another")]
    public class MoveRule : Rule
    {
        public string DestinationFolder { get; set; }

        public override void Process(string path, string extension)
        {
            if (!IsEnabled) return;

            var folder = Path.GetDirectoryName(path);
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(path);

            if (!Filter.Matches(Path.Combine(folder, fileWithoutExtension), extension))
            {
                return;
            }
            
            if (folder != SourceFolder) return;

            if (!File.Exists(path)) return;

            var destination = Path.Combine(DestinationFolder, Path.GetFileName(path));
            File.Move(path, destination);

            RaiseProcessedEvent(path);
        }

        private void RaiseProcessedEvent(string path)
        {
            var fileNameOnly = Path.GetFileName(path);
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
