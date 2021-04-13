using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public class RequiredRule<T> : ValidationRuleBase<T>
    {
        public RequiredRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties, T defaultValue)
            : base(fieldName, validationMessage, allProperties)
        {
            DefaultValue = defaultValue;
        }

        public override string ClassName { get => nameof(RequiredRule<object>); }
        public T DefaultValue { get; }

        public override bool Check() => !EqualityComparer<T>.Default.Equals(Value, DefaultValue);
    }
}
