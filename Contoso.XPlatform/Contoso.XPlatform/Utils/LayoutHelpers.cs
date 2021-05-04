using AutoMapper;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    internal static class LayoutHelpers
    {
        public static T AddBinding<T>(this T bindable, BindableProperty targetProperty, BindingBase binding) where T : BindableObject
        {
            bindable.SetBinding
            (
                targetProperty,
                binding
            );

            return bindable;
        }

        public static Style GetStaticStyleResource(string styleName)
        {
            if (Application.Current.Resources.TryGetValue(styleName, out object resource)
                && resource is Style style)
                return style;

            throw new ArgumentException($"{nameof(styleName)}: DF65BD5C-E8A5-409C-A736-F6DF1B29D5E7");
        }

        internal static Page CreatePage(this ScreenSettingsBase screenSettings)
        {
            if (screenSettings.ViewType == ViewType.EditForm)
            {
                return screenSettings.CreateEditForm();
            }

            return null;
        }

        private static Page CreateEditForm(this ScreenSettingsBase screenSettings)
        {
            return new EditFormViewCS(CreateEditFormViewModel());

            EditFormViewModelBase CreateEditFormViewModel()
                => (EditFormViewModelBase)Activator.CreateInstance
                (
                    typeof(EditFormViewModel<>).MakeGenericType
                    (
                        Type.GetType
                        (
                            Descriptors.StudentForm.ModelType,
                            AssemblyResolver,
                            TypeResolver
                        )
                    ),
                    new object[]
                    {
                        screenSettings,
                        App.ServiceProvider.GetRequiredService<UiNotificationService>(),
                        App.ServiceProvider.GetRequiredService<IMapper>(),
                        App.ServiceProvider.GetRequiredService<IHttpService>()
                    }
                );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
