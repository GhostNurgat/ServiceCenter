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

    public class OrderValidationViewModel : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>>
            _validationErrors = new Dictionary<string, ICollection<string>>();
        private readonly Order _order = new Order();

        public OrderValidationViewModel(Order order)
        {
            _order = order;
        }

        public string Service
        {
            get => _order.Service;
            set
            {
                _order.Service = value;
                ValidateModelProperty(value, "Service");
            }
        }

        public int ClientId
        {
            get => _order.ClientId;
            set
            {
                _order.ClientId = value;
                ValidateModelProperty(value, "ClientId");
            }
        }

        public int TypeId
        {
            get => _order.TypeId;
            set
            {
                _order.TypeId = value;
                ValidateModelProperty(value, "TypeId");
            }
        }

        public string BrandName
        {
            get => _order.BrandName;
            set
            {
                _order.BrandName = value;
                ValidateModelProperty(value, "BrandName");
            }
        }

        public string TechnologyName
        {
            get => _order.TechnologyName;
            set
            {
                _order.TechnologyName = value;
                ValidateModelProperty(value, "TechnologyName");
            }
        }

        public decimal Price
        {
            get => _order.Price;
            set
            {
                _order.Price = value;
                ValidateModelProperty(value, "Price");
            }
        }

        public string Guarantee
        {
            get => _order.Guarantee;
            set
            {
                _order.Guarantee = value;
                ValidateModelProperty(value, "Guarantee");
            }
        }

        public int? StaffId
        {
            get => _order.StaffId;
            set
            {
                _order.StaffId = value;
                ValidateModelProperty(value, "StaffId");
            }
        }

        public DateTime DateOrder
        {
            get => _order.DateOrder;
            set
            {
                _order.DateOrder = value;
                ValidateModelProperty(value, "DateOrder");
            }
        }

        public bool Done
        {
            get => _order.Done;
            set
            {
                _order.Done = value;
                ValidateModelProperty(value, "Done");
            }
        }

        public decimal Payment
        {
            get => _order.Payment;
            set
            {
                _order.Payment = value;
                ValidateModelProperty(value, "Payment");
            }
        }

        protected void ValidateModelProperty(object value, string propertyName)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            PropertyInfo propertyInfo = _order.GetType().GetProperty(propertyName);
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
            ValidationContext validationContext = new ValidationContext(_order, null, null);

            if (!Validator.TryValidateObject(_order, validationContext, validationResults, true))
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

            RaiseErrorsChanged("Service");
            RaiseErrorsChanged("ClientId");
            RaiseErrorsChanged("Price");
            RaiseErrorsChanged("Guarantee");
            RaiseErrorsChanged("StaffId");
            RaiseErrorsChanged("DateOrder");
            RaiseErrorsChanged("Done");
            RaiseErrorsChanged("Payment");
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
