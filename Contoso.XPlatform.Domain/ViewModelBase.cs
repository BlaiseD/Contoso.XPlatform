using Contoso.XPlatform.Domain.Json;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contoso.XPlatform.Domain
{
    [JsonConverter(typeof(ModelConverter))]
    public abstract class ViewModelBase : BaseModelClass, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;

            if (eventHandler != null)
            {
                var eventArgs = new PropertyChangedEventArgs(propertyName);

                this.PropertyChanged(this, eventArgs);
            }
        }
    }
}
