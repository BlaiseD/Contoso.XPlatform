﻿using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.SearchPage
{
    public class SearchPageCollectionViewModel<TModel> : SearchPageCollectionViewModelBase where TModel : Domain.EntityModelBase
    {
        public SearchPageCollectionViewModel(ScreenSettings<SearchFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
            : base(screenSettings)
        {
            this.uiNotificationService = contextProvider.UiNotificationService;
            this.getItemFilterBuilder = contextProvider.GetItemFilterBuilder;
            this.httpService = contextProvider.HttpService;
            this.searchSelectorBuilder = contextProvider.SearchSelectorBuilder;
            defaultSkip = FormSettings.SortCollection.Skip;
            GetItems();
        }

        private readonly UiNotificationService uiNotificationService;
        private readonly IGetItemFilterBuilder getItemFilterBuilder;
        private readonly IHttpService httpService;
        private readonly ISearchSelectorBuilder searchSelectorBuilder;
        private readonly int? defaultSkip;

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText; set
            {
                if (_searchText == value)
                    return;

                _searchText = value;
                OnPropertyChanged();
            }
        }

        private TModel _selectedItem;
        public TModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem == null || !_selectedItem.Equals(value))
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    CheckCanExecute();
                }
            }
        }

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

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand != null)
                    return _addCommand;

                _addCommand = new Command<CommandButtonDescriptor>
                (
                     Add
                );

                return _addCommand;
            }
        }

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
                    (button) => SelectedItem != null
                );

                return _editCommand;
            }
        }

        private ICommand _detailCommand;
        public ICommand DetailCommand
        {
            get
            {
                if (_detailCommand != null)
                    return _detailCommand;

                _detailCommand = new Command<CommandButtonDescriptor>
                (
                    Detail,
                    (button) => SelectedItem != null
                );

                return _detailCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command<CommandButtonDescriptor>
                (
                    Delete,
                    (button) => SelectedItem != null
                );

                return _deleteCommand;
            }
        }

        private ICommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand != null)
                    return _selectionChangedCommand;

                _selectionChangedCommand = new Command
                (
                    CheckCanExecute
                );

                return _selectionChangedCommand;
            }
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand != null)
                    return _refreshCommand;

                _refreshCommand = new Command
                (
                    PullMoreItems
                );

                return _refreshCommand;
            }
        }

        private ICommand _textChangedCommand;
        public ICommand TextChangedCommand
        {
            get
            {
                if (_textChangedCommand != null)
                    return _textChangedCommand;

                _textChangedCommand = new Command
                (
                    async (parameter) =>
                    {
                        const int debounceDelay = 1000;
                        string text = ((TextChangedEventArgs)parameter).NewTextValue;
                        if (text == null)
                            return;

                        await Task.Delay(debounceDelay).ContinueWith
                        (
                            (task, oldText) =>
                            {
                                if (text == (string)oldText)
                                    Filter();
                            },
                            text
                        );
                    }
                );

                return _textChangedCommand;
            }
        }

        private void Filter()
        {
            this.FormSettings.SortCollection.Skip = defaultSkip;
            GetItems();
        }

        private void CheckCanExecute()
        {
            (EditCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
            (DetailCommand as Command).ChangeCanExecute();
        }

        private Task<GetListResponse> GetList()
            => BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.httpService.GetList
                (
                    new GetTypedListRequest
                    {
                        Selector = this.searchSelectorBuilder.CreatePagingSelector
                        (
                            this.FormSettings.SortCollection,
                            typeof(TModel),
                            this.FormSettings.SearchFilterGroup,
                            SearchText
                        ),
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

        private async void PullMoreItems()
        {
            this.FormSettings.SortCollection.Skip = (defaultSkip ?? 0) + this.Items.Count;

            IsRefreshing = true;
            GetListResponse getListResponse = await GetList();
            IsRefreshing = false;

            if (getListResponse.Success == false)
                return;

            if (this.Items == null)
                this.Items = new ObservableCollection<TModel>();

            foreach (TModel model in getListResponse.List)
                this.Items.Add(model);

        }

        private void Add(CommandButtonDescriptor button)
        {
            NavigateNext(button);
        }

        private void Edit(CommandButtonDescriptor button)
        {
            SetItemFilter();
            NavigateNext(button);
        }

        private void Delete(CommandButtonDescriptor button)
        {
            SetItemFilter();
            NavigateNext(button);
        }

        private void Detail(CommandButtonDescriptor button)
        {
            SetItemFilter();
            NavigateNext(button);
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
                    SelectedItem
                )
            );
        }

        private Task NavigateNext(CommandButtonDescriptor button)
            => this.uiNotificationService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
    }
}
