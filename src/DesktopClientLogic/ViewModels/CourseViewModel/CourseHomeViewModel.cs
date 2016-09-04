using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;
using Shared.Filters;

namespace DesktopClientLogic.ViewModels.CourseViewModel
{
    public class CourseHomeViewModel : ViewModelBase
    {
        private readonly ServicesProxy _servicesProxy;
        private readonly ServiceTypeProxy _serviceTypeProxy;
        private readonly SportTypeProxy _sportTypeProxy;
        private string _instructor;
        private ServiceType _selectedServiceType;
        private SportType _selectedSportType;

        public CourseHomeViewModel()
        {
            _servicesProxy = new ServicesProxy();
            _serviceTypeProxy = new ServiceTypeProxy();
            _sportTypeProxy = new SportTypeProxy();
            EditCourseCommand = new RelayCommand(EditCourse);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
        }

        public ICommand EditCourseCommand { get; set; }
        public ICommand OnPageLoadCommand { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public Course SelectedCourse { get; set; }
        public SolidColorBrush AccentColor => new SolidColorBrush(AppearanceManager.Current.AccentColor);
        public ObservableCollection<ServiceType> ServiceTypes { get; set; }
        public ObservableCollection<SportType> SportTypes { get; set; }

        public string Instructor
        {
            get { return _instructor; }
            set
            {
                _instructor = value;
                GetCourses();
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

        public SportType SelectedSportType
        {
            get { return _selectedSportType; }
            set
            {
                _selectedSportType = value;
                GetCourses();
                GetServiceTypes();
            }
        }

        private async void Initialize()
        {
            GetCourses();
            GetServiceTypes();

            OnPropertyChanged("ServiceTypes");

            SportTypes = new ObservableCollection<SportType> {new SportType {Id = 0, Name = "Wszystkie"}};
            foreach (var sport in await _sportTypeProxy.GetSportTypes())
                SportTypes.Add(sport);
            OnPropertyChanged("SportTypes");
        }

        private void OnPageLoad(object obj)
        {
            OnPropertyChanged("AccentColor");
            Initialize();
        }

        private void EditCourse(object obj)
        {
            NavigationCommands.GoToPage.Execute("/Course/AddCoursePage.xaml#" + SelectedCourse.Id, null);
        }

        private async void GetServiceTypes()
        {
            ServiceTypes = await _serviceTypeProxy.GetServiceTypes(new ServiceTypeFilter
            {
                SportId = SelectedSportType != null && SelectedSportType.Id > 0 ? SelectedSportType.Id : (int?) null,
                IsCourse = true
            });
            OnPropertyChanged("ServiceTypes");
        }

        private async void GetCourses()
        {
            var filter = new ServiceFilter();
            if (!string.IsNullOrEmpty(Instructor))
                filter.Instructor = Instructor;
            if (SelectedServiceType != null && SelectedServiceType.Id > 0)
                filter.ServiceTypeId = _selectedServiceType.Id;
            if (SelectedSportType != null && SelectedSportType.Id > 0)
                filter.SportId = _selectedSportType.Id;
            Courses = await _servicesProxy.GetCourses(filter);
            OnPropertyChanged("Courses");
        }
    }
}