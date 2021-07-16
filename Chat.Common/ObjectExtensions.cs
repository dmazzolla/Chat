using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Chat.Common
{
    public static class ObjectExtensions
    {

        ///// <summary>
        ///// Converte o valor em string evitando erro de NullReference. Se for nulo uma string vazia será retornada
        ///// </summary>
        ///// <param name="input">Valor do objeto</param>
        ///// <returns></returns>
        //public static string ToStringN(this object input)
        //{
        //    if (input == null)
        //        return string.Empty;

        //    return input.ToString();
        //}

        ///// <summary>
        ///// Converte o valor em string evitando erro de NullReference. Se for nulo uma string vazia será retornada
        ///// </summary>
        ///// <param name="input">Valor do objeto</param>
        ///// <param name="output">Valor de saída caso esteja nulo</param>
        ///// <returns></returns>
        //public static string ToStringN(this object input, string output = "")
        //{
        //    if (input == null)
        //        return output;

        //    return input.ToString();
        //}

        ///// <summary>
        ///// Retorna o primeiro valor não nulo encontrado como string. Nunca retorna nulo.
        ///// </summary>
        ///// <param name="input">Valor do objeto</param>
        ///// <returns></returns>
        //public static string Coalesce(this object input, params object[] args)
        //{
        //    if (input != null)
        //        return input.ToStringN();

        //    foreach (object arg in args)
        //    {
        //        if (arg != null)
        //            return arg.ToStringN();
        //    }

        //    return string.Empty;
        //}

        ///// <summary>
        ///// Lista todas as propriedades de um objeto, incluindo as propriedades dos objetos filhos
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static List<PropertyInfo> GetAllProperties(this object obj)
        //{
        //    return GetProperties(obj);
        //}
        //private static List<PropertyInfo> GetProperties(object obj)
        //{
        //    List<PropertyInfo> props = new List<PropertyInfo>();
        //    if (obj == null) return props;

        //    Type objType = obj.GetType();
        //    PropertyInfo[] properties = objType.GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        props.Add(property);
        //        object propValue = property.GetValue(obj, null);

        //        props.AddRange(GetProperties(propValue));
        //    }

        //    return props;
        //}

        ///// <summary>
        ///// Verifica se o objeto possui determinada propriedade, analisando uma string que contem o caminho da propriedade. Funciona recursivamente
        ///// </summary>
        ///// <param name="propertyNameOrPath">Nome ou caminho completo da propriedade. Ex: Name ou Bill.PayMode.Name</param>
        ///// <returns></returns>
        //public static bool HasProperty(this object obj, string propertyNameOrPath)
        //{
        //    if (propertyNameOrPath.Contains("."))
        //    {
        //        var className = propertyNameOrPath.Substring(0, propertyNameOrPath.IndexOf('.'));
        //        string propertyName = propertyNameOrPath.Substring(propertyNameOrPath.IndexOf('.') + 1);
        //        var prop = obj.GetType().GetProperty(className);
        //        if (prop != null)
        //        {
        //            var propObj = prop.GetValue(obj, null);
        //            return propObj.HasProperty(propertyName);
        //        }
        //        return false;
        //    }
        //    else
        //    {
        //        var prop = obj.GetType().GetProperty(propertyNameOrPath);
        //        return prop != null;
        //    }
        //}

        ///// <summary>
        ///// Pega o valor da propriedade na instancia atual. Funciona recursivamente
        ///// </summary>
        ///// <param name="propertyNameOrPath">Nome ou caminho da propriedade. Ex: Name ou PayMode.Name</param>
        ///// <returns></returns>
        //public static object GetPropertyValueByPath(this object obj, string propertyNameOrPath)
        //{
        //    if (propertyNameOrPath.Contains("."))
        //    {
        //        var className = propertyNameOrPath.Substring(0, propertyNameOrPath.IndexOf('.'));
        //        string propertyName = propertyNameOrPath.Substring(propertyNameOrPath.IndexOf('.') + 1);
        //        var prop = obj.GetType().GetProperty(className);
        //        if (prop != null)
        //        {
        //            var propObj = prop.GetValue(obj, null);
        //            return propObj.GetPropertyValueByPath(propertyName);
        //        }
        //        return prop;
        //    }
        //    else
        //    {
        //        var prop = obj.GetType().GetProperty(propertyNameOrPath);
        //        return prop.GetValue(obj, null);
        //    }
        //}

        ///// <summary>
        ///// Pega o valor da propriedade na instancia atual. Funciona recursivamente
        ///// </summary>
        ///// <param name="propertyNameOrPath">Nome ou caminho da propriedade. Ex: Name ou PayMode.Name</param>
        ///// <returns></returns>
        //public static PropertyInfo GetPropertyByPath(this object obj, string propertyNameOrPath, out object actualObject)
        //{
        //    if (obj == null)
        //    {
        //        actualObject = null;
        //        return null;
        //    }

        //    if (propertyNameOrPath.Contains("."))
        //    {
        //        var className = propertyNameOrPath.Substring(0, propertyNameOrPath.IndexOf('.'));
        //        string propertyName = propertyNameOrPath.Substring(propertyNameOrPath.IndexOf('.') + 1);
        //        var prop = obj.GetType().GetProperty(className);
        //        if (prop != null)
        //        {
        //            var propObj = prop.GetValue(obj, null);
        //            return propObj.GetPropertyByPath(propertyName, out actualObject);
        //        }
        //        actualObject = null;
        //        return prop;
        //    }
        //    else
        //    {
        //        actualObject = obj;
        //        var prop = obj.GetType().GetProperty(propertyNameOrPath);
        //        return prop;
        //    }
        //}

        ///// <summary>
        ///// Verifica se um objeto é do tipo Collection
        ///// </summary>
        ///// <param name="o"></param>
        ///// <returns></returns>
        //public static bool IsCollection(this object o)
        //{
        //    return typeof(ICollection).IsAssignableFrom(o.GetType())
        //        || typeof(ICollection<>).IsAssignableFrom(o.GetType());
        //}


        ///// <summary>
        ///// Verifica se um objeto é do tipo Collection
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public static bool IsCollectionType(this Type t)
        //{
        //    return typeof(ICollection).IsAssignableFrom(t)
        //        || typeof(ICollection<>).IsAssignableFrom(t);
        //}


        ///// <summary>
        ///// Pega o valor da propriedade Id
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static object GetId(this object input)
        //{
        //    //Pega primeiro a propriedade declara no próprio objeto, se exitir
        //    PropertyInfo property = input.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //    //Senão pega a da classe base
        //    if (property == null)
        //        property = input.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance );

        //    if (property == null)
        //        throw new ArgumentException("A classe do tipo " + input.GetType().Name + " não possui uma propriedade Id.");

        //    return property.GetValue(input, null);
        //}

        ///// <summary>
        ///// Seta o valor da propriedade Id
        ///// </summary>
        ///// <param name="input"></param>
        ///// <param name="value">Valor do Id</param>
        ///// <returns></returns>
        //public static void SetId(this object input, object value)
        //{
        //    //Pega primeiro a propriedade declara no próprio objeto, se exitir
        //    PropertyInfo property = input.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //    //Senão pega a da classe base
        //    if (property == null)
        //        property = input.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

        //    if (property == null)
        //        throw new ArgumentException("A classe do tipo " + input.GetType().Name + " não possui uma propriedade Id.");

        //    property.SetValue(input, value, null);
        //}



        //public static void CopyBaseTypeTo(this object input, object to)
        //{
        //    Type type = input.GetType().BaseType;
        //    while (type != null)
        //    {
        //        UpdateForType(type, input, to);
        //        type = type.BaseType;
        //    }
        //}

        //static void UpdateForType(Type type, object source, object destination)
        //{
        //    FieldInfo[] myObjectFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        //    //myObjectFields.ForEach(fi => { try { fi.SetValue(destination, fi.GetValue(source)); } catch { } });
        //    foreach (FieldInfo fi in myObjectFields)
        //    {
        //        try { fi.SetValue(destination, fi.GetValue(source)); } catch { }
        //    }
        //}

        //public static void UpdateCorrespondingFieldValuesTo(this object input, object to)
        //{
        //    Type type = input.GetType();
        //    while (type != null)
        //    {
        //        UpdateCorrespondingField(type, input, to);
        //        type = type.BaseType;
        //    }
        //}

        //static void UpdateCorrespondingField(Type type, object source, object destination)
        //{
        //    Type typeN1 = destination.GetType();
        //    Type typeN2 = null;
        //    Type typeN3 = null;

        //    if (typeN1.BaseType != null)
        //    {
        //        typeN2 = typeN1.BaseType;
        //        if (typeN2.BaseType != null)
        //        {
        //            typeN3 = typeN2.BaseType;            
        //        }
        //    }

        //    FieldInfo[] myObjectFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        //    foreach (FieldInfo fi in myObjectFields)
        //    {
        //        string name = fi.Name.GetStringBetweenStrings("<", ">");

        //        PropertyInfo property;

        //        property = typeN1.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //        if (property != null)
        //        {
        //            try { property.SetValue(destination, fi.GetValue(source), null); } catch { }
        //            continue;
        //        }
        //        else
        //        {
        //            if (typeN2 == null) continue;
        //            property = typeN2.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //            if (property != null)
        //            {
        //                try { property.SetValue(destination, fi.GetValue(source), null); } catch { }
        //                continue;
        //            }
        //            else
        //            {
        //                if (typeN3 == null) continue;
        //                property = typeN3.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        //                if (property != null)
        //                {
        //                    try { property.SetValue(destination, fi.GetValue(source), null); } catch { }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
