using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public interface IDetailGroupSettings : IDetailGroupBoxSettings
    {
        string ModelType { get; }
        string Title { get; }
    }
}
