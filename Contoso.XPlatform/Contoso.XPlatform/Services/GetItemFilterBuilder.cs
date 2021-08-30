using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.ItemFilter;
using Contoso.XPlatform.Utils;
using System;

namespace Contoso.XPlatform.Services
{
    public class GetItemFilterBuilder : IGetItemFilterBuilder
    {
        public FilterLambdaOperatorDescriptor CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity) 
            => CreateItemFilterHelper.CreateFilter(descriptor, modelType, entity);
    }
}
