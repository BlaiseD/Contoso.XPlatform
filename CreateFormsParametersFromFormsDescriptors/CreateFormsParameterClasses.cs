using LogicBuilder.Expressions.Utils;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CreateFormsParametersFromFormsDescriptors
{
    static class CreateFormsParameterClasses
    {
        internal static void Write()
        {
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                typeof(Contoso.Forms.Configuration.CommandButtonDescriptor).Assembly.GetTypes().Where
                                (
                                    p => p.Namespace != null &&
                                    p.Namespace.StartsWith("Contoso.Forms.Configuration")
                                    && !p.IsEnum
                                    && !p.IsGenericTypeDefinition
                                    && !p.IsInterface
                                    && !p.Namespace.EndsWith(".Json")
                                    && !p.Namespace.EndsWith(".Navigation")
                                    && (p.FullName.EndsWith(DESCRIPTOR) || p.FullName.EndsWith("DescriptorBase"))
                                    && Attribute.GetCustomAttribute(p, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null
                                )
                                .ToList()
                                .ForEach(t =>
                                {
                                    WriteCommonClass(t, compiler);
                                });
            }
        }

        const string PROPERTIES = "#Properties#";
        const string NAMESPACES = "#NameSpaces#";
        const string CONSTRUCTORS = "#Constructors#";
        const string DESCRIPTOR = "Descriptor";
        const string PARAMETER = "Parameter";

        private static void WriteCommonClass(Type type, CSharpCodeProvider compiler)
        {
            string name = type.Name.Replace(DESCRIPTOR, PARAMETER);
            string subFolder = GetSubfolder();

            List<string> propertiesList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite)
                .Aggregate(new List<string>(), (list, p) =>
                {
                    if (p.GetMethod.IsAbstract)
                        list.Add(string.Format("\t\tabstract public {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));
                    else if (p.GetMethod.IsVirtual && p.GetMethod.GetBaseDefinition() == p.GetMethod)
                        list.Add(string.Format("\t\tvirtual public {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));
                    else if (p.GetMethod.GetBaseDefinition() != p.GetMethod)
                        list.Add(string.Format("\t\tpublic override {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));
                    else
                        list.Add(string.Format("\t\tpublic {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));

                    return list;
                });

            List<string> propertyNameList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite)
                .Aggregate(new List<string>(), (list, p) =>
                {
                    list.Add(p.Name);

                    return list;
                });

            HashSet<string> nameSpaces = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite)
                .Select(p => p.PropertyType)
                .Aggregate(new HashSet<string>(), (list, next) =>
                {
                    if (next == typeof(object)
                        || next == typeof(string)
                        || (next.IsLiteralType() && !typeof(Enum).IsAssignableFrom(next) && !(next.IsGenericType && next.GetGenericTypeDefinition() == typeof(Nullable<>))))
                        return list;

                    if (!next.Namespace.Equals("Contoso.Forms.Configuration") 
                    && !next.Namespace.Equals($"Contoso.Forms.Configuration.{subFolder}")
                    && next.Namespace.StartsWith("Contoso.Forms.Configuration"))
                        list.Add($"using {next.Namespace.Replace("Contoso.Forms.Configuration", "Contoso.Forms.Parameters")};");
                    else if (next.Namespace.StartsWith("Contoso.Common.Configuration.ExpressionDescriptors"))
                        list.Add($"using {next.Namespace.Replace("Contoso.Common.Configuration.ExpressionDescriptors", "Contoso.Parameters.Expressions")};");
                    else if (next.Namespace.StartsWith("Contoso.Common.Configuration.ExpansionDescriptors"))
                        list.Add($"using {next.Namespace.Replace("Contoso.Common.Configuration.ExpansionDescriptors", "Contoso.Parameters.Expansions")};");
                    else if (!next.Namespace.StartsWith("Contoso.Forms.Configuration"))
                        list.Add($"using {next.Namespace};");

                    return list;
                });

            string constructorString = GetConstructor(type, compiler, propertyNameList);

            string baseClassString = type.BaseType != typeof(object) ? $" : {type.BaseType.GetNewPropertyClassName(compiler, replaceCommonTypeName)}" : "";

            string nameSpacesString = nameSpaces.Any()
                ? $"{string.Join(Environment.NewLine, nameSpaces.OrderBy(n => n))}{Environment.NewLine}{Environment.NewLine}"
                : "";

            string propertiestring = propertiesList.Any()
                ? $"{Environment.NewLine}{Environment.NewLine}{string.Join(Environment.NewLine, propertiesList)}"
                : "";

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\ParameterClassTemplate.txt")
                .Replace("#Name#", name)
                .Replace(PROPERTIES, propertiestring)
                .Replace(NAMESPACES, nameSpacesString)
                .Replace(CONSTRUCTORS, constructorString)
                .Replace("#Base#", baseClassString.Replace(DESCRIPTOR, PARAMETER));

            text = text.Replace("#Modifier#", type.IsAbstract ? "abstract " : "");

            text = text.Replace("#Folder#", string.IsNullOrEmpty(subFolder) ? "" : $@".{subFolder}");

            string savePath = string.IsNullOrEmpty(subFolder) ? Constants.BASEPATH : $@"{Constants.BASEPATH}\{subFolder}";

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            using (StreamWriter sr = new StreamWriter($"{savePath}\\{name}.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
            }

            string GetSubfolder()
            {
                if (type.Namespace == "Contoso.Forms.Configuration")
                    return "";

                return type.Namespace.Replace("Contoso.Forms.Configuration.", "");
            }
        }

        //private static string GetConstructors(Type type, CSharpCodeProvider compiler, List<string> propertiesList)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append
        //    (
        //        GetConstructor(type, compiler, propertiesList)
        //    );

        //    return sb.ToString();
        //}

        private static string GetConstructor(Type type, CSharpCodeProvider compiler, List<string> propertiesList)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite).ToArray();
            if (!properties.Any())
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append($"\t\tpublic {type.Name.Replace(DESCRIPTOR, PARAMETER)}(");
            IEnumerable<string> parameterStrings = properties.Select(p => GetParameterString(p, compiler));

            sb.Append(string.Join(", ", properties.Select(p => GetParameterString(p, compiler))));
            sb.Append(")");
            //if (type.BaseType == typeof(object))
            //{
            //    sb.Append(string.Join(", ", properties.Select(p => GetParameterString(p, compiler))));
            //    sb.Append(")");
            //}
            //else
            //{
            //    sb.Append(string.Join(", ", parameters.Select(p => GetParameterString(p, compiler))));
            //    sb.Append(")");

            //    IEnumerable<ParameterInfo> baseConstructorParameters = parameters.Where(p => !propertiesList.Contains(FirstCharToUpper(p.Name)));
            //    sb.Append($" : base({string.Join(", ", baseConstructorParameters.Select(p => p.Name))})");
            //}

            sb.Append($"{Environment.NewLine}\t\t{{");
            foreach (PropertyInfo property in properties)
                sb.Append($"{Environment.NewLine}\t\t\t{property.Name} = {FirstCharToLower(property.Name)};");
            //if (type.BaseType == typeof(object))
            //{
            //    foreach (ParameterInfo parameter in parameters)
            //        sb.Append($"{Environment.NewLine}\t\t\t{FirstCharToUpper(parameter.Name)} = {parameter.Name};");
            //}
            //else
            //{
            //    IEnumerable<ParameterInfo> declaringClassConstructorParameters = parameters.Where(p => propertiesList.Contains(FirstCharToUpper(p.Name)));
            //    foreach (ParameterInfo parameter in declaringClassConstructorParameters)
            //        sb.Append($"{Environment.NewLine}\t\t\t{FirstCharToUpper(parameter.Name)} = {parameter.Name};");
            //}

            sb.Append($"{Environment.NewLine}\t\t}}");
            return sb.ToString();

            //string FirstCharToUpper(string parameterName)
            //    => $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";
        }

        private static string GetParameterString(PropertyInfo property, CSharpCodeProvider compiler)
        {
            return $"{property.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName)} {FirstCharToLower(property.Name)}";
        }

        static string FirstCharToLower(string parameterName)
                => $"{parameterName[0].ToString().ToLowerInvariant()}{parameterName.Substring(1)}";

        static readonly Func<string, string> replaceCommonTypeName = oldName =>
        {
            string result = Regex.Replace(oldName, "Descriptor$", PARAMETER);
            result = Regex.Replace(result, "DescriptorBase$", "ParameterBase");
            result = Regex.Replace(result, "Contoso.Forms.Configuration\\.([\\w]+)$", "$1");
            result = Regex.Replace(result, "Contoso.Forms.Configuration\\.[\\w]+.([\\w]+)$", "$1");
            return result.Replace("System.", "");
        };

        private static string GetNewPropertyClassName(this Type propertyType, CSharpCodeProvider compiler, Func<string, string> replace)
        {
            string final = "";
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {//Nullable value type
                final = replace(string.Concat
                                (
                                    compiler.GetTypeOutput(new CodeTypeReference(Nullable.GetUnderlyingType(propertyType))), "?"
                                ));
            }
            else if (propertyType.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other Generic Types that are enumerable i.e. lists, dictionaries, etc
                string init = propertyType.Name.Substring(0, propertyType.Name.IndexOf("`"));
                int count = 0;
                Type[] genericArguments = propertyType.GetGenericArguments();
                System.Text.StringBuilder declaration = genericArguments.Aggregate(new System.Text.StringBuilder(init + "<"), (sb, next) =>
                {
                    count++;
                    if (!next.IsValueType && next != typeof(object) && next != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(next))
                    {
                        sb.Append(replace(next.Name));
                    }
                    else
                    {
                        sb.Append(next.GetNewPropertyClassName(compiler, replace));
                    }

                    sb.Append((count < genericArguments.Length) ? ", " : ">");
                    return sb;
                });

                final = replace(declaration.ToString());
            }
            else if (!propertyType.IsValueType && propertyType != typeof(object) && propertyType != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other reference types
                final = replace(propertyType.Name);
            }
            else
            {//value types
                final = replace(compiler.GetTypeOutput(new CodeTypeReference(propertyType)));
            }

            return final;
        }
    }


    internal struct Constants
    {
        internal const string BASEPATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.Forms.Parameters";
    }
}
