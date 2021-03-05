using Contoso.Bsl.Business.Responses;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Bsl.Flow
{
    public static class ResponseHelper<TResponse> where TResponse : BaseResponse
    {
        [AlsoKnownAs("CreateResponse")]
        public static TResponse CreateResponse() => Activator.CreateInstance<TResponse>();
    }
}
