using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.SportTypeViewModel
{
    /// <summary>
    ///     View Model Class for Sport Type Form Page
    /// </summary>
    /// <remarks>
    ///     Implements IDataErrorInfo interface and iherites from ViewModelBase
    /// </remarks>
    public class SportTypeFormViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        ///     Proxy
        /// </summary>
        private readonly SportTypeProxy _sportTypeProxy;

        /// <summary>
        ///     Private fields used to initialize properties
        /// </summary>
        private CancellationTokenSource _cts;

        private string _errorText;
        private string _sportTypeDescription;
        private int? _sportTypeId;
        private string _sportTypeName;
        private string _titleText;

        /// <summary>
        ///     Constructor initializes Commands
        /// </summary>
        public SportTypeFormViewModel()
        {
            TitleText = "Stwórz Sport";
            SaveSportTypeCommand = new RelayCommand(SaveSportType, SaveCanExecute);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            _cts = new CancellationTokenSource();
            _sportTypeProxy = new SportTypeProxy();
        }

        /// <summary>
        ///     Property bound to save button
        /// </summary>
        public ICommand SaveSportTypeCommand { get; set; }

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
        ///     Sport Type Name
        /// </summary>
        public string SportTypeName
        {
            get { return _sportTypeName; }
            set
            {
                _sportTypeName = value;
                OnPropertyChanged("SportTypeName");
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
        ///     Sport Type Description
        /// </summary>
        public string SportTypeDescription
        {
            get { return _sportTypeDescription; }
            set
            {
                _sportTypeDescription = value;
                OnPropertyChanged("SportTypeDescription");
            }
        }

        /// <summary>
        ///     Sport Type Id
        /// </summary>
        public int? SportTypeId
        {
            get { return _sportTypeId; }
            set
            {
                _sportTypeId = value;
                OnPropertyChanged("SportTypeId");
            }
        }

        /// <summary>
        ///     SportType Object
        /// </summary>
        private SportType SportType { get; set; }

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
                if (columnName == "SportTypeName")
                    return string.IsNullOrWhiteSpace(_sportTypeName) ? "Pole obowiązkowe" : null;
                return null;
            }
        }

        /// <summary>
        ///     On Page load method clears form
        /// </summary>
        /// <param name="obj">Leave it empty</param>
        private async void OnPageLoad(object obj)
        {
            if (SportTypeId != null)
            {
                TitleText = "Edytuj Sport";
                SportType = await _sportTypeProxy.GetSportType((int) SportTypeId);
                SportTypeDescription = SportType.Description;
                SportTypeName = SportType.Name;
                SportTypeId = SportType.Id;
            }
            else
            {
                TitleText = "Stwórz Stanowisko";
                SportTypeDescription = null;
                SportTypeName = null;
                SportTypeId = null;
            }
        }

        /// <summary>
        ///     Saves sport to a database
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void SaveSportType(object o)
        {
            SportType = new SportType
            {
                Id = SportTypeId ?? -1,
                Name = SportTypeName,
                Description = SportTypeDescription
            };
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            ErrorText = "Trwa zapisywanie...";
            var success = await _sportTypeProxy.SaveSportType(SportType);
            if (success)
            {
                ErrorText = "Zapisano.";
                SportTypeDescription = null;
                SportTypeName = null;
                SportTypeId = null;
                await RemoveErrorMesage(_cts.Token);
            }
            else
            {
                ErrorText = "Zapisywanie nie powidoło się. Pamiętaj, że nazwa musi być unikalna.";
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
            return !string.IsNullOrWhiteSpace(SportTypeName);
        }
    }
}