using NovelNest.UserInterface.ViewModels.StartScreenViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.UserInterface.Services.StartScreenService
{
    public class StartScreenWindowViewModelService
    {
        private StartScreenViewModels _startScreenViewModel;

        public StartScreenViewModels GetStartScreen()
        {
            if(_startScreenViewModel is null )
                _startScreenViewModel = new StartScreenViewModels();
            return _startScreenViewModel;
        }
    }
}
