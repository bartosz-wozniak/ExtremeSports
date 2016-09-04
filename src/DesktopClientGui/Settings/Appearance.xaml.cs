using DesktopClientLogic.ViewModels;

namespace DesktopClientGui.Settings
{
    /// <summary>
    ///     Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance
    {
        public Appearance()
        {
            InitializeComponent();
            // create and assign the appearance view model
            DataContext = new AppearanceViewModel();
        }
    }
}