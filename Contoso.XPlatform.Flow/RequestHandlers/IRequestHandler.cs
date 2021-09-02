using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Flow.RequestHandlers
{
    public interface IRequestHandler
    {
        void Complete(IFlowManager flowManager);
    }
}
