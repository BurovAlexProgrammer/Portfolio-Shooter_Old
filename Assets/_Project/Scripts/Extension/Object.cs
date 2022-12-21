using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Object = System.Object;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static void CopyDataFrom(this Object target, Object source)
        {
            var sourceTypeInfo = source.GetType().GetTypeInfo();
            var sourceFields = sourceTypeInfo.DeclaredFields;

            foreach (var field in sourceFields)
            {
                field.SetValue(target, field.GetValue(source));
            }
        }

        public static T ConvertTo<T>(this Object source) where T : new()
        {
            var result = new T();
            var resultTypeInfo = typeof(T).GetTypeInfo();
            var resultFields = resultTypeInfo.GetAllDeclaredFields();

            var sourceTypeInfo = source.GetType().GetTypeInfo();
            var sourceFields = sourceTypeInfo.DeclaredFields;

            foreach (var field in sourceFields)
            {
                if (resultFields.Contains(field))
                    field.SetValue(result, field.GetValue(source));
            }

            return result;
        }

        public static T CopyAsNewInstance<T>(this Object source) where T : new()
        {
            var result = new T();
            var sourceTypeInfo = source.GetType().GetTypeInfo();
            var sourceFields = sourceTypeInfo.DeclaredFields;

            foreach (var field in sourceFields)
            {
                field.SetValue(result, field.GetValue(source));
            }

            return result;
        }
        
        //Use for deserialize via Newtonsoft
        //JsonConvert.DeserializeObject<T>(json, new SOConverter<T>());
        public class ScriptableObjectConverter<T> : CustomCreationConverter<T> where T : ScriptableObject
        {
            public override T Create(Type aObjectType)
            {
                if (typeof(T).IsAssignableFrom(aObjectType))
                    return (T)ScriptableObject.CreateInstance(aObjectType);
                return null;
            }
        }
        
        private static IEnumerable<FieldInfo> GetAllDeclaredFields(this TypeInfo typeInfo)
        {
            //TODO may be optimized
            foreach (var field in typeInfo.DeclaredFields)
            {
                yield return field;
            }

            if (typeInfo.BaseType != null)
            {
                typeInfo = typeInfo.BaseType.GetTypeInfo();
                foreach (var field in typeInfo.DeclaredFields)
                {
                    yield return field;
                }
            }
        }
    }
}