using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopClientLogic.Proxy;
using DesktopClientLogic.ViewModelObjects;
using FirstFloor.ModernUI.Presentation;

namespace DesktopClientLogic.ViewModels.CustomerViewModel
{
    /// <summary>
    ///     View Model Class for Customer Form Page
    /// </summary>
    /// <remarks>
    ///     Implements IDataErrorInfo interface and iherites from ViewModelBase
    /// </remarks>
    public class CustomerFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly CustomerProxy _customerProxy;
        private string _apartmentNumber;
        private string _city;

        /// <summary>
        ///     Private fields used to initialize properties
        /// </summary>
        private CancellationTokenSource _cts;

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
        private string _postalCode;
        private string _street;
        private string _surname;
        private string _titleText;

        /// <summary>
        ///     Constructor initializes Commands
        /// </summary>
        public CustomerFormViewModel()
        {
            TitleText = "Stwórz Klienta";
            SaveCustomerCommand = new RelayCommand(SaveCustomer, SaveCanExcecute);
            OnPageLoadCommand = new RelayCommand(OnPageLoad);
            _cts = new CancellationTokenSource();
            _customerProxy = new CustomerProxy();
        }

        /// <summary>
        ///     Property bound to save button
        /// </summary>
        public ICommand SaveCustomerCommand { get; set; }

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
        ///     Customer Name
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
        ///     Customer Id
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
        ///     Customer Surname
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
        ///     Customer Street
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
        ///     Customer House Number
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
        ///     Customer Apartment Number
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
        ///     Customer City
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
        ///     Customer Postal Code
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
        ///     Customer Phone Number
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
        ///     Customer ICE Phone Number
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
        ///     Customer E-Mail
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
        ///     Customer Identity Card Number
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

        /// <summary>
        ///     Customer Personal Identity Number
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
        ///     Customer Object
        /// </summary>
        private Customer Customer { get; set; }

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
                    case "PersonalIdentityNumber":
                        return string.IsNullOrWhiteSpace(PersonalIdentityNumber) ? "Pole obowiązkowe" : null;
                    case "Email":
                        return string.IsNullOrWhiteSpace(Email) ? "Pole obowiązkowe" : null;
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
            if (Id != null)
            {
                TitleText = "Edytuj Klienta";
                Customer = await _customerProxy.GetCustomer((int) Id);
                Id = Customer.Id;
                Name = Customer.Name;
                Surname = Customer.Surname;
                Street = Customer.Street;
                HouseNumber = Customer.HouseNumber;
                ApartmentNumber = Customer.ApartmentNumber;
                City = Customer.City;
                PostalCode = Customer.PostalCode;
                PhoneNumber = Customer.PhoneNumber;
                IcePhoneNumber = Customer.IcePhoneNumber;
                Email = Customer.Email;
                IdentityCardNumber = Customer.IdentityCardNumber;
                PersonalIdentityNumber = Customer.PersonalIdentityNumber;
                Password = Customer.Password;
            }
            else
            {
                TitleText = "Stwórz Klienta";
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
                Password = null;
            }
        }

        /// <summary>
        ///     Saves Customer to a database
        /// </summary>
        /// <param name="o">Leave it empty</param>
        private async void SaveCustomer(object o)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            ErrorText = "Trwa zapisywanie...";
            var customer = new Customer
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
                Password = Password
            };
            var success = await _customerProxy.SaveCustomer(customer);
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
            if (string.IsNullOrWhiteSpace(PersonalIdentityNumber))
                return false;
            if (string.IsNullOrWhiteSpace(Name))
                return false;
            if (string.IsNullOrWhiteSpace(Email))
                return false;
            if (string.IsNullOrWhiteSpace(Password))
                return false;
            return !string.IsNullOrWhiteSpace(Surname);
        }
    }
}