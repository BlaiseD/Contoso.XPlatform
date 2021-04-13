﻿using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public class IsValidEmailRule : ValidationRuleBase<string>
    {
        public IsValidEmailRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName { get => nameof(IsValidEmailRule); }

        public override bool Check()
        {
            try
            {
                return new System.Net.Mail.MailAddress(Value).Address == Value;
            }
            catch
            {
                return false;
            }
        }
    }
}