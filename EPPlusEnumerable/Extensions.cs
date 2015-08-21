namespace EPPlusEnumerable
{
    using System;
    using System.Reflection;

    internal static class Extensions
    {
        public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(element, typeof(T), inherit);
        }

        public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(element, typeof(T));
        }

        public static object GetValue(this PropertyInfo element, Object obj)
        {
            return element.GetValue(obj, BindingFlags.Default, null, null, null);
        }
    }
}
