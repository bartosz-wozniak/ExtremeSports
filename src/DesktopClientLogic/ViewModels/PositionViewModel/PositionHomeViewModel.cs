using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.PositionViewModel
{
    /// <summary>
    ///     View Model Class for Position Home Page
    /// </summary>
    /// <remarks>
    ///     Inherites from View Model Base
    /// </remarks>
    public class PositionHomeViewModel : ViewModelBase
    {
        private readonly PositionProxy _positionProxy;

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
        ///     Private field for Positions
        /// </summary>
        private ObservableCollection<Position> _positions;

        /// <summary>
        ///     Constructor initializes commands
        /// </summary>
        public PositionHomeViewModel()
        {
            _positionProxy = new PositionProxy();
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            RemovePositionCommand = new RelayCommand(RemovePosition);
            EditPositionCommand = new RelayCommand(EditPosition);
            _cts = new CancellationTokenSource();
        }

        /// <summary>
        ///     Command bound to Page Loaded event
        /// </summary>
        public ICommand OnPageLoadCommand { get; set; }

        /// <summary>
        ///     Command bound to Remove Button
        /// </summary>
        public ICommand RemovePositionCommand { get; set; }

        /// <summary>
        ///     Command bound to Edit button
        /// </summary>
        public ICommand EditPositionCommand { get; set; }

        /// <summary>
        ///     Currentlu selected row - Position
        /// </summary>
        public Position SelectedPosition { get; set; }

        /// <summary>
        ///     Collection of all Positions
        /// </summary>
        public ObservableCollection<Position> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                OnPropertyChanged("Positions");
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
        private void EditPosition(object obj)
        {
            NavigationCommands.GoToPage.Execute("/Position/PositionFormPage.xaml#" + SelectedPosition.Id, null);
        }

        /// <summary>
        ///     Gets updated List of Positions
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void OnPageLoad(object o)
        {
            Positions = await _positionProxy.GetPositions();
            if (Positions != null) return;
            ErrorText = "Nie znaleziono stanowisk.";
        }

        /// <summary>
        ///     Removes Selected Position if it is possible
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void RemovePosition(object o)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var success = await _positionProxy.RemovePosition(SelectedPosition.Id);
            OnPageLoad(null);
            ErrorText = null;
            if (success) return;
            ErrorText =
                "Usuwanie stanowiska nie powiodło się. Sprawdź, czy żaden pracownik nie jest do niego przypisany.";
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