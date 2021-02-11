using Contoso.Domain.Entities;
using LogicBuilder.Workflow.Activities.Rules;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Contoso.Bsl.Flow.Rules
{
    public static class RulesService
    {
        public static async Task<RulesCache> LoadRules()
        {
            return await LoadRules(new RulesLoader());
        }

        static async Task<RulesCache> LoadRules(IRulesLoader rulesLoader)
        {
            RulesCache cache = new RulesCache(new Dictionary<string, RuleEngine>(), new Dictionary<string, string>());

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FlowActivity)).Assembly;
            string[] uwpResources = GetResourceStrings(assembly);

            Dictionary<string, string> rules = uwpResources
                                                .Where(f => f.EndsWith(".module"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            Dictionary<string, string> resources = uwpResources
                                                .Where(f => f.EndsWith(".resources"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            int count = 0;
            foreach (string key in rules.Keys)
            {
                await rulesLoader.LoadRulesOnStartUp
                (
                    new RulesModuleModel
                    {
                        Name = key,
                        ResourceSetFile = GetBytes(resources[key], assembly),
                        RuleSetFile = GetBytes(rules[key], assembly)
                    },
                    cache
                );
                count++;
                //Progress = (double)count / rules.Count;
            }

            return cache;

            string GetKey(string fullResourceName)
                => Path.GetExtension(Path.GetFileNameWithoutExtension(fullResourceName)).Substring(1);
        }

        private static string[] GetResourceStrings(Assembly assembly)
         => assembly.GetManifestResourceNames()
                    .Where
                    (
                        res => res.StartsWith
                        (
                            "Contoso.Bsl.Flow.Rulesets.",
                            System.StringComparison.InvariantCultureIgnoreCase
                        )
                    ).ToArray();

        private static byte[] GetBytes(string file, Assembly assembly)
        {
            using (Stream platformStream = assembly.GetManifestResourceStream(file))
            {
                byte[] byteArray = new byte[platformStream.Length];
                platformStream.Read(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }
    }
}
