using UnityEngine;

namespace _Project.Scripts.Extension.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ReadOnlyFieldAttribute : PropertyAttribute { }
}