using Contoso.XPlatform.Utils;
using LogicBuilder.Domain;
using System;

namespace Contoso.XPlatform.Domain
{
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().ToTypeString();
    }
}
