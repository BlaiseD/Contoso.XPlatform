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
        public AddFormValidatableObject(string name, IChildFormGroupSettings setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService, IMapper mapper, IFieldsCollectionBuilder fieldsCollectionBuilder, IEntityUpdater entityUpdater) : base(name, setting, validations, uiNotificationService, mapper, fieldsCollectionBuilder, entityUpdater)
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
