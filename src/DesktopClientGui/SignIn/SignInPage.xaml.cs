using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace DesktopClientGui.SignIn
{
    /// <summary>
    ///     Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : IContent
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
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

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.AccessLevel = ((TextBox) sender).Text;
        }
    }
}