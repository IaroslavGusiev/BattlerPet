using System;
using UnityEditor;
using UnityEngine;
using Code.StaticData.Hero;

namespace Project.Editor
{
    [CustomEditor(typeof(HeroData))]
    public class HeroDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var heroData = (HeroData)target;
            GUILayout.Space(20);
            if (GUILayout.Button("Set Prefab Path"))
            {
                string prefabAssetPath = AssetDatabase.GetAssetPath(heroData.Prefab);
                if (prefabAssetPath.Contains("Resources"))
                {
                    string relativePath = prefabAssetPath[(prefabAssetPath.IndexOf("Resources", StringComparison.Ordinal) + "Resources".Length + 1)..];
                    relativePath = relativePath.Replace(".prefab", "");
                    heroData.PrefabPath = relativePath;
                    EditorUtility.SetDirty(heroData);
                }
            }
        }
    }
}