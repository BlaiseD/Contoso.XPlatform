﻿using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.ItemFilter;
using System;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal static class CreateItemFilterHelper
    {
        private static readonly string parameterName = "f";

        public static FilterLambdaOperatorDescriptor CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity)
            => new FilterLambdaOperatorDescriptor
            {
                FilterBody = CreateFilterGroupBody(descriptor, entity),
                ParameterName = parameterName,
                SourceElementType = modelType.AssemblyQualifiedName
            };

        private static OperatorDescriptorBase CreateFilterGroupBody(ItemFilterGroupDescriptor descriptor, object entity)
        {
            if (descriptor?.Filters?.Any() != true)
                throw new ArgumentException($"{nameof(descriptor.Filters)}: 165BB6D3-1D2F-4EEB-B546-102825505ED2");

            if (descriptor.Filters.Count > 2)
                throw new ArgumentException($"{nameof(descriptor.Filters)}: 1EAC3591-1BBE-412A-A915-C779E3463FB7");

            if (descriptor.Filters.Count == 1)
                return CreateBody(descriptor.Filters.First()); ;

            return SetMembers
            (
                GetLogicBinaryOperatorDescriptor(descriptor.Logic)
            );

            BinaryOperatorDescriptor SetMembers(BinaryOperatorDescriptor binaryOperator)
            {
                binaryOperator.Left = CreateBody(descriptor.Filters.First());
                binaryOperator.Right = CreateBody(descriptor.Filters.Last());

                return binaryOperator;
            }

            OperatorDescriptorBase CreateBody(ItemFilterDescriptorBase filterDescriptorBase)
            {
                return filterDescriptorBase switch
                {
                    ValueSourceFilterDescriptor valueSourceFilterDescriptor => CreateValueFilterBody(valueSourceFilterDescriptor),
                    MemberSourceFilterDescriptor memberSourceFilterDescriptor => CreateMemberSourceFilterBody(memberSourceFilterDescriptor, entity),
                    ItemFilterGroupDescriptor itemFilterGroupDescriptor => CreateFilterGroupBody(itemFilterGroupDescriptor, entity),
                    _ => throw new ArgumentException($"{nameof(filterDescriptorBase)}: 7EB79C70-91D4-4C9E-96AD-C5ABF1D8603A"),
                };
            }
        }

        private static OperatorDescriptorBase CreateValueFilterBody(ValueSourceFilterDescriptor descriptor)
        {
            return SetMembers
            (
                GetOperatorBinaryOperatorDescriptor(descriptor.Operator)
            );

            BinaryOperatorDescriptor SetMembers(BinaryOperatorDescriptor binaryOperator)
            {
                binaryOperator.Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor
                    {
                        ParameterName = parameterName
                    },
                    MemberFullName = descriptor.Field
                };
                binaryOperator.Right = new ConstantOperatorDescriptor
                {
                    ConstantValue = descriptor.Value,
                    Type = descriptor.Type
                };

                return binaryOperator;
            }
        }

        private static OperatorDescriptorBase CreateMemberSourceFilterBody(MemberSourceFilterDescriptor descriptor, object entity)
        {
            return SetMembers
            (
                GetOperatorBinaryOperatorDescriptor(descriptor.Operator)
            );

            BinaryOperatorDescriptor SetMembers(BinaryOperatorDescriptor binaryOperator)
            {
                binaryOperator.Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor
                    {
                        ParameterName = parameterName
                    },
                    MemberFullName = descriptor.Field
                };
                binaryOperator.Right = new ConstantOperatorDescriptor
                {
                    ConstantValue = entity.GetPropertyValue(descriptor.MemberSource),
                    Type = descriptor.Type
                };

                return binaryOperator;
            }
        }

        private static BinaryOperatorDescriptor GetOperatorBinaryOperatorDescriptor(string oper) 
            => oper.ToLowerInvariant() switch
            {
                Operators.eq => new EqualsBinaryOperatorDescriptor(),
                Operators.neq => new NotEqualsBinaryOperatorDescriptor(),
                _ => throw new ArgumentException($"{nameof(oper)}: 85953974-7864-4D0A-9B60-1C1D746FD8D1"),
            };

        private static BinaryOperatorDescriptor GetLogicBinaryOperatorDescriptor(string logic) 
            => logic.ToLowerInvariant() switch
            {
                Logic.and => new AndBinaryOperatorDescriptor(),
                Logic.or => new OrBinaryOperatorDescriptor(),
                _ => throw new ArgumentException($"{nameof(logic)}: F937E0EC-734C-406C-90B6-2C6D8DEC4541"),
            };

        private struct Operators
        {
            public const string eq = "eq";
            public const string neq = "neq";
        }

        private struct Logic
        {
            public const string or = "or";
            public const string and = "and";
        }
    }
}
