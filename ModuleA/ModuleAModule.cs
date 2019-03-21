using ModuleA.ViewModels;
using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;
        public ModuleAModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            //IRegion region = regionManager.Regions["ContentRegion"];

            //var tabA = containerProvider.Resolve<TabView>();
            //SetTitle(tabA, "Tab A");
            //region.Add(tabA);

            //var tabB = containerProvider.Resolve<TabView>();
            //SetTitle(tabB, "Tab B");
            //region.Add(tabB);

            //var tabC = containerProvider.Resolve<TabView>();
            //SetTitle(tabC, "Tab C");
            //region.Add(tabC);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(TabView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        void SetTitle(TabView tab, string title)
        {
            (tab.DataContext as TabViewVM).Title = title;
        }
    }
}