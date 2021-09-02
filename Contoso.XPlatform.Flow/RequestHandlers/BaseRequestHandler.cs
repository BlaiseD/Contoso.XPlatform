using Contoso.XPlatform.Flow.Requests;

namespace Contoso.XPlatform.Flow.RequestHandlers
{
    public class BaseRequestHandler : IRequestHandler
    {
        public BaseRequestHandler(RequestBase request)
        {
            Request = request;
        }

        protected RequestBase Request { get; }

        public void Complete(IFlowManager flowManager) 
            => flowManager.Director.SetSelection(this.Request.CommandButtonRequest.NewSelection);

        public static BaseRequestHandler Create(RequestBase request) 
            => new DefaultRequestHandler(request);
    }
}
