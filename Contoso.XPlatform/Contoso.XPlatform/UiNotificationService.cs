using System.Reactive.Subjects;

namespace Contoso.XPlatform
{
    public class UiNotificationService
    {
        public Subject<string> ValueChanged { get; set; } = new Subject<string>();

        public void NotifyPropertyChanged(string fieldName)
        {
            this.ValueChanged.OnNext(fieldName);
        }
    }
}
