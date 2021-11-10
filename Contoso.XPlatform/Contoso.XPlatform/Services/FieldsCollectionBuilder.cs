﻿using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using System;

namespace Contoso.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public FieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType)
            => new FieldsCollectionHelper(formSettings, this.contextProvider, modelType).CreateFields();
    }
}
