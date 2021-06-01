using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.ViewModel
{
    using ServiceCenter.Models;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class StaffValidationViewModel : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>>
            _validationErrors = new Dictionary<string, ICollection<string>>();
        private readonly StaffMember _staff = new StaffMember();

        public StaffValidationViewModel(StaffMember staff)
        {
            _staff = staff;
        }

        public string Surname
        {
            get => _staff.Surname;
            set
            {
                _staff.Surname = value;
                ValidateModelProperty(value, "Surname");
            }
        }

        public string Name
        {
            get => _staff.Name;
            set
            {
                _staff.Name = value;
                ValidateModelProperty(value, "Name");
            }
        }

        public string Patronymic
        {
            get => _staff.Patronymic;
            set
            {
                _staff.Patronymic = value;
                ValidateModelProperty(value, "Patronymic");
            }
        }

        public string Position
        {
            get => _staff.Position;
            set
            {
                _staff.Position = value;
                ValidateModelProperty(value, "Position");
            }
        }

        public string Phone
        {
            get => _staff.Phone;
            set
            {
                _staff.Phone = value;
                ValidateModelProperty(value, "Phone");
            }
        }

        protected void ValidateModelProperty(object value, string propertyName)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            PropertyInfo propertyInfo = _staff.GetType().GetProperty(propertyName);
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
            ValidationContext validationContext = new ValidationContext(_staff, null, null);

            if (!Validator.TryValidateObject(_staff, validationContext, validationResults, true))
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
            RaiseErrorsChanged("Position");
            RaiseErrorsChanged("Phone");
        }

        public bool HasErrors => _validationErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void RaiseErrorsChanged(string propertyName)
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
