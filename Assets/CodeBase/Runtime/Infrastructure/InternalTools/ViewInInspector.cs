using UnityEditor;
using UnityEngine;

namespace CodeBase.Runtime.Infrastructure.InternalTools
{
    public class ViewInInspector : PropertyAttribute { }
    
    [CustomPropertyDrawer(typeof(ViewInInspector))]
    public class ViewInInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            if(property.propertyType is SerializedPropertyType.Integer)
                EditorGUI.LabelField(position, label.text, property.intValue.ToString());
            if(property.propertyType is SerializedPropertyType.String)
                EditorGUI.LabelField(position, label.text, property.stringValue);
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}