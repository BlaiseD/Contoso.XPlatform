using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormValidatableObject<T> : ValidatableObjectBase<T> where T : class
    {
        public FormValidatableObject(string name, FormGroupSettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService, IHttpService httpService) : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            FieldsCollectionHelper fieldsCollectionHelper = new FieldsCollectionHelper(setting, Properties, uiNotificationService, httpService);
            fieldsCollectionHelper.CreateFieldsCollection();
        }

        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();
    }
}
