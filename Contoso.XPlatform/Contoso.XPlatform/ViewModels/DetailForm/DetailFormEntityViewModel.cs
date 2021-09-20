using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DetailForm;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public class DetailFormEntityViewModel<TModel> : DetailFormEntityViewModelBase where TModel : Domain.ViewModelBase
    {
        public DetailFormEntityViewModel(ScreenSettings<DetailFormSettingsDescriptor> screenSettings, IContextProvider contextProvider) 
            : base(screenSettings, contextProvider)
        {
            this.httpService = contextProvider.HttpService;
            this.propertiesUpdater = contextProvider.ReadOnlyPropertiesUpdater;
            this.uiNotificationService = contextProvider.UiNotificationService;
            this.getItemFilterBuilder = contextProvider.GetItemFilterBuilder;
            GetEntity();
        }

        private readonly IHttpService httpService;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly IGetItemFilterBuilder getItemFilterBuilder;
        private readonly UiNotificationService uiNotificationService;
        private TModel entity;

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                    return _editCommand;

                _editCommand = new Command<CommandButtonDescriptor>
                (
                    Edit,
                    (button) => this.entity != null
                );

                return _editCommand;
            }
        }

        private void Edit(CommandButtonDescriptor button)
        {
            SetItemFilter();
            NavigateNext(button);
        }

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

            this.entity = (TModel)getEntityResponse.Entity;
            (EditCommand as Command).ChangeCanExecute();

            this.propertiesUpdater.UpdateProperties
            (
                Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private void SetItemFilter()
        {
            this.uiNotificationService.SetFlowDataCacheItem
            (
                typeof(FilterLambdaOperatorParameters).FullName,
                this.getItemFilterBuilder.CreateFilter
                (
                    this.FormSettings.ItemFilterGroup,
                    typeof(TModel),
                    this.entity
                )
            );
        }
    }
}
