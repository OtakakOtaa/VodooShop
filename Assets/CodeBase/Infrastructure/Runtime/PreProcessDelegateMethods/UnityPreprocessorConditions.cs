using System;

namespace CodeBase.Infrastructure.Runtime.PreProcessDelegateMethods
{
    public static class UnityPreprocessorConditions
    {
        public static void InvokeIfUnityEditor(Action action)
        {
#if UNITY_EDITOR
            action?.Invoke();
#endif
        } 
    }
}