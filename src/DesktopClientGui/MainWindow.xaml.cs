using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DesktopClientLogic.ViewModels.SignInViewModel;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace DesktopClientGui
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static string _accessLevel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
            var window = Application.Current.MainWindow as ModernWindow;
            AdminList = window?.MenuLinkGroups.ToList();
            EmployeeList = window?.MenuLinkGroups.ToList();
            SignInList = window?.MenuLinkGroups.ToList();
            var toRemove1 = window?.MenuLinkGroups.ElementAt(4);
            var toRemove2 = window?.MenuLinkGroups.ElementAt(5);
            var toRemove3 = window?.MenuLinkGroups.ElementAt(6);
            AdminList?.Remove(toRemove3);
            EmployeeList?.Remove(toRemove1);
            EmployeeList?.Remove(toRemove2);
            EmployeeList?.Remove(toRemove3);
            SignInList?.Clear();
            SignInList?.Add(toRemove3);
            window?.MenuLinkGroups.Clear();
            if (SignInList == null) return;
            foreach (var item in SignInList)
                window?.MenuLinkGroups.Add(item);
        }

        private static List<LinkGroup> AdminList { get; set; }
        private static List<LinkGroup> EmployeeList { get; set; }
        private static List<LinkGroup> SignInList { get; set; }

        public static string AccessLevel
        {
            get { return _accessLevel; }
            set
            {
                _accessLevel = value;
                var window = Application.Current.MainWindow as ModernWindow;
                switch (value)
                {
                    case "Administrator":
                        window?.MenuLinkGroups.Clear();
                        foreach (var item in AdminList)
                            window?.MenuLinkGroups.Add(item);
                        break;
                    case "Employee":
                        window?.MenuLinkGroups.Clear();
                        foreach (var item in EmployeeList)
                            window?.MenuLinkGroups.Add(item);
                        break;
                }
            }
        }

        public static void Logout()
        {
            var window = Application.Current.MainWindow as ModernWindow;
            window?.MenuLinkGroups.Clear();
            if (SignInList == null) return;
            foreach (var item in SignInList)
                window?.MenuLinkGroups.Add(item);
        }
    }
}