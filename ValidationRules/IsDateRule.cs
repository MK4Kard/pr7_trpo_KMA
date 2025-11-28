using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pr7_trpo_1_KMA.ValidationRules
{
    internal class IsDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (!DateTime.TryParse(input, out DateTime dateValue))
            {
                return new ValidationResult(false, "Необходимо ввести дату");
            }

            return ValidationResult.ValidResult;
        }
    }
}
