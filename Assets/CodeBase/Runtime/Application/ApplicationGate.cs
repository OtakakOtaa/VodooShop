using UnityEditor;
using UnityEngine;

namespace CodeBase.Runtime.Application
{
    public sealed class ApplicationGate
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Bootstrap()
        {
#if UNITY_EDITOR
            EditorWindow.focusedWindow.maximized = true;
#else
            Screen.SetResolution(1920, 1080, true);
#endif
        }
        
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            UnityEngine.Application.Quit();
        }
        
    }
}