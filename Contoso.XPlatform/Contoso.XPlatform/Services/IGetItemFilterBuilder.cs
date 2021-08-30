using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.ItemFilter;
using System;

namespace Contoso.XPlatform.Services
{
    public interface IGetItemFilterBuilder
    {
        FilterLambdaOperatorDescriptor CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity);
    }
}
