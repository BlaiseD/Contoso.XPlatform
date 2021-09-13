﻿using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public interface IEntityStateUpdater
    {
        TModel GetUpdatedModel<TModel>(TModel existingEntity, ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings);
    }
}