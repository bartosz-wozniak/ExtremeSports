using DesktopClientLogic.ViewModels.CustomerViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using static System.Int32;

namespace DesktopClientGui.Customer
{
    /// <summary>
    ///     Interaction logic for CustomerFormPage.xaml
    /// </summary>
    public partial class CustomerFormPage : IContent
    {
        public CustomerFormPage()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            DataContext = new CustomerFormViewModel();
            var vm = (CustomerFormViewModel) DataContext;
            if (!string.IsNullOrEmpty(e.Fragment))
            {
                var customerId = Parse(e.Fragment);
                if (customerId > 0)
                    vm.Id = customerId;
            }
            else
                vm.Id = null;
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