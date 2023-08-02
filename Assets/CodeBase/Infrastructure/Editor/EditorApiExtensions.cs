using System;
using UnityEditor;

namespace CodeBase.Infrastructure.Editor
{
    public static class CustomEditorExtensions
    {
        public static TValue GetRefValue<TValue>(this SerializedProperty target)
        {
            var rootObject = target.serializedObject.targetObject;
            var property = rootObject.GetType().GetProperty(target.propertyPath) ?? throw new Exception();

            return (TValue)property.GetValue(rootObject);
        }
    }
}