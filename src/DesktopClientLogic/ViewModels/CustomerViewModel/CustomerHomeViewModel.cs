using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.CustomerViewModel
{
    /// <summary>
    ///     View Model Class for Customer Home Page
    /// </summary>
    /// <remarks>
    ///     Inherites from View Model Base
    /// </remarks>
    public class CustomerHomeViewModel : ViewModelBase
    {
        private readonly CustomerProxy _customerProxy;

        /// <summary>
        ///     Private field Cancellation Token Source.
        ///     Used to cancel task displaying error message
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        ///     Private field for Customer
        /// </summary>
        private ObservableCollection<Customer> _customers;

        /// <summary>
        ///     Private Field for error Text
        /// </summary>
        private string _errorText;

        /// <summary>
        ///     Filter (name, surname, personalIdentityNumber)
        ///     Updates table of Customers
        /// </summary>
        private string _filter;

        /// <summary>
        ///     Constructor initializes commands
        /// </summary>
        public CustomerHomeViewModel()
        {
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            RemoveCustomerCommand = new RelayCommand(RemoveCustomer);
            EditCustomerCommand = new RelayCommand(EditCustomer);
            _cts = new CancellationTokenSource();
            _customerProxy = new CustomerProxy();
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
        public ICommand RemoveCustomerCommand { get; set; }

        /// <summary>
        ///     Command bound to Edit button
        /// </summary>
        public ICommand EditCustomerCommand { get; set; }

        /// <summary>
        ///     Currently selected row - Customer
        /// </summary>
        public Customer SelectedCustomer { get; set; }

        /// <summary>
        ///     Collection of Customers
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
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
        ///     Gets updated List of Customers
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void OnPageLoad(object o)
        {
            Customers = await _customerProxy.GetCustomers(Filter);
            if (Customers == null)
                ErrorText = "Nie znaleziono klientów.";
        }

        /// <summary>
        ///     Method redirects to edit page and fills form
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private void EditCustomer(object o)
        {
            NavigationCommands.GoToPage.Execute("/Customer/CustomerFormPage.xaml#" + SelectedCustomer.Id, null);
        }

        /// <summary>
        ///     Removes Selected Customer if it is possible
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void RemoveCustomer(object o)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var success = await _customerProxy.RemoveCustomer(SelectedCustomer.Id);
            OnPageLoad(null);
            ErrorText = null;
            if (success) return;
            ErrorText = "Usuwanie klienta nie powiodło się. Sprawdź, czy żaden kurs nie jest do niego przypisany.";
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