﻿//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Contoso.Utils
//{
//    public class BaseSpecifiedConcreteClassConverter<T> : DefaultContractResolver
//    {
//        protected override JsonConverter ResolveContractConverter(Type objectType)
//        {
//            if (typeof(T).IsAssignableFrom(objectType) && !objectType.IsAbstract)
//                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
//            return base.ResolveContractConverter(objectType);
//        }
//    }
//}
