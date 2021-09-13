using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.EditForm;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels
{
    public class EditFormViewModel : FlyoutDetailViewModelBase
    {
        private readonly UiNotificationService uiNotificationService;
        private readonly IHttpService httpService;
        private readonly IMapper mapper;
        private readonly IFieldsCollectionBuilder fieldsCollectionBuilder;
        private readonly IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder;
        private readonly IEntityStateUpdater entityStateUpdater;

        public EditFormViewModel(UiNotificationService uiNotificationService, IHttpService httpService, IMapper mapper, IFieldsCollectionBuilder fieldsCollectionBuilder, IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder, IEntityStateUpdater entityStateUpdater)
        {
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
            this.mapper = mapper;
            this.fieldsCollectionBuilder = fieldsCollectionBuilder;
            this.conditionalValidationConditionsBuilder = conditionalValidationConditionsBuilder;
            this.entityStateUpdater = entityStateUpdater;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            EditFormEntityViewModel = CreateEditFormViewModel((ScreenSettings<EditFormSettingsDescriptor>)screenSettings);
        }

        public EditFormEntityViewModelBase EditFormEntityViewModel { get; set; }

        private EditFormEntityViewModelBase CreateEditFormViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings)
        {
            return (EditFormEntityViewModelBase)Activator.CreateInstance
            (
                typeof(EditFormEntityViewModel<>).MakeGenericType
                (
                    Type.GetType
                    (
                        screenSettings.Settings.ModelType,
                        AssemblyResolver,
                        TypeResolver
                    )
                ),
                new object[]
                {
                    screenSettings,
                    this.uiNotificationService,
                    this.httpService,
                    this.mapper,
                    this.fieldsCollectionBuilder,
                    this.conditionalValidationConditionsBuilder,
                    this.entityStateUpdater
                }
            );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
