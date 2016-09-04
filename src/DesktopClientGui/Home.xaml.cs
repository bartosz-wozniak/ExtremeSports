using DesktopClientLogic.ViewModels;

namespace DesktopClientGui
{
    /// <summary>
    ///     Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}