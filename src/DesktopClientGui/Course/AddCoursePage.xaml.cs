using DesktopClientLogic.ViewModels.CourseViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace DesktopClientGui.Course
{
    /// <summary>
    ///     Interaction logic for AddCoursePage.xaml
    /// </summary>
    public partial class AddCoursePage : IContent
    {
        public AddCoursePage()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            DataContext = new AddCourseViewModel();
            var vm = (AddCourseViewModel)DataContext;
            if (!string.IsNullOrEmpty(e.Fragment))
            {
                var courseId = int.Parse(e.Fragment);
                if (courseId > 0)
                {
                    vm.CourseId = courseId;
                    vm.IsEditing = true;
                }
            }
            else
            {
                vm.CourseId = null;
                vm.IsEditing = false;
            }
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