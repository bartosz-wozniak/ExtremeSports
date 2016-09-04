using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.SportTypeViewModel
{
    /// <summary>
    ///     View Model Class for Sport Type Home Page
    /// </summary>
    /// <remarks>
    ///     Inherites from View Model Base
    /// </remarks>
    public class SportTypeHomeViewModel : ViewModelBase
    {
        /// <summary>
        ///     Proxy
        /// </summary>
        private readonly SportTypeProxy _sportTypeProxy;

        /// <summary>
        ///     Private field Cancellation Token Source.
        ///     Used to cancel task displaying error message
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        ///     Private Field for error Text
        /// </summary>
        private string _errorText;

        /// <summary>
        ///     Private field for Sport Types
        /// </summary>
        private ObservableCollection<SportType> _sportTypes;

        /// <summary>
        ///     Constructor initializes commands
        /// </summary>
        public SportTypeHomeViewModel()
        {
            _sportTypeProxy = new SportTypeProxy();
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            RemoveSportTypeCommand = new RelayCommand(RemoveSportType);
            EditSportTypeCommand = new RelayCommand(EditSportType);
            _cts = new CancellationTokenSource();
        }

        /// <summary>
        ///     Command bound to Page Loaded event
        /// </summary>
        public ICommand OnPageLoadCommand { get; set; }

        /// <summary>
        ///     Command bound to Remove Button
        /// </summary>
        public ICommand RemoveSportTypeCommand { get; set; }

        /// <summary>
        ///     Command bound to Edit button
        /// </summary>
        public ICommand EditSportTypeCommand { get; set; }

        /// <summary>
        ///     Currentlu selected row - Sport Type
        /// </summary>
        public SportType SelectedSportType { get; set; }

        /// <summary>
        ///     Collection of all Sport Types
        /// </summary>
        public ObservableCollection<SportType> SportTypes
        {
            get { return _sportTypes; }
            set
            {
                _sportTypes = value;
                OnPropertyChanged("SportTypes");
            }
        }

        /// <summary>
        ///     Text displayed when record cannot be deleted
        /// </summary>
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        /// <summary>
        ///     Method redirects to edit page and fills form
        /// </summary>
        /// <param name="obj">Leave it empty</param>
        private void EditSportType(object obj)
        {
            NavigationCommands.GoToPage.Execute("/SportType/SportTypeFormPage.xaml#" + SelectedSportType.Id, null);
        }

        /// <summary>
        ///     Gets updated List of Sports
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void OnPageLoad(object o)
        {
            SportTypes = await _sportTypeProxy.GetSportTypes();
            if (SportTypes != null) return;
            ErrorText = "Nie znaleziono sportów.";
        }

        /// <summary>
        ///     Removes Selected Sport if it is possible
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void RemoveSportType(object o)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var success = await _sportTypeProxy.RemoveSportType(SelectedSportType.Id);
            OnPageLoad(null);
            ErrorText = null;
            if (success) return;
            ErrorText =
                "Usuwanie sportu nie powiodło się. Sprawdź, czy żaden pracownik lub kurs nie jest do niego przypisany.";
            await RemoveErrorMesage(_cts.Token);
        }

        /// <summary>
        ///     Cancel displaying error message after specific time
        /// </summary>
        /// <param name="cts">Cancelation Token</param>
        /// <returns>False if cancelled by CT and true otherwise</returns>
        private async Task<bool> RemoveErrorMesage(CancellationToken cts)
        {
            try
            {
                await Task.Delay(5000, cts);
                ErrorText = null;
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
    }
}