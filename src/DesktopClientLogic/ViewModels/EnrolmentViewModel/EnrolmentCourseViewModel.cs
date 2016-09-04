using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;
using Shared.Filters;

namespace DesktopClientLogic.ViewModels.EnrolmentViewModel
{
    public class EnrolmentCourseViewModel : ViewModelBase
    {
        private readonly CustomerProxy _customerProxy;
        private readonly ServicesProxy _servicesProxy;
        private readonly ServiceTypeProxy _serviceTypeProxy;
        private string _filter;
        private ServiceType _selectedServiceType;
        private SelectStage _stage = SelectStage.Customer;

        public EnrolmentCourseViewModel()
        {
            _serviceTypeProxy = new ServiceTypeProxy();
            _servicesProxy = new ServicesProxy();
            _customerProxy = new CustomerProxy();
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            EnrolCommand = new RelayCommand(Enrol, EnrolCanExcecute);
            NextCommand = new RelayCommand(Next, NextCanExcecute);
            PrevCommand = new RelayCommand(Prev, PrevCanExcecute);
            Initialize();
        }

        public ICommand OnPageLoadCommand { get; set; }
        public ICommand EnrolCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PrevCommand { get; set; }

        public ObservableCollection<ServiceType> ServiceTypes { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public Course SelectedCourse { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public SolidColorBrush AccentColor => new SolidColorBrush(AppearanceManager.Current.AccentColor);
        public bool PaymentDone { get; set; }

        public Visibility SelectCustomerVisibility
            => _stage == SelectStage.Customer ? Visibility.Visible : Visibility.Collapsed;

        public Visibility SelectCourseTypeVisibility
            => _stage == SelectStage.CourseType ? Visibility.Visible : Visibility.Collapsed;

        public Visibility SelectCourseVisibility
            => _stage == SelectStage.Course ? Visibility.Visible : Visibility.Collapsed;

        public Visibility NextVisibility => _stage != SelectStage.Course ? Visibility.Visible : Visibility.Collapsed;

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
                GetCourses();
            }
        }

        private bool PrevCanExcecute(object arg)
        {
            return _stage != SelectStage.Customer;
        }

        private bool NextCanExcecute(object arg)
        {
            return _stage != SelectStage.Course;
        }

        private void Prev(object obj)
        {
            _stage--;
            OnPropertyChanged("SelectCustomerVisibility");
            OnPropertyChanged("SelectCourseVisibility");
            OnPropertyChanged("SelectCourseTypeVisibility");
            OnPropertyChanged("NextVisibility");
            OnPropertyChanged("SelectedCustomer");
            OnPropertyChanged("SelectedServiceType");
        }

        private void Next(object obj)
        {
            _stage++;
            OnPropertyChanged("SelectCustomerVisibility");
            OnPropertyChanged("SelectCourseVisibility");
            OnPropertyChanged("SelectCourseTypeVisibility");
            OnPropertyChanged("NextVisibility");
            OnPropertyChanged("SelectedCustomer");
            OnPropertyChanged("SelectedServiceType");
        }

        private async void Initialize()
        {
            ServiceTypes = await _serviceTypeProxy.GetServiceTypes(new ServiceTypeFilter {IsCourse = true});
            OnPropertyChanged("ServiceTypes");
            GetCustomers();
            _stage = SelectStage.Customer;
        }

        private async void GetCourses()
        {
            Courses = await _servicesProxy.GetCourses(new ServiceFilter {ServiceTypeId = SelectedServiceType?.Id});
            OnPropertyChanged("Courses");
        }

        private void OnPageLoad(object obj)
        {
            OnPropertyChanged("AccentColor");
            Initialize();
        }

        private async void GetCustomers()
        {
            Customers = await _customerProxy.GetCustomers(string.Empty);
            OnPropertyChanged("Customers");
        }

        private async void Enrol(object obj)
        {
            await _servicesProxy.EnrolCourse(SelectedCustomer.Id, SelectedCourse.Id);
            NavigationCommands.GoToPage.Execute("/Home.xaml", null);
        }

        private bool EnrolCanExcecute(object obj)
        {
            return PaymentDone && SelectedCourse != null && SelectedCustomer != null && SelectedServiceType != null;
        }
    }
}