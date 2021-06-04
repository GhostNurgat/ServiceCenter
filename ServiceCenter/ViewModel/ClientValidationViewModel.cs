using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.ViewModel
{
    using ServiceCenter.Models;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class ClientValidationViewModel : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>>
            _validationErrors = new Dictionary<string, ICollection<string>>();
        private readonly Client _client = new Client();

        public ClientValidationViewModel(Client client)
        {
            _client = client;
        }

        public string Surname
        {
            get => _client.Surname;
            set
            {
                _client.Surname = value;
                ValidateModelProperty(value, "Surname");
            }
        }

        public string Name
        {
            get => _client.Name;
            set
            {
                _client.Name = value;
                ValidateModelProperty(value, "Name");
            }
        }

        public string Patronymic
        {
            get => _client.Patronymic;
            set
            {
                _client.Patronymic = value;
                ValidateModelProperty(value, "Patronymic");
            }
        }

        public DateTime Birthday
        {
            get => _client.Birthday;
            set
            {
                _client.Birthday = value;
                ValidateModelProperty(value, "Birthday");
            }
        }

        public string Address
        {
            get => _client.Address;
            set
            {
                _client.Address = value;
                ValidateModelProperty(value, "Address");
            }
        }

        public string Technology
        {
            get => _client.Technology;
            set
            {
                _client.Technology = value;
                ValidateModelProperty(value, "Technology");
            }
        }

        public string BrandName
        {
            get => _client.BrandName;
            set
            {
                _client.BrandName = value;
                ValidateModelProperty(value, "BrandName");
            }
        }

        public string TechnologyName
        {
            get => _client.TechnologyName;
            set
            {
                _client.TechnologyName = value;
                ValidateModelProperty(value, "TechnologyName");
            }
        }

        public string Phone
        {
            get => _client.Phone;
            set
            {
                _client.Phone = value;
                ValidateModelProperty(value, "Phone");
            }
        }

        public string Email
        {
            get => _client.Email;
            set
            {
                _client.Email = value;
                ValidateModelProperty(value, "Email");
            }
        }

        protected void ValidateModelProperty(object value, string propertyName)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            PropertyInfo propertyInfo = _client.GetType().GetProperty(propertyName);
            IList<string> validationErrors =
                (from validationAttribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>()
                 where !validationAttribute.IsValid(value)
                 select validationAttribute.FormatErrorMessage(string.Empty))
                 .ToList();

            _validationErrors.Add(propertyName, validationErrors);
            RaiseErrorsChanged(propertyName);
        }

        protected void ValidateModel()
        {
            _validationErrors.Clear();
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(_client, null, null);

            if (!Validator.TryValidateObject(_client, validationContext, validationResults, true))
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    string property = validationResult.MemberNames.ElementAt(0);
                    if (_validationErrors.ContainsKey(property))
                        _validationErrors[property].Add(validationResult.ErrorMessage);
                    else
                        _validationErrors.Add(property, new List<string> { validationResult.ErrorMessage });
                }
            }

            RaiseErrorsChanged("Surname");
            RaiseErrorsChanged("Name");
            RaiseErrorsChanged("Patronymic");
            RaiseErrorsChanged("Birthday");
            RaiseErrorsChanged("Address");
            RaiseErrorsChanged("Technology");
            RaiseErrorsChanged("BrandName");
            RaiseErrorsChanged("TechnologyName");
            RaiseErrorsChanged("Phone");
            RaiseErrorsChanged("Email");
        }

        public bool HasErrors => _validationErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)
                || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }
    }
}
