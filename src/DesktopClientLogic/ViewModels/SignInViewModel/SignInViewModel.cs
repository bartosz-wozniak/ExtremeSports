using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.SignInViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly AuthenticationProxy _authenticationProxy;
        private CancellationTokenSource _cts;
        private string _eMail;
        private string _errorText;
        private string _grantedAccess;
        private string _password;
        private string _titleText;

        public SignInViewModel()
        {
            _authenticationProxy = new AuthenticationProxy();
            TitleText = "Zaloguj się";
            SignInCommand = new RelayCommand(SignInAction, SignInCanExecute);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            _cts = new CancellationTokenSource();
        }

        public ICommand SignInCommand { get; set; }
        public ICommand OnPageLoadCommand { get; set; }

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        public string EMail
        {
            get { return _eMail; }
            set
            {
                _eMail = value;
                OnPropertyChanged("EMail");
            }
        }

        public string TitleText
        {
            get { return _titleText; }
            set
            {
                _titleText = value;
                OnPropertyChanged("TitleText");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string GrantedAccess
        {
            get { return _grantedAccess; }
            set
            {
                _grantedAccess = value;
                OnPropertyChanged("GrantedAccess");
            }
        }

        private SignIn SignInObject { get; set; }

        /// <summary>
        ///     On Page load method clears form
        /// </summary>
        /// <param name="obj">Leave it empty</param>
        private void OnPageLoad(object obj)
        {
            if (GrantedAccess != null)
            {
                TitleText = "Jesteś zalogowany!";
            }
            else
            {
                TitleText = "Zaloguj się!";
                Password = null;
                EMail = null;
                GrantedAccess = null;
            }
        }

        /// <summary>
        ///     Saves sport to a database
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void SignInAction(object o)
        {
            SignInObject = new SignIn
            {
                EMail = EMail,
                Password = Password
            };
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            ErrorText = "Trwa logowanie...";
            var signInResponse = await _authenticationProxy.Authenticate(SignInObject);
            var cookieData = signInResponse.Token + "|" + EMail;
            Application.SetCookie(new Uri(ConfigurationManager.AppSettings["CookiePath"]), cookieData);
            GrantedAccess = signInResponse.AuthorizationResult;
            //GrantedAccess = "Administrator"; 
            //GrantedAccess = "Employee";
            //GrantedAccess = "Unauthorized";
            if (GrantedAccess == "Administrator" || GrantedAccess == "Employee") return;
            ErrorText = "Logowanie nie powidoło się. Podaj poprawne dane.";
            Password = null;
            EMail = null;
            GrantedAccess = null;
            await RemoveErrorMesage(_cts.Token);
        }

        private async Task<bool> RemoveErrorMesage(CancellationToken cts)
        {
            try
            {
                await Task.Delay(5000, cts);
                ErrorText = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SignInCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(EMail) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}