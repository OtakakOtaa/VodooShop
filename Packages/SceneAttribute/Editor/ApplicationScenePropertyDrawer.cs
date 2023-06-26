using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ApplicationScene))]
    public sealed class ApplicationScenePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                var sceneAttribute = (ApplicationScene)attribute;
                
                EditorGUI.BeginProperty(position, label, property);

                var ifAssetRefCleared = property.stringValue is not null && sceneAttribute.SceneAssetPath is null;
                if (ifAssetRefCleared)
                {
                     var guid = AssetDatabase.FindAssets(property.stringValue).First();
                     sceneAttribute.SceneAssetPath = AssetDatabase.GUIDToAssetPath(guid);
                }

                EditorGUI.BeginChangeCheck();
                var target = EditorGUI.ObjectField(position,
                    label,
                    AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneAttribute.SceneAssetPath),
                    typeof(SceneAsset), false);

                if (EditorGUI.EndChangeCheck())
                {
                    sceneAttribute.SceneAssetPath = AssetDatabase.GetAssetPath(target);
                    property.stringValue = target.name;
                }
                
                EditorGUI.EndProperty();
            }
            else
                Debug.Log("[ApplicationScene] Attribute applied to non-string");
        }
    }
}