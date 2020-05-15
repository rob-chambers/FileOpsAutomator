using FileOpsAutomator.Core;
using Ninject.Modules;

namespace FileOpsAutomator.Host
{
    internal class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<STAApplicationContext>().To<STAApplicationContext>().InThreadScope();
            Bind<IViewManager>().To<ViewManager>().InThreadScope();
            Bind<IRuleRepository>().To<JsonRuleRepository>().InThreadScope();
        }
    }
}
