using NovelNest.ApplicationLogic.Interfaces.NavigationService;

namespace NovelNest.UserInterface.Common.NavigationService
{
    public class NavigationService : INavigationService
    {
        public void NavigateToMainWindow()
        {
            var window = new MainWindow();
            if (window != null)
            {
                window.ShowDialog();
            }
        }
    }
}
