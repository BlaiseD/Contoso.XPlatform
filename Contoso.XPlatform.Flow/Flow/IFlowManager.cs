using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Flow
{
    public interface IFlowManager
    {
        DirectorBase Director { get; }
        IFlowActivity FlowActivity { get; }
        Variables Variables { get; }
        Progress Progress { get; }
        IDialogFunctions DialogFunctions { get; }
        

        void FlowComplete();
        void Wait();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
