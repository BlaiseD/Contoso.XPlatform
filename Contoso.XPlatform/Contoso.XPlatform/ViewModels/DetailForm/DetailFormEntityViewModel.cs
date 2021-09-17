using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public class DetailFormEntityViewModel<TModel> : DetailFormEntityViewModelBase where TModel : Domain.ViewModelBase
    {
        public DetailFormEntityViewModel(ScreenSettings<DetailFormSettingsDescriptor> screenSettings, IContextProvider contextProvider) 
            : base(screenSettings, contextProvider)
        {
            this.httpService = contextProvider.HttpService;
            this.propertiesUpdater = contextProvider.ReadOnlyPropertiesUpdater;
            GetEntity();
        }

        private readonly IHttpService httpService;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 51755FE3-099A-44EB-A59B-3ED312EDD8D1");

            GetEntityResponse getEntityResponse = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.httpService.GetEntity
                (
                    new GetEntityRequest
                    {
                        Filter = this.FormSettings.RequestDetails.Filter,
                        SelectExpandDefinition = this.FormSettings.RequestDetails.SelectExpandDefinition,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType
                    }
                )
            );

            if (getEntityResponse.Success == false)
            {
                await App.Current.MainPage.DisplayAlert
                (
                    "Errors",
                    string.Join(Environment.NewLine, getEntityResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            this.propertiesUpdater.UpdateProperties
            (
                Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }
    }
}
