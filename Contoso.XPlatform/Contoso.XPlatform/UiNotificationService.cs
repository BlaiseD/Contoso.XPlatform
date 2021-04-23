using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace Contoso.XPlatform
{
    public class UiNotificationService
    {
        public Subject<bool> PropertyChanged { get; set; } = new Subject<bool>();

        public void NotifyPropertyChanged()
        {
            this.PropertyChanged.OnNext(true);
        }
    }
}
