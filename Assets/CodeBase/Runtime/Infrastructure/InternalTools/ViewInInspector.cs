using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Runtime.Infrastructure.InternalTools
{
    public class ViewInInspector : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(ViewInInspector))]
    public sealed class ViewInInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    EditorGUI.LabelField(position, label.text, property.intValue.ToString());
                    return;
                case SerializedPropertyType.String:
                    EditorGUI.LabelField(position, label.text, property.stringValue);
                    return;
            }
        }
    }
}