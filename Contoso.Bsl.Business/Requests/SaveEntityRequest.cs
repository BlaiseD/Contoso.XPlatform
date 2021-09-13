using Contoso.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Bsl.Business.Requests
{
    public class SaveEntityRequest<TModel> : BaseRequest where TModel : BaseModelClass
    {
        public TModel Entity { get; set; }
    }
}
