using AutoMapper;
using Contoso.Repositories;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings;
using LogicBuilder.RulesDirector;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Flow
{
    public interface IFlowManager
    {
        Progress Progress { get; }
        DirectorBase Director { get; }
        FlowDataCache FlowDataCache { get; }
        IDialogFunctions DialogFunctions { get; }
        IActions Actions { get; }
        IFlowActivity FlowActivity { get; }
        IMapper Mapper { get; }
        ISchoolRepository SchoolRepository { get; }

        Task<FlowSettings> Start(string module);
        Task<FlowSettings> Next(RequestBase request);
        Task<FlowSettings> NavStart(NavBarRequest navBarRequest);
        void FlowComplete();
        void Wait();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
