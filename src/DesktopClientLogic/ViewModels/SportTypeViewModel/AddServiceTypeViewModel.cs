using System.Collections.ObjectModel;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.SportTypeViewModel
{
    public class AddServiceTypeViewModel : ViewModelBase
    {
        private readonly ServiceTypeProxy _serviceTypeProxy;
        private readonly SportTypeProxy _sportTypeProxy;

        public AddServiceTypeViewModel()
        {
            _sportTypeProxy = new SportTypeProxy();
            _serviceTypeProxy = new ServiceTypeProxy();
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            AddCourseTypeCommand = new RelayCommand(AddCourseType, AddCourseTypeCanExcecute);
            Initialize();
        }

        public ICommand OnPageLoadCommand { get; set; }
        public string PriceString { get; set; }
        public string Name { get; set; }
        public ObservableCollection<SportType> Sports { get; set; }
        public SportType SelectedSport { get; set; }
        public string Description { get; set; }
        public ICommand AddCourseTypeCommand { get; set; }
        public string DurationString { get; set; }
        public bool IsCourse { get; set; }

        public decimal Price
        {
            get
            {
                decimal d;
                decimal.TryParse(PriceString, out d);
                return d;
            }
        }

        private int Duration
        {
            get
            {
                int d;
                int.TryParse(DurationString, out d);
                return d;
            }
        }

        private async void Initialize()
        {
            Sports = await _sportTypeProxy.GetSportTypes();
            OnPropertyChanged("Sports");
            PriceString = null;
            OnPropertyChanged("PriceString");
            Name = null;
            OnPropertyChanged("Name");
            SelectedSport = null;
            OnPropertyChanged("SelectedSport");
            Description = null;
            OnPropertyChanged("Description");
            DurationString = null;
            OnPropertyChanged("DurationString");
        }

        private async void AddCourseType(object obj)
        {
            var s = new ServiceType
            {
                Description = Description,
                DurationInMinutes = Duration,
                Name = Name,
                Price = Price,
                SportTypeId = SelectedSport.Id,
                IsCourse = IsCourse
            };
            await _serviceTypeProxy.AddServiceType(s);
            NavigationCommands.GoToPage.Execute("/Home.xaml", null);
        }

        private bool AddCourseTypeCanExcecute(object obj)
        {
            return !string.IsNullOrEmpty(Name) && SelectedSport != null && Duration != 0;
        }

        private void OnPageLoad(object obj)
        {
            Initialize();
        }
    }
}