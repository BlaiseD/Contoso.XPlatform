using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels
{
    public class EditFormViewModel : ViewModelBase
    {
        public EditFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();


        private IDictionary<string, object> values;
        private readonly FieldsCollectionHelper fieldsCollectionHelper;
        private CommandButtonDescriptor _selectedButton;

        public EditFormViewModel(EditFormSettingsDescriptor formSettings)
        {
            FormSettings = formSettings;
            fieldsCollectionHelper = new FieldsCollectionHelper(FormSettings, Properties);
            fieldsCollectionHelper.CreateFieldsCollection();
        }

        public CommandButtonDescriptor SelectedButton
        {
            get { return _selectedButton; }
            set
            {
                if (_selectedButton == value)
                    return;

                _selectedButton = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignUpCommand => new Command(async () =>
        {
            if (!AreFieldsValid())
                return;

            await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
        });

        bool AreFieldsValid()
            => Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );
    }
}
