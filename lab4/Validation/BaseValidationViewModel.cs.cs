using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Classes
{
    public abstract class BaseValidationViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Error
        {
            get
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(this);
                Validator.TryValidateObject(this, context, results, true);

                if (results.Any())
                {
                    return string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage));
                }
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var validationContext = new ValidationContext(this, null, null)
                {
                    MemberName = columnName
                };

                var validationResults = new List<ValidationResult>();

                var property = GetType().GetProperty(columnName);
                if (property != null)
                {
                    Validator.TryValidateProperty(property.GetValue(this), validationContext, validationResults);
                }

                if (validationResults.Any())
                {
                    return validationResults.First().ErrorMessage;
                }
                return null;
            }
        }
    }
}