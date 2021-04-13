using Xamarin.Forms;

namespace Contoso.XPlatform.Behaviours
{
    public class EntryLineValidationBehaviour : BehaviorBase<Entry>
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create
        (
            nameof(IsValid), 
            typeof(bool), 
            typeof(EntryLineValidationBehaviour), 
            true, 
            BindingMode.Default, 
            null, 
            (bindable, oldValue, newValue) => OnIsValidChanged(bindable, newValue)
        );

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        private static void OnIsValidChanged(BindableObject bindable, object newValue)
        {
            if (bindable is EntryLineValidationBehaviour IsValidBehavior &&
                 newValue is bool IsValid)
            {
                if (IsValid)
                    IsValidBehavior.AssociatedObject.PlaceholderColor = Color.Default;
                else
                    IsValidBehavior.AssociatedObject.SetDynamicResource(Entry.PlaceholderColorProperty, "ErrorTextColor");
            }
        }
    }
}
