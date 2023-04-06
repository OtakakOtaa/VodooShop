using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class BezierCreatorTool
    {
        [MenuItem("Bezier tools / Create")]
        public static void Create()
            => new GameObject("BeziersCurve").AddComponent<BeziersComponent>();

        [MenuItem("Bezier tools / Add")]
        public static void Add()
        {
            bool isObjectNotSelected = Selection.activeGameObject is null;
            if(isObjectNotSelected) return;

            bool isBeziers = Selection.activeGameObject.TryGetComponent<BeziersComponent>(out var component);
            if(isBeziers is false) return;
            component.ExtendCurve();
        }
        
    }
}