using System;
using UnityEditor;
using UnityEngine;
using Code.Gameplay.Battlefield;

#if UNITY_EDITOR
namespace Project.Editor
{
    [CustomEditor(typeof(BattlefieldPartData))]
    public class CubeTemplateEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var cubeTemplate = (BattlefieldPartData) target;

            GUILayout.Space(20);
            GeneratePrefabAddress(cubeTemplate);

            GUILayout.Space(10);
            RenameAssetToPrefabName(cubeTemplate);

            GUILayout.Space(10);
            ExtractMaterialPath(cubeTemplate);
        }

        private void GeneratePrefabAddress(BattlefieldPartData battlefieldPartTemplate)
        {
            if (GUILayout.Button("Generate Prefab Address"))
            {
                UnityEngine.Object prefab = battlefieldPartTemplate.Prefab;
                string prefabPath = AssetDatabase.GetAssetPath(prefab);
                string prefabGuid = AssetDatabase.AssetPathToGUID(prefabPath);
                string prefabAddress = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(prefabGuid)?.address;

                if (prefabAddress != null)
                {
                    battlefieldPartTemplate.PrefabAddress = prefabAddress;
                    EditorUtility.SetDirty(target);
                }
                else
                {
                    Debug.LogError("Prefab is not an Addressable Asset. Make sure it is assigned in the Addressable Assets window.");
                }
            }
        }

        private void RenameAssetToPrefabName(BattlefieldPartData battlefieldPartTemplate)
        {
            if (GUILayout.Button("Rename Asset to Prefab Name + Data"))
            {
                string prefabName = battlefieldPartTemplate.Prefab.name;
                prefabName = System.Text.RegularExpressions.Regex.Replace(prefabName, "[^a-zA-Z0-9]", "");
                string assetPath = AssetDatabase.GetAssetPath(target);
                string newAssetName = prefabName + "Data";
                AssetDatabase.RenameAsset(assetPath, newAssetName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void ExtractMaterialPath(BattlefieldPartData battlefieldPartTemplate)
        {
            if (GUILayout.Button("Extract Material Path"))
            {
                Cube prefab = battlefieldPartTemplate.Prefab;
                if (prefab != null)
                {
                    var meshRenderer = prefab.GetComponentInChildren<MeshRenderer>();
                    if (meshRenderer != null && meshRenderer.sharedMaterial != null)
                    {
                        string materialPath = AssetDatabase.GetAssetPath(meshRenderer.sharedMaterial);
                        const string resourcesFolder = "/Resources/";
                        int resourcesIndex = materialPath.IndexOf(resourcesFolder, StringComparison.OrdinalIgnoreCase);
                        if (resourcesIndex != -1)
                        {
                            materialPath = materialPath[(resourcesIndex + resourcesFolder.Length)..];
                            int dotIndex = materialPath.LastIndexOf('.');
                            if (dotIndex != -1)
                                materialPath = materialPath[..dotIndex];
                        }

                        battlefieldPartTemplate.MaterialPath = materialPath;
                        EditorUtility.SetDirty(target);
                    }
                }
            }
        }
    }
}
#endif
