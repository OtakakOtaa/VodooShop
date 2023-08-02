using System.Linq;
using CodeBase.Configuration.Data.MainConfig;
using UnityEditor;

namespace CodeBase.Configuration.GameRemoteConfigurationLoader
{
    public static class ConfigLoaderMenu
    {
        [MenuItem("Internal tools/Open Google Sheets")]
        private static void OpenGoogleSheets()
        {
            System.Diagnostics.Process.Start(GameConfigOriginLoader.OriginSettings.RootUrl);
        }

        [MenuItem("Internal tools/Fetch Config from google.docs")]
        private static void FetchGameConfiguration()
        {
            StartFetching();
            async void StartFetching()
            {
                var config = await new GameConfigOriginLoader().FetchConfig();

                var configPath = $"Assets/Resources/{GameConfigResolver.MainConfigurationPatch}.asset";
                var hasConfiguration = AssetDatabase.FindAssets(GameConfigResolver.MainConfigurationPatch).FirstOrDefault() is not null;
                if(hasConfiguration)
                    AssetDatabase.DeleteAsset(configPath);
                AssetDatabase.CreateAsset(config, configPath);
                
                const string successMessage = "Config fetched and saved in assets";
                EditorUtility.DisplayDialog(nameof(FetchGameConfiguration), successMessage, "OK");
            }
        }
    }
}