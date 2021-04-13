using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Contoso.XPlatform.Validators.Rules
{
    public class IsValidPasswordRule : ValidationRuleBase<string>
    {
        public IsValidPasswordRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        private static readonly Regex RegexPassword = new Regex("(?=.*[A-Z])(?=.*\\d)(?=.*[¡!@#$%*¿?\\-_.\\(\\)])[A-Za-z\\d¡!@#$%*¿?\\-\\(\\)&]{8,20}");
        public override string ClassName { get => nameof(IsValidPasswordRule); }

        public override bool Check() => RegexPassword.IsMatch(Value ?? string.Empty);
    }
}
