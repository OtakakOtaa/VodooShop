using CodeBase.Runtime;
using CodeBase.Runtime.Configuration;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor.OriginGameConfig
{
    public static class ConfigLoadingStartPoint
    {
        [MenuItem("Internal tools/Fetch Config from google.docs")]
        private static void FetchGameConfiguration()
        {
            StartFetching();
            async void StartFetching()
            {
                GameConfiguration config = await new GameConfigOriginLoader().FetchConfig();
                AssetDatabase.DeleteAsset(GameConfigProvider.MainConfigurationPatch);
                AssetDatabase.CreateAsset(config, GameConfigProvider.MainConfigurationPatch); 
                
                Rect popupSettings = new (Screen.width / 2, Screen.height / 2, 4, 2);
                const string successMessage = "Config fetched and saved in assets"; 
                PopupWindow.Show(popupSettings, new OperationResultPopup(successMessage));
            }
        }

        private class OperationResultPopup : PopupWindowContent
        {
            private readonly string _notificationMassage;

            public OperationResultPopup(string notificationMassage)
                => _notificationMassage = notificationMassage;

            public override void OnGUI(Rect rect)
                => GUILayout.Label(_notificationMassage);
        }
    }
}