using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.EmployeeViewModel
{
    /// <summary>
    ///     View Model Class for Employee Home Page
    /// </summary>
    /// <remarks>
    ///     Inherites from View Model Base
    /// </remarks>
    public class EmployeeHomeViewModel : ViewModelBase
    {
        private readonly EmployeeProxy _employeeProxy;

        /// <summary>
        ///     Private field Cancellation Token Source.
        ///     Used to cancel task displaying error message
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        ///     Private field for Employee
        /// </summary>
        private ObservableCollection<Employee> _employees;

        /// <summary>
        ///     Private Field for error Text
        /// </summary>
        private string _errorText;

        /// <summary>
        ///     Filter (name, surname, personalIdentityNumber)
        ///     Updates table of Employee
        /// </summary>
        private string _filter;

        /// <summary>
        ///     Constructor initializes commands
        /// </summary>
        public EmployeeHomeViewModel()
        {
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            RemoveEmployeeCommand = new RelayCommand(RemoveEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee);
            _cts = new CancellationTokenSource();
            _employeeProxy = new EmployeeProxy();
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPageLoad(null);
            }
        }

        /// <summary>
        ///     Command bound to Page Loaded event
        /// </summary>
        public ICommand OnPageLoadCommand { get; set; }

        /// <summary>
        ///     Command bound to Remove Button
        /// </summary>
        public ICommand RemoveEmployeeCommand { get; set; }

        /// <summary>
        ///     Command bound to Edit button
        /// </summary>
        public ICommand EditEmployeeCommand { get; set; }

        /// <summary>
        ///     Currently selected row - Employee
        /// </summary>
        public Employee SelectedEmployee { get; set; }

        /// <summary>
        ///     Collection of Employees
        /// </summary>
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }

        /// <summary>
        ///     Text displayed when record cannot be deleted
        /// </summary>
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        /// <summary>
        ///     Gets updated List of Employees
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void OnPageLoad(object o)
        {
            Employees = await _employeeProxy.GetEmployees(Filter);
            if (Employees == null)
                ErrorText = "Nie znaleziono pracowników.";
        }

        /// <summary>
        ///     Method redirects to edit page and fills form
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private void EditEmployee(object o)
        {
            NavigationCommands.GoToPage.Execute("/Employee/EmployeeFormPage.xaml#" + SelectedEmployee.Id, null);
        }

        /// <summary>
        ///     Removes Selected Employee if it is possible
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void RemoveEmployee(object o)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var success = await _employeeProxy.RemoveEmployee(SelectedEmployee.Id);
            OnPageLoad(null);
            ErrorText = null;
            if (success) return;
            ErrorText = "Usuwanie pracownika nie powiodło się. Sprawdź, czy żaden kurs nie jest do niego przypisany.";
            await RemoveErrorMesage(_cts.Token);
        }

        /// <summary>
        ///     Cancel displaying error message after specific time
        /// </summary>
        /// <param name="cts">Cancelation Token</param>
        /// <returns>False if cancelled by CT and true otherwise</returns>
        private async Task<bool> RemoveErrorMesage(CancellationToken cts)
        {
            try
            {
                await Task.Delay(5000, cts);
                ErrorText = null;
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
    }
}