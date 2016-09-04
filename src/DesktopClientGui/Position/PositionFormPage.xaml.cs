using DesktopClientLogic.ViewModels.PositionViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using static System.Int32;

namespace DesktopClientGui.Position
{
    /// <summary>
    ///     Interaction logic for PositionForm.xaml
    /// </summary>
    public partial class PositionForm : IContent
    {
        public PositionForm()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            DataContext = new PositionFormViewModel();
            var vm = (PositionFormViewModel) DataContext;
            if (!string.IsNullOrEmpty(e.Fragment))
            {
                var positionId = Parse(e.Fragment);
                if (positionId > 0)
                    vm.PositionId = positionId;
            }
            else
                vm.PositionId = null;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}