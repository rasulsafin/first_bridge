using System;
using System.Reflection;

namespace DM.Tests.Helpers
{
    public static class CustomExtensions
    {
        /// <summary>
        ///     A T extension method that gets property value.
        /// </summary>
        public static object GetPropertyValue<T>(this T @this, string propertyName)
        {
            Type type = @this.GetType();
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        
            return property.GetValue(@this, null);
        }
    }
}