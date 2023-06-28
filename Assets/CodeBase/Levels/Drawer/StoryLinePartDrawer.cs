using UnityEditor;

namespace CodeBase.Levels.Drawer
{
    [CustomEditor(typeof(CustomEditor))]
    public sealed class StoryLinePartDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_storyPlotCustomer"));
        }
    }
}