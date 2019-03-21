using Prism.Mvvm;
using System.ComponentModel.Composition;
using UsingCompositeCommands.Core;

namespace X_Signal.Main.ViewModels
{
    [Export(typeof(MainViewVM))]
    public class MainViewVM : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }
        public MainViewVM(IApplicationCommands applicationCommands)
        {
            ApplicationCommands = applicationCommands;
        }
    }
}
