using DesktopClientLogic.ViewModels.SportTypeViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using static System.Int32;

namespace DesktopClientGui.SportType
{
    /// <summary>
    ///     Interaction logic for SportTypeForm.xaml
    /// </summary>
    public partial class SportTypeForm : IContent
    {
        public SportTypeForm()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            DataContext = new SportTypeFormViewModel();
            var vm = (SportTypeFormViewModel) DataContext;
            if (!string.IsNullOrEmpty(e.Fragment))
            {
                var sportTypeId = Parse(e.Fragment);
                if (sportTypeId > 0)
                    vm.SportTypeId = sportTypeId;
            }
            else
                vm.SportTypeId = null;
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