using Prism.Unity;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using Prism.Modularity;
using UsingCompositeCommands.Core;
using ModuleA;
using X_Signal.Main.Views;

namespace X_Signal.Main
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleAModule>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }
    }
}
