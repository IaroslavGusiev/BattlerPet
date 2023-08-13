using System;
using UnityEditor;
using UnityEngine;
using Code.Data.Gameplay.Battlefield;

namespace Project.Editor
{
    [CustomEditor(typeof(CubeData))]
    public class CubeTemplateEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var cubeTemplate = (CubeData)target;
            
            GUILayout.Space(20);
            GeneratePrefabResourcePath(cubeTemplate);
            
            GUILayout.Space(10);
            RenameAssetToPrefabName(cubeTemplate);
        }

        private void GeneratePrefabResourcePath(CubeData cubeTemplate)
        {
            if (GUILayout.Button("Generate Prefab Resource Path"))
            {
                const string resourcesFolder = "/Resources/";
                string prefabPath = AssetDatabase.GetAssetPath(cubeTemplate.Prefab);
                int resourcesIndex = prefabPath.IndexOf(resourcesFolder, StringComparison.OrdinalIgnoreCase);
                if (resourcesIndex != -1)
                {
                    prefabPath = prefabPath[(resourcesIndex + resourcesFolder.Length)..];
                    int dotIndex = prefabPath.LastIndexOf('.');
                    if (dotIndex != -1)
                    {
                        prefabPath = prefabPath.Substring(0, dotIndex);
                    }
                }
                cubeTemplate.PrefabPath = prefabPath;
                EditorUtility.SetDirty(target);
            }
        }

        private void RenameAssetToPrefabName(CubeData cubeTemplate)
        {
            if (GUILayout.Button("Rename Asset to Prefab Name + Data"))
            {
                string prefabName = cubeTemplate.Prefab.name;
                prefabName = System.Text.RegularExpressions.Regex.Replace(prefabName, "[^a-zA-Z0-9]", "");
                string assetPath = AssetDatabase.GetAssetPath(target);
                string newAssetName = prefabName + "Data";
                AssetDatabase.RenameAsset(assetPath, newAssetName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}