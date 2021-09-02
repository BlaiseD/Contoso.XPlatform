using Contoso.Domain.Entities;

namespace Contoso.XPlatform.Rules
{
    public interface IRulesLoader
    {
        void LoadRules(RulesModuleModel modules, RulesCache cache);
    }
}
