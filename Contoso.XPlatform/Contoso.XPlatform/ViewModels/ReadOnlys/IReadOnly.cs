using System.ComponentModel;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public interface IReadOnly : INotifyPropertyChanged
    {
        string Name { get; set; }
        string TemplateName { get; set; }
        object Value { get; set; }
    }
}
