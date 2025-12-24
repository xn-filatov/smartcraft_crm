using System.Diagnostics;

namespace SmartCraft.Application.Extensions;

/// <summary>
/// object extensions
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Get current method name
    /// </summary>
    /// <param name="this">object instance</param>
    /// <returns>name of method</returns>
    public static string GetCurrentMethodName(this object @this)
    {
        var method = new StackTrace(1)?.GetFrame(0)?.GetMethod();
        return $"{method?.DeclaringType?.FullName}.{method?.Name}";
    }

    /// <summary>
    /// Get generic type name
    /// </summary>
    /// <param name="type">type</param>
    /// <returns>type name</returns>
    public static string GetGenericTypeName(this Type type)
    {
        string typeName;

        if (type.IsGenericType)
        {
            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
            typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }
        else
        {
            typeName = type.Name;
        }

        return typeName;
    }

    /// <summary>
    /// Get generic type name from object
    /// </summary>
    /// <param name="object">object instance</param>
    /// <returns>type name</returns>
    public static string GetGenericTypeName(this object @object)
    {
        return @object.GetType().GetGenericTypeName();
    }

    /// <summary>
    /// Generate current instance key name
    /// </summary>
    /// <param name="this">object instance</param>
    /// <param name="subKeys">sub key objects</param>
    /// <returns>generated key</returns>
    public static string GetInstanceKeyName(this object @this, params object[] subKeys)
    {
        var method = new StackTrace(1)?.GetFrame(0)?.GetMethod();
        var allSubKeysStr = string.Join(":", subKeys.Select(x => x.ToString()));
        return $"{method?.DeclaringType?.Name}.{method?.Name}:{allSubKeysStr}";
    }

    /// <summary>
    /// Generate key name from params
    /// </summary>
    /// <param name="this">object instance</param>
    /// <param name="subKeys">sub key objects</param>
    /// <returns>generated key</returns>
    public static string GetKeyName(this object @this, params object[] subKeys)
    {
        return string.Join(":", subKeys.Select(x => x.ToString()));
    }
}
