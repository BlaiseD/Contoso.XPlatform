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
        private readonly IUtilities utilities;

        public EditFormViewModel(UiNotificationService uiNotificationService, IUtilities utilities)
        {
            this.uiNotificationService = uiNotificationService;
            this.utilities = utilities;
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
                    this.utilities
                }
            );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
