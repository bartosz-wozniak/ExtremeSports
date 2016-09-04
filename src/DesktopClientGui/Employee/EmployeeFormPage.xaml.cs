using DesktopClientLogic.ViewModels.EmployeeViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using static System.Int32;

namespace DesktopClientGui.Employee
{
    /// <summary>
    ///     Interaction logic for EmployeeFormPage.xaml
    /// </summary>
    public partial class EmployeeFormPage : IContent
    {
        public EmployeeFormPage()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            DataContext = new EmployeeFormViewModel();
            var vm = (EmployeeFormViewModel) DataContext;
            if (!string.IsNullOrEmpty(e.Fragment))
            {
                var employeeId = Parse(e.Fragment);
                if (employeeId > 0)
                    vm.Id = employeeId;
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