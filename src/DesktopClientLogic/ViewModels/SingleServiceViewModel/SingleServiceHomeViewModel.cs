using System.Collections.ObjectModel;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;
using Shared.Filters;

namespace DesktopClientLogic.ViewModels.SingleServiceViewModel
{
    public class SingleServiceHomeViewModel : ViewModelBase
    {
        private readonly ServicesProxy _servicesProxy;
        private readonly ServiceTypeProxy _serviceTypeProxy;
        private readonly SportTypeProxy _sportTypeProxy;
        private string _customer;
        private ServiceType _selectedServiceType;
        private SportType _selectedSportType;

        public SingleServiceHomeViewModel()
        {
            _servicesProxy = new ServicesProxy();
            _serviceTypeProxy = new ServiceTypeProxy();
            _sportTypeProxy = new SportTypeProxy();
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            Initialize();
        }

        public ICommand OnPageLoadCommand { get; set; }
        public ObservableCollection<ServiceType> ServiceTypes { get; set; }
        public ObservableCollection<SportType> SportTypes { get; set; }
        public ObservableCollection<SingleService> Services { get; set; }

        public ServiceType SelectedServiceType
        {
            get { return _selectedServiceType; }
            set
            {
                _selectedServiceType = value;
                GetServices();
            }
        }

        public SportType SelectedSportType
        {
            get { return _selectedSportType; }
            set
            {
                _selectedSportType = value;
                GetServices();
                GetServiceTypes();
            }
        }

        public string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                GetServices();
            }
        }

        private async void Initialize()
        {
            GetServices();

            ServiceTypes = new ObservableCollection<ServiceType>();
            GetServiceTypes();

            SportTypes = new ObservableCollection<SportType> {new SportType {Id = 0, Name = "Wszystkie"}};
            foreach (var sport in await _sportTypeProxy.GetSportTypes())
                SportTypes.Add(sport);
            OnPropertyChanged("SportTypes");
        }

        private void OnPageLoad(object obj)
        {
            Initialize();
        }

        private async void GetServices()
        {
            var filter = new ServiceFilter();
            if (!string.IsNullOrEmpty(Customer))
                filter.Customer = _customer;
            if (SelectedServiceType != null && SelectedServiceType.Id > 0)
                filter.ServiceTypeId = _selectedServiceType.Id;
            if (SelectedSportType != null && SelectedSportType.Id > 0)
                filter.SportId = _selectedSportType.Id;
            Services = await _servicesProxy.GetSingleServices(filter);
            OnPropertyChanged("Services");
        }

        private async void GetServiceTypes()
        {
            ServiceTypes.Add(new ServiceType {Id = 0, Name = "Wszystkie"});
            ServiceTypes = await _serviceTypeProxy.GetServiceTypes(new ServiceTypeFilter
            {
                SportId = SelectedSportType?.Id > 0 ? SelectedSportType.Id : (int?) null,
                IsCourse = false
            });
            OnPropertyChanged("ServiceTypes");
        }
    }
}