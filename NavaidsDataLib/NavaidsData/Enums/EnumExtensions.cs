﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavaidsDataLib
{
    public static class EnumExtensions
    {
        private static Dictionary<string, IEnumCache> keysDictionary = new Dictionary<string, IEnumCache>();

        public static T GetTagKeyEnum<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return (T)Enum.ToObject(typeof(T), 0);
            }

            EnumCache<T> cache = null;
            if (keysDictionary.ContainsKey(typeof(T).Name))
            {
                cache = keysDictionary[typeof(T).Name] as EnumCache<T>;
            }


            if (cache == null)
            {
                cache = new EnumCache<T>();

                var enumValues = typeof(T).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);//Enum.GetValues(typeof(TagKeyEnum));
                if (enumValues != null)
                {
                    foreach (var enumValue in enumValues)
                    {

                        var attr = enumValue.GetCustomAttributes(typeof(EnumAttribute), false);
                        if (attr != null && attr.Length > 0)
                        {
                            EnumAttribute keyEnumAttr = attr[0] as EnumAttribute;
                            if (keyEnumAttr != null)
                            {
                                try
                                {
                                    cache.keys[keyEnumAttr.Key] = (T)Enum.Parse(typeof(T), enumValue.Name);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                }
                keysDictionary[typeof(T).Name] = cache;
            }
            foreach (var key in cache.keys)
            {
                if (key.Key == value)
                {
                    return key.Value;
                }
            }

            return (T)Enum.ToObject(typeof(T), 0);
        }

        public static string GetEnumAttributeValue<T>(T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            var field = obj.GetType().GetField(obj.ToString(), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            if (field != null)
            {
                var attrs = field.GetCustomAttributes(typeof(EnumAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    var attr = attrs[0] as EnumAttribute;
                    if (attr != null)
                    {
                        return attr.Key;
                    }
                }
            }

            return obj.ToString();


        }
    }

    [System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumAttribute : Attribute
    {
        // This is a positional argument
        public EnumAttribute(string key)
        {
            Key = key;
        }

        // This is a named argument
        public string Key { get; set; }
    }
}
