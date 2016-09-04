using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.EmployeeViewModel
{
    /// <summary>
    ///     View Model Class for Employee Form Page
    /// </summary>
    /// S
    /// <remarks>
    ///     Implements IDataErrorInfo interface and iherites from ViewModelBase
    /// </remarks>
    public class EmployeeFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly EmployeeProxy _employeeProxy;
        private readonly PositionProxy _positionProxy;
        private readonly SportTypeProxy _sportTypeProxy;
        private string _apartmentNumber;
        private string _city;

        /// <summary>
        ///     Private fields used to initialize properties
        /// </summary>
        private CancellationTokenSource _cts;

        private string _description;
        private string _email;
        private string _errorText;
        private string _houseNumber;
        private string _icePhoneNumber;
        private int? _id;
        private string _identityCardNumber;
        private string _name;
        private string _password;
        private string _personalIdentityNumber;
        private string _phoneNumber;
        private Position _position;
        private string _postalCode;
        private SportType _sportType;
        private string _street;
        private Employee _supervisor;
        private string _surname;
        private string _titleText;

        /// <summary>
        ///     Constructor initializes Commands
        /// </summary>
        public EmployeeFormViewModel()
        {
            TitleText = "Stwórz Pracownika";
            SaveEmployeeCommand = new RelayCommand(SaveEmployee, SaveCanExcecute);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            _cts = new CancellationTokenSource();
            _employeeProxy = new EmployeeProxy();
            _sportTypeProxy = new SportTypeProxy();
            _positionProxy = new PositionProxy();
        }

        /// <summary>
        ///     Property bound to save button
        /// </summary>
        public ICommand SaveEmployeeCommand { get; set; }

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
        ///     Employee Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///     Employee Id
        /// </summary>
        public int? Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///     Employee Surname
        /// </summary>
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }

        /// <summary>
        ///     Employee Street
        /// </summary>
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }

        /// <summary>
        ///     Employee House Number
        /// </summary>
        public string HouseNumber
        {
            get { return _houseNumber; }
            set
            {
                _houseNumber = value;
                OnPropertyChanged("HouseNumber");
            }
        }

        /// <summary>
        ///     Employee Apartment Number
        /// </summary>
        public string ApartmentNumber
        {
            get { return _apartmentNumber; }
            set
            {
                _apartmentNumber = value;
                OnPropertyChanged("ApartmentNumber");
            }
        }

        /// <summary>
        ///     Employee City
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        /// <summary>
        ///     Employee Postal Code
        /// </summary>
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        /// <summary>
        ///     Employee Phone Number
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        /// <summary>
        ///     Employee ICE Phone Number
        /// </summary>
        public string IcePhoneNumber
        {
            get { return _icePhoneNumber; }
            set
            {
                _icePhoneNumber = value;
                OnPropertyChanged("IcePhoneNumber");
            }
        }

        /// <summary>
        ///     Employee E-Mail
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        /// <summary>
        ///     Employee Identity Card Number
        /// </summary>
        public string IdentityCardNumber
        {
            get { return _identityCardNumber; }
            set
            {
                _identityCardNumber = value;
                OnPropertyChanged("IdentityCardNumber");
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

        /// <summary>
        ///     Employee Personal Identity Number
        /// </summary>
        public string PersonalIdentityNumber
        {
            get { return _personalIdentityNumber; }
            set
            {
                _personalIdentityNumber = value;
                OnPropertyChanged("PersonalIdentityNumber");
            }
        }

        /// <summary>
        ///     Employee Supervisor
        /// </summary>
        public Employee Supervisor
        {
            get { return _supervisor; }
            set
            {
                _supervisor = value;
                OnPropertyChanged("Supervisor");
            }
        }

        /// <summary>
        ///     Employee Postion
        /// </summary>
        public Position Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        /// <summary>
        ///     Employee Object
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        ///     Employee Sport Type
        /// </summary>
        public SportType SportType
        {
            get { return _sportType; }
            set
            {
                _sportType = value;
                OnPropertyChanged("SportType");
            }
        }

        /// <summary>
        ///     Employee Descriiption
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///     Supervisors Collection
        /// </summary>
        public ObservableCollection<Employee> Supervisors { get; set; }

        /// <summary>
        ///     Sport Types Collection
        /// </summary>
        public ObservableCollection<SportType> SportTypes { get; set; }

        /// <summary>
        ///     Positions Collection
        /// </summary>
        public ObservableCollection<Position> Positions { get; set; }

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
                switch (columnName)
                {
                    case "Name":
                        return string.IsNullOrWhiteSpace(Name) ? "Pole obowiązkowe" : null;
                    case "Surname":
                        return string.IsNullOrWhiteSpace(Surname) ? "Pole obowiązkowe" : null;
                    case "Street":
                        return string.IsNullOrWhiteSpace(Street) ? "Pole obowiązkowe" : null;
                    case "HouseNumber":
                        return string.IsNullOrWhiteSpace(HouseNumber) ? "Pole obowiązkowe" : null;
                    case "City":
                        return string.IsNullOrWhiteSpace(City) ? "Pole obowiązkowe" : null;
                    case "ApartmentNumber":
                        return string.IsNullOrWhiteSpace(ApartmentNumber) ? "Pole obowiązkowe" : null;
                    case "PostalCode":
                        return string.IsNullOrWhiteSpace(PostalCode) ? "Pole obowiązkowe" : null;
                    case "PhoneNumber":
                        return string.IsNullOrWhiteSpace(PhoneNumber) ? "Pole obowiązkowe" : null;
                    case "Email":
                        return string.IsNullOrWhiteSpace(Email) ? "Pole obowiązkowe" : null;
                    case "IdentityCardNumber":
                        return string.IsNullOrWhiteSpace(IdentityCardNumber) ? "Pole obowiązkowe" : null;
                    case "PersonalIdentityNumber":
                        return string.IsNullOrWhiteSpace(PersonalIdentityNumber) ||
                               PersonalIdentityNumber.Any(c => !char.IsDigit(c))
                            ? "Pole obowiązkowe, musi być liczbą"
                            : null;
                    case "Position":
                        return Position == null ? "Pole obowiązkowe" : null;
                    case "SportType":
                        return SportType == null ? "Pole obowiązkowe" : null;
                }
                return null;
            }
        }

        /// <summary>
        ///     On Page load method clears or fills form
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void OnPageLoad(object o)
        {
            await Initialize();
            if (Id != null)
            {
                TitleText = "Edytuj Pracownika";
                Employee = await _employeeProxy.GetEmployee((int) Id);
                Id = Employee.Id;
                Name = Employee.Name;
                Surname = Employee.Surname;
                Street = Employee.Street;
                HouseNumber = Employee.HouseNumber;
                ApartmentNumber = Employee.ApartmentNumber;
                City = Employee.City;
                PostalCode = Employee.PostalCode;
                PhoneNumber = Employee.PhoneNumber;
                IcePhoneNumber = Employee.IcePhoneNumber;
                Email = Employee.Email;
                IdentityCardNumber = Employee.IdentityCardNumber;
                PersonalIdentityNumber = Employee.PersonalIdentityNumber;
                SportType = SportTypes.FirstOrDefault(s => s.Id == Employee.SportTypes[0].Id);
                Position = Positions.FirstOrDefault(p => p.Id == Employee.Position.Id);
                Supervisor = Supervisors.FirstOrDefault(s => s.Id == Employee.SupervisorId);
                Description = Employee.Description;
                Password = Employee.Password;
            }
            else
            {
                TitleText = "Stwórz Pracownika";
                Id = null;
                Name = null;
                Surname = null;
                Street = null;
                HouseNumber = null;
                ApartmentNumber = null;
                City = null;
                PostalCode = null;
                PhoneNumber = null;
                IcePhoneNumber = null;
                Email = null;
                IdentityCardNumber = null;
                PersonalIdentityNumber = null;
                SportType = null;
                Position = null;
                Supervisor = null;
                Description = null;
                Password = null;
            }
        }

        private async Task Initialize()
        {
            Supervisors = await _employeeProxy.GetEmployees(null);
            OnPropertyChanged("Supervisors");
            SportTypes = await _sportTypeProxy.GetSportTypes();
            OnPropertyChanged("SportTypes");
            Positions = await _positionProxy.GetPositions();
            OnPropertyChanged("Positions");
        }

        /// <summary>
        ///     Saves Employee to a database
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void SaveEmployee(object o)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            ErrorText = "Trwa zapisywanie...";
            var employee = new Employee
            {
                Id = Id ?? -1,
                Name = Name,
                Surname = Surname,
                Street = Street,
                City = City,
                PostalCode = PostalCode,
                PhoneNumber = PhoneNumber,
                IcePhoneNumber = IcePhoneNumber,
                Email = Email,
                IdentityCardNumber = IdentityCardNumber,
                PersonalIdentityNumber = PersonalIdentityNumber,
                ApartmentNumber = ApartmentNumber,
                HouseNumber = HouseNumber,
                Description = Description ?? " ",
                Position = Position,
                SupervisorId = Supervisor?.Id,
                Password = Password,
                SportTypes =
                    new ObservableCollection<SportType>(from item in SportTypes
                        where item.Name == SportType.Name
                        select item)
            };
            var success = await _employeeProxy.SaveEmployee(employee);
            if (success)
            {
                ErrorText = "Zapisano.";
                Id = null;
                Name = null;
                Surname = null;
                Street = null;
                HouseNumber = null;
                ApartmentNumber = null;
                City = null;
                PostalCode = null;
                PhoneNumber = null;
                IcePhoneNumber = null;
                Email = null;
                IdentityCardNumber = null;
                PersonalIdentityNumber = null;
                Description = null;
                Position = null;
                Supervisor = null;
                SportType = null;
                Password = null;
                await RemoveErrorMesage(_cts.Token);
            }
            else
            {
                ErrorText = "Zapisywanie nie powidło się.";
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
        private bool SaveCanExcecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(PersonalIdentityNumber) || PersonalIdentityNumber.Any(c => !char.IsDigit(c)))
                return false;
            if (string.IsNullOrWhiteSpace(Name))
                return false;
            if (string.IsNullOrWhiteSpace(Surname))
                return false;
            if (string.IsNullOrWhiteSpace(Street))
                return false;
            if (string.IsNullOrWhiteSpace(HouseNumber))
                return false;
            if (string.IsNullOrWhiteSpace(City))
                return false;
            if (string.IsNullOrWhiteSpace(ApartmentNumber))
                return false;
            if (string.IsNullOrWhiteSpace(PostalCode))
                return false;
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                return false;
            if (string.IsNullOrWhiteSpace(Email))
                return false;
            if (string.IsNullOrWhiteSpace(IdentityCardNumber))
                return false;
            if (Position == null)
                return false;
            if (string.IsNullOrWhiteSpace(Password))
                return false;
            return SportType != null;
        }
    }
}