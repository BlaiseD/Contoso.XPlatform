﻿using AutoMapper;
using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : FlyoutPage
    {
        public MainPageView()
        {
            InitializeComponent();
            flyout.ListView.SelectionChanged += ListView_SelectionChanged;
            FlowSettingsChanged();
        }

        private void FlowSettingsChanged()
        {
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 1)
                return;

            if (!(e.CurrentSelection.First() is MainPageViewMasterMenuItem item))
                return;

            DisposeCurrentPageBindingContext(Detail);

            Page page;
            if (item.TargetType == typeof(EditFormViewCS))
            {
                page = new EditFormViewCS(CreateEditFormViewModel());
            }
            else if(item.TargetType == typeof(EditFormView))
            {
                page = new EditFormView(CreateEditFormViewModel());
            }
            else
            {
                page = (Page)Activator.CreateInstance(item.TargetType);
            }
            
            page.Title = item.Title;

            Detail = GetNavigationPage(page);
            IsPresented = false;

            flyout.ListView.SelectedItem = null;

            object CreateEditFormViewModel()
                => Activator.CreateInstance
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
                        Descriptors.StudentForm, 
                        App.ServiceProvider.GetRequiredService<UiNotificationService>() ,
                        App.ServiceProvider.GetRequiredService<IMapper>()
                    }
                );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase) 
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName arg) 
                => typeof(Domain.BaseModelClass).Assembly;

            void DisposeCurrentPageBindingContext(Page detail)
            {
                if (detail is not NavigationPage navigationPage)
                    return;

                if (navigationPage?.RootPage.BindingContext is not IDisposable disposable)
                    return;

                disposable.Dispose();
            }
        }

        private NavigationPage GetNavigationPage(Page page)
        {
            NavigationPage.SetHasBackButton(page, false);
            page.SetDynamicResource(Page.BackgroundColorProperty, "PageBackgroundColor");
            var navigationPage = new NavigationPage(page);
            navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, "PageBackgroundColor");
            navigationPage.SetDynamicResource(NavigationPage.BarTextColorProperty, "PrimaryTextColor");

            return navigationPage;
        }
    }
}