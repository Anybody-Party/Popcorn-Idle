using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Compilation;
using UnityEngine;

namespace _Project.Scripts.Analytics.Editor
{
    public class SdkConfigurationWindow : EditorWindow
    {
        private static Dictionary<SdkConfiguration, bool> _sdkList;
        private static string[] _buildDefines;

        private void OnGUI()
        {
            if (_sdkList == null)
                Init();

            var sdkListClone = new Dictionary<SdkConfiguration, bool>(_sdkList);
            foreach ((SdkConfiguration config, bool active) in sdkListClone)
            {
                _sdkList[config] = GUILayout.Toggle(active, $"Use {config.Name}");
            }

            if (GUILayout.Button("Compile"))
                Compile();
        }

        private static void Compile()
        {
            List<string> newDefines = _buildDefines
                .Where(buildDefine => _sdkList.All(sdk => sdk.Key.Define != buildDefine))
                .ToList();

            AddDefinesToCollectionIfActive(_sdkList, newDefines);
            _buildDefines = newDefines.ToArray();
            PlayerSettings.SetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                _buildDefines);
            CompilationPipeline.RequestScriptCompilation();
        }

        private static void AddDefinesToCollectionIfActive(Dictionary<SdkConfiguration, bool> defines,
            ICollection<string> collection)
        {
            foreach ((SdkConfiguration config, bool active) in defines)
            {
                if (active)
                    collection.Add(config.Define);
            }
        }


        [MenuItem("Tools/SDK Configuration Window")]
        private static void ShowWindow()
        {
            Init();
            GetWindow<SdkConfigurationWindow>("SDK Configuration Window").Show();
        }

        private static void Init()
        {
            IEnumerable<SdkConfiguration> configs = FindConfigurations();
            _sdkList = new Dictionary<SdkConfiguration, bool>();

            PlayerSettings.GetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                out string[] defines);

            _buildDefines = defines;

            foreach (SdkConfiguration config in configs)
                _sdkList.Add(config, _buildDefines.Contains(config.Define));
        }

        private static IEnumerable<SdkConfiguration> FindConfigurations()
        {
            string sdkListGuid = AssetDatabase.FindAssets($"t:{nameof(SdkList)}")[0];
            string sdkListPath = AssetDatabase.GUIDToAssetPath(sdkListGuid);
            return AssetDatabase.LoadAssetAtPath<SdkList>(sdkListPath).Configurations;
        }
    }
}