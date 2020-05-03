using Ninject.Modules;

namespace FileOpsAutomator.Core
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileManager>().To<FileManager>().InThreadScope();
        }
    }
}
