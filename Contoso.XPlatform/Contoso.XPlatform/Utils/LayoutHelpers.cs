using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
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
            return CreatePage(Enum.GetName(typeof(ViewType), screenSettings.ViewType));

            Page CreatePage(string viewName) 
                => (Page)Activator.CreateInstance
                (
                    typeof(MainPageView).Assembly.GetType
                    (
                        $"Contoso.XPlatform.Views.{viewName}ViewCS"
                    ),
                    (ViewModelBase)Activator.CreateInstance
                    (
                        typeof(ViewModelBase).Assembly.GetType
                        (
                            $"Contoso.XPlatform.ViewModels.{viewName}ViewModel"
                        ),
                        screenSettings
                    )
                );
        }
    }
}
