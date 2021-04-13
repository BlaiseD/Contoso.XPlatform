using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public class IsValueTrueRule : ValidationRuleBase<bool>
    {
        public IsValueTrueRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName { get => nameof(IsValueTrueRule); }
        public override bool Check() => Value;
    }
}
