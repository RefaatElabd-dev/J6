﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace J6.BL.Extentions
{
    public static class  DynamicFilterExtensionMethods
    {
        public static IEnumerable<T> FilterByQueryParams<T>(this IEnumerable<T> collection, IQueryCollection queryParams) where T : new()
        {
            var classType = typeof(T);
            var propList = classType.GetProperties();
            //accountNumber ==> accountNumber, AccountNumber ==> accountNumber
            var props = new Dictionary<string, PropertyInfo>(propList.Select(x => new KeyValuePair<string, PropertyInfo>(Char.ToLowerInvariant(x.Name[0]) + x.Name.Substring(1), x)));

            foreach (var param in queryParams)
            {
                if (props.ContainsKey(param.Key))
                {
                    var prop = props[param.Key];
                    if (prop.PropertyType.IsPrimitive)
                    {
                        if (param.Value.Count == 1)
                        {
                            collection = collection.Where(x => prop.GetValue(x, null).ToString() == param.Value.First());
                        }
                        else
                        {
                            var aggregate = new List<T>();
                            foreach (var value in param.Value)
                            {
                                aggregate = aggregate.Union<T>(collection.Where(x => prop.GetValue(x, null).ToString() == value)).ToList();
                            }
                            collection = aggregate.AsEnumerable();
                        }
                    }
                }
            }

            return collection;
        }
    }
}

