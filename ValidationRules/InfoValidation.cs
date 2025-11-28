using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pr7_trpo_1_KMA.ValidationRules
{
    public class InfoValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (input == string.Empty)
            {
                return new ValidationResult(false, "Заполнение данного поля обязательно");
            }

            return ValidationResult.ValidResult;
        }
    }
}
