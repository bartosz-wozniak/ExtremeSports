using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.PositionViewModel
{
    /// <summary>
    ///     View Model Class for Position Form Page
    /// </summary>
    /// <remarks>
    ///     Implements IDataErrorInfo interface and iherites from ViewModelBase
    /// </remarks>
    public class PositionFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly PositionProxy _positionProxy;

        /// <summary>
        ///     Private fields used to initialize properties
        /// </summary>
        private CancellationTokenSource _cts;

        private string _errorText;
        private string _positionDescription;
        private int? _positionId;
        private string _positionName;
        private string _titleText;

        /// <summary>
        ///     Constructor initializes Commands
        /// </summary>
        public PositionFormViewModel()
        {
            _positionProxy = new PositionProxy();
            TitleText = "Stwórz Stanowisko";
            SavePositionCommand = new RelayCommand(SavePosition, SaveCanExecute);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            _cts = new CancellationTokenSource();
        }

        /// <summary>
        ///     Property bound to save button
        /// </summary>
        public ICommand SavePositionCommand { get; set; }

        /// <summary>
        ///     Property bound to loaded event
        /// </summary>
        public ICommand OnPageLoadCommand { get; set; }

        /// <summary>
        ///     Error Text informs whether saving to a database was successful
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
        ///     Position Type Name
        /// </summary>
        public string PositionName
        {
            get { return _positionName; }
            set
            {
                _positionName = value;
                OnPropertyChanged("PositionName");
            }
        }

        /// <summary>
        ///     Title Text of a Page
        /// </summary>
        public string TitleText
        {
            get { return _titleText; }
            set
            {
                _titleText = value;
                OnPropertyChanged("TitleText");
            }
        }

        /// <summary>
        ///     Position Description
        /// </summary>
        public string PositionDescription
        {
            get { return _positionDescription; }
            set
            {
                _positionDescription = value;
                OnPropertyChanged("PositionDescription");
            }
        }

        /// <summary>
        ///     Position Id
        /// </summary>
        public int? PositionId
        {
            get { return _positionId; }
            set
            {
                _positionId = value;
                OnPropertyChanged("PositionId");
            }
        }

        /// <summary>
        ///     Position Object
        /// </summary>
        private Position Position { get; set; }

        /// <summary>
        ///     Error property implements IDataErrorInfo
        /// </summary>
        public string Error => null;

        /// <summary>
        ///     Error Validation
        /// </summary>
        /// <param name="columnName">Property Name</param>
        /// <returns>Error Message</returns>
        public string this[string columnName]
        {
            get
            {
                if (columnName == "PositionName")
                    return string.IsNullOrWhiteSpace(PositionName) ? "Pole obowiązkowe" : null;
                return null;
            }
        }

        /// <summary>
        ///     On Page load method clears form
        /// </summary>
        /// <param name="obj">Leave it empty</param>
        private async void OnPageLoad(object obj)
        {
            if (PositionId != null)
            {
                TitleText = "Edytuj Stanowisko";
                Position = await _positionProxy.GetPosition((int) PositionId);
                PositionDescription = Position.Description;
                PositionName = Position.Name;
                PositionId = Position.Id;
            }
            else
            {
                TitleText = "Stwórz Stanowisko";
                PositionDescription = null;
                PositionName = null;
                PositionId = null;
            }
        }

        /// <summary>
        ///     Saves position to a database
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void SavePosition(object o)
        {
            Position = new Position
            {
                Id = PositionId ?? -1,
                Name = PositionName,
                Description = PositionDescription
            };
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            ErrorText = "Trwa zapisywanie...";
            var success = await _positionProxy.SavePosition(Position);
            if (success)
            {
                ErrorText = "Dodano stanowisko.";
                PositionDescription = null;
                PositionName = null;
                PositionId = null;
                await RemoveErrorMesage(_cts.Token);
            }
            else
            {
                ErrorText = "Dodawanie nie powidoło się. Pamiętaj, że nazwa musi być unikalna.";
                await RemoveErrorMesage(_cts.Token);
            }
        }

        /// <summary>
        ///     Removes displaying error message after specific time
        /// </summary>
        /// <param name="cts">Cancelation Token to stop method</param>
        /// <returns>False if cancelled by CT and true otherwise</returns>
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

        /// <summary>
        ///     Method checks whether SaveButton should be disabled or not
        /// </summary>
        /// <param name="obj">Leave it empty</param>
        /// <returns>True if button can be enabled and false otherwise</returns>
        private bool SaveCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(PositionName);
        }
    }
}