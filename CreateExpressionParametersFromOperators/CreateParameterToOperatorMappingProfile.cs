using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CreateExpressionParametersFromOperators
{
    static class CreateParameterToOperatorMappingProfile
    {
        internal static void Write()
        {
            List<Type> types = typeof(LogicBuilder.Expressions.Utils.ExpressionBuilder.ParameterOperator).Assembly.GetTypes()
                .Where
                (
                    p => p.Namespace != null &&
                    p.Namespace.StartsWith("LogicBuilder.Expressions.Utils.ExpressionBuilder")
                    && !p.IsEnum
                    && !p.IsGenericTypeDefinition
                    && !p.IsInterface
                    && p.FullName.EndsWith("Operator")
                    && Attribute.GetCustomAttribute(p, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null
                )
                .OrderBy(type => type.Name)
                .ToList();

            WriteProfile(types);
        }

        static readonly string MAPPING_SAVE_PATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.AutoMapperProfiles";

        private static void WriteProfile(List<Type> types)
        {
            List<string> createMapStatements = types.Select
            (
                type =>
                {
                    var constructorInfo = type.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length)
                    .First();

                    var parameters = constructorInfo.GetParameters();
                    if (parameters.Length > 0 && parameters[0].Name == "parameters" && parameters[0].ParameterType == typeof(IDictionary<string, ParameterExpression>))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append($"\t\t\tCreateMap<{type.Name.Replace("Operator", "OperatorParameter")}, {type.Name}>()");
                        sb.Append($"{Environment.NewLine}\t\t\t\t.ConstructUsing");
                        sb.Append($"{Environment.NewLine}\t\t\t\t(");
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t(src, context) => new {type.Name}");
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t(");
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t\t(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY]");
                        sb.Append(GetRemainingParameters(parameters.Skip(1)));
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t)");
                        sb.Append($"{Environment.NewLine}\t\t\t\t)");
                        sb.Append($"{Environment.NewLine}\t\t\t\t.ForAllMembers(opt => opt.Ignore());");
                        sb.Append($"{Environment.NewLine}");

                        return sb.ToString();
                    }

                    return $"\t\t\tCreateMap<{type.Name.Replace("Operator", "OperatorParameter")}, {type.Name}>();";
                })
            .ToList();

            string GetRemainingParameters(IEnumerable<ParameterInfo> parameters)
            {
                if (!parameters.Any())
                    return string.Empty;

                StringBuilder sb = new StringBuilder();
                sb.Append($",{Environment.NewLine}\t\t\t\t\t\t");

                sb.Append
                (
                    string.Join
                    (
                        $",{Environment.NewLine}\t\t\t\t\t\t",
                        parameters.Select(p => GetParameterString(p))
                    )
                );

                return sb.ToString();
            }

            string GetParameterString(ParameterInfo parameter)
            {
                if (parameter.ParameterType == typeof(IExpressionPart))
                    return $"context.Mapper.Map<IExpressionPart>(src.{FirstCharToUpper(parameter.Name)})";

                return $"src.{FirstCharToUpper(parameter.Name)}";
            }

            string FirstCharToUpper(string parameterName)
            {
                return $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";
            }

            List<string> includeMapStatements = types.Select
            (
                type => $"\t\t\t\t.Include<{type.Name.Replace("Operator", "OperatorParameter")}, {type.Name}>()"
            )
            .ToList();

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\MappingProfileTemplate.txt")
                .Replace("#Mappings#", string.Join(Environment.NewLine, createMapStatements))
                .Replace("#DescriptorToPartIncludes#", $"{string.Join(Environment.NewLine, includeMapStatements)};");

            using (StreamWriter sr = new StreamWriter($@"{MAPPING_SAVE_PATH}\ParameterToOperatorMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
                sr.Close();
            }

        }
    }
}
