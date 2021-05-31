using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormArrayValidatableObject<T, E> : ValidatableObjectBase<T> where T : ObservableCollection<E>
    {
        public FormArrayValidatableObject(string name, FormGroupArraySettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
        }

        private ObservableCollection<E> _entitiess;
        public ObservableCollection<E> Entities
        {
            get
            {
                return _entitiess;
            }

            set
            {
                _entitiess = value;
                Value = (T)_entitiess;
                OnPropertyChanged();
            }
        }
    }
}
