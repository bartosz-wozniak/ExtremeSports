using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;
using Shared.Filters;

namespace DesktopClientLogic.ViewModels.CourseViewModel
{
    public class AddCourseViewModel : ViewModelBase
    {
        private readonly EmployeeProxy _employeeProxy;
        private readonly ServicesProxy _servicesProxy;
        private readonly ServiceTypeProxy _serviceTypeProxy;

        public AddCourseViewModel()
        {
            _employeeProxy = new EmployeeProxy();
            _serviceTypeProxy = new ServiceTypeProxy();
            _servicesProxy = new ServicesProxy();
            Course = new Course();
            SaveChangesCommand = new RelayCommand(SaveChanges);
            AddCourseCommand = new RelayCommand(AddCourse, AddCourseCanExcecute);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            AddDateCommand = new RelayCommand(AddDate, AddDateCanExcecute);
            RemoveCustomerCommand = new RelayCommand(RemoveCustomer);
            RemoveDateCommand = new RelayCommand(RemoveDate);
            RemoveCourseCommand = new RelayCommand(RemoveCourse);
        }

        /// <summary>
        ///     Is adding
        /// </summary>
        public bool IsAdding => !IsEditing;

        /// <summary>
        ///     Is editing
        /// </summary>
        public bool IsEditing { get; set; }

        /// <summary>
        ///     Course id
        /// </summary>
        public int? CourseId { get; set; }

        /// <summary>
        ///     Course
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        ///     Instructucctors
        /// </summary>
        public ObservableCollection<Employee> Instructors { get; set; }

        /// <summary>
        ///     Course types
        /// </summary>
        public ObservableCollection<ServiceType> CourseTypes { get; set; }

        public Employee SelectedInstructor { get; set; }
        public ServiceType SelectedCourse { get; set; }
        public DateTime? DateToAdd { get; set; }
        public ICommand AddCourseCommand { get; set; }
        public ICommand OnPageLoadCommand { get; set; }
        public ICommand AddDateCommand { get; set; }
        public ICommand RemoveCustomerCommand { get; set; }
        public ICommand RemoveDateCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveCourseCommand { get; set; }

        private async void OnPageLoad(object o)
        {
            Instructors = await _employeeProxy.GetEmployees(string.Empty);
            OnPropertyChanged("Instructors");
            CourseTypes = await _serviceTypeProxy.GetServiceTypes(new ServiceTypeFilter {IsCourse = true});
            OnPropertyChanged("CourseTypes");
            if (IsEditing && CourseId != null)
            {
                Course = await _servicesProxy.GetCourse(CourseId.Value);
                SelectedCourse = CourseTypes.First(c => c.Id == Course.ServiceTypeId);
                SelectedInstructor = Instructors.First(i => i.Id == Course.InstructorId);
                OnPropertyChanged("Course");
                OnPropertyChanged("SelectedCourse");
                OnPropertyChanged("SelectedInstructor");
            }
            OnPropertyChanged("IsEditing");
            OnPropertyChanged("IsAdding");
        }

        private async void AddCourse(object o)
        {
            Course.InstructorId = SelectedInstructor.Id;
            Course.ServiceTypeId = SelectedCourse.Id;
            await _servicesProxy.AddCourse(Course);
            NavigationCommands.GoToPage.Execute("/Home.xaml", null);
        }

        private void AddDate(object obj)
        {
            if (DateToAdd == null) return;
            Course.Dates.Add(DateToAdd.Value);
            OnPropertyChanged("Course");
        }

        private bool AddCourseCanExcecute(object o)
        {
            return SelectedInstructor != null && SelectedCourse != null && SelectedCourse.Id > 0;
        }

        private bool AddDateCanExcecute(object obj)
        {
            return DateToAdd != null;
        }

        private void RemoveCustomer(object obj)
        {
            var customerId = (int) obj;
            var customerToRemove = Course.Customers.First(c => c.Id == customerId);
            Course.Customers.Remove(customerToRemove);
            OnPropertyChanged("Course");
        }

        private void RemoveDate(object obj)
        {
            var date = (DateTime) obj;
            Course.Dates.Remove(date);
            OnPropertyChanged("Course");
        }

        private async void SaveChanges(object obj)
        {
            Course.InstructorId = SelectedInstructor.Id;
            Course.ServiceTypeId = SelectedCourse.Id;
            await _servicesProxy.EditCourse(Course);
            NavigationCommands.GoToPage.Execute("/Home.xaml", null);
        }

        private async void RemoveCourse(object obj)
        {
            Course.Customers.Clear();
            Course.Dates.Clear();
            SaveChanges(null);
            await _servicesProxy.RemoveCourse(Course.Id);
            NavigationCommands.GoToPage.Execute("/Home.xaml", null);
        }
    }
}