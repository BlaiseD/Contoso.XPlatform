using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class AddFormValidatableObject<T> : FormValidatableObject<T> where T : class
    {
        public AddFormValidatableObject(string name, IChildFormGroupSettings setting, IEnumerable<IValidationRule> validations, IContextProvider contextProvider) : base(name, setting, validations, contextProvider)
        {
        }

        public event EventHandler AddCancelled;

        protected override void Cancel()
        {
            AddCancelled?.Invoke(this, new EventArgs());
            base.Cancel();
        }
    }
}
