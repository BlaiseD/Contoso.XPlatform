﻿using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration.ListForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.ViewModels.ListPage
{
    public class ListPageCollectionViewModel<TModel> : ListPageCollectionViewModelBase where TModel : Domain.ViewModelBase
    {
        public ListPageCollectionViewModel(ScreenSettings<ListFormSettingsDescriptor> screenSettings, IHttpService httpService) : base(screenSettings)
        {
            this.httpService = httpService;
            GetItems();
        }

        private readonly IHttpService httpService;

        private ObservableCollection<TModel> _items;
        public ObservableCollection<TModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private Task<GetListResponse> GetList()
            => BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => httpService.GetList
                (
                    new GetTypedListRequest
                    {
                        Selector = this.FormSettings.FieldsSelector,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType,
                        ModelReturnType = this.FormSettings.RequestDetails.ModelReturnType,
                        DataReturnType = this.FormSettings.RequestDetails.DataReturnType
                    }
                )
            );

        private async void GetItems()
        {
            GetListResponse getListResponse = await GetList();

            if (getListResponse.Success == false)
                return;

            this.Items = new ObservableCollection<TModel>(getListResponse.List.Cast<TModel>());
        }
    }
}