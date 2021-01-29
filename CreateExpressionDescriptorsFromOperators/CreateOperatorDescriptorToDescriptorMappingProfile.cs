using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CreateExpressionDescriptorsFromOperators
{
    static class CreateOperatorDescriptorToDescriptorMappingProfile
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
                    bool hasTypeParameters = parameters.Any(p => p.ParameterType == typeof(Type));

                    StringBuilder sb = new StringBuilder();
                    sb.Append($"\t\t\tCreateMap<{type.Name.Replace("Operator", "OperatorDescriptor")}, {type.Name.Replace("Operator", "Descriptor")}>()");
                    if (!hasTypeParameters)
                        sb.Append(";");

                    else
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter.ParameterType == typeof(Type))
                            {
                                sb.Append($"{Environment.NewLine}\t\t\t\t.ForMember(dest => dest.{FirstCharToUpper(parameter.Name)}, opts => opts.Ignore())");
                                sb.Append($"{Environment.NewLine}\t\t\t\t.ForCtorParam(\"{parameter.Name}\", opts => opts.MapFrom(x => Type.GetType(x.{FirstCharToUpper(parameter.Name)})))");
                            }
                        }

                        sb.Append(";");
                    }


                    return sb.ToString();
                })
            .ToList();

            string FirstCharToUpper(string parameterName)
            {
                return $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";
            }

            List<string> includeMapStatements = types.Select
            (
                type => $"\t\t\t\t.Include<{type.Name.Replace("Operator", "OperatorDescriptor")}, {type.Name.Replace("Operator", "Descriptor")}>()"
            )
            .ToList();

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\OperatorDescriptorToDescriptorMappingProfileTemplate.txt")
                .Replace("#Mappings#", string.Join(Environment.NewLine, createMapStatements))
                .Replace("#DescriptorToPartIncludes#", $"{string.Join(Environment.NewLine, includeMapStatements)};");

            using (StreamWriter sr = new StreamWriter($@"{MAPPING_SAVE_PATH}\OperatorDescriptorToDescriptorMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
                sr.Close();
            }

        }
    }
}
