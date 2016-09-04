using System.Windows.Input;

namespace DesktopClientGui.SportType
{
    /// <summary>
    ///     Interaction logic for AddServiceTypePage.xaml
    /// </summary>
    public partial class AddServiceTypePage
    {
        public AddServiceTypePage()
        {
            InitializeComponent();
        }

        private void Duration_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
                e.Handled = true;
        }

        private void Price_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (
                !((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                  e.Key == Key.OemComma))
                e.Handled = true;
        }
    }
}