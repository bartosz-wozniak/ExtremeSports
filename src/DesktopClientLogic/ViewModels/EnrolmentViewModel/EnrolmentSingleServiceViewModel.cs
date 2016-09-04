using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;
using Shared.Filters;

namespace DesktopClientLogic.ViewModels.EnrolmentViewModel
{
    public class EnrolmentSingleServiceViewModel : ViewModelBase
    {
        private readonly CustomerProxy _customerProxy;
        private readonly EmployeeProxy _employeeProxy;
        private readonly ServicesProxy _serviceProxy;
        private readonly ServiceTypeProxy _serviceTypeProxy;
        private string _filter;
        private ServiceType _selectedServiceType;

        public EnrolmentSingleServiceViewModel()
        {
            _customerProxy = new CustomerProxy();
            _serviceProxy = new ServicesProxy();
            _serviceTypeProxy = new ServiceTypeProxy();
            _employeeProxy = new EmployeeProxy();
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            EnrolCommand = new RelayCommand(Enrol, EnrolCanExcecute);
            Initialize();
        }

        public ICommand OnPageLoadCommand { get; set; }
        public ICommand EnrolCommand { get; set; }
        public ObservableCollection<ServiceType> ServiceTypes { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public ObservableCollection<Employee> Instructors { get; set; }
        public Employee SelectedInstructor { get; set; }
        public SolidColorBrush AccentColor => new SolidColorBrush(AppearanceManager.Current.AccentColor);
        public DateTime? Date { get; set; }
        public bool PaymentDone { get; set; }

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                GetCustomers();
            }
        }

        public ServiceType SelectedServiceType
        {
            get { return _selectedServiceType; }
            set
            {
                _selectedServiceType = value;
                GetInstructors();
            }
        }

        private async void Initialize()
        {
            ServiceTypes = await _serviceTypeProxy.GetServiceTypes(new ServiceTypeFilter {IsCourse = false});
            OnPropertyChanged("ServiceTypes");
            GetCustomers();
        }

        private void OnPageLoad(object obj)
        {
            OnPropertyChanged("AccentColor");
            Initialize();
        }

        private async void GetCustomers()
        {
            Customers = await _customerProxy.GetCustomers(Filter);
            OnPropertyChanged("Customers");
        }

        private async void Enrol(object obj)
        {
            if (Date != null)
            {
                var s = new SingleService
                {
                    Customer = SelectedCustomer,
                    Date = Date.Value,
                    ServiceTypeId = SelectedServiceType.Id,
                    InstructorId = SelectedInstructor.Id
                };
                await _serviceProxy.EnrolSingleService(s);
            }
            NavigationCommands.GoToPage.Execute("/Home.xaml", null);
        }

        private bool EnrolCanExcecute(object obj)
        {
            return PaymentDone && Date != null && SelectedCustomer != null && SelectedServiceType != null &&
                   SelectedInstructor != null;
        }

        private async void GetInstructors()
        {
            Instructors = await _employeeProxy.GetEmployees(string.Empty);
            OnPropertyChanged("Instructors");
        }
    }
}