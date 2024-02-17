using UnityEditor;
using UnityEngine;
using Code.StaticData.Gameplay;

namespace Project.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EntityConfig))]
    public class EntityConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var entityData = (EntityConfig) target;
            GUILayout.Space(5);
            if (GUILayout.Button("Rename ScriptableObject"))
                RenameScriptableObject(entityData);
        }

        private void RenameScriptableObject(EntityConfig entityConfig)
        {
            var newAssetName = $"{ entityConfig.EntityType.ToString()}_Config";
            string assetPath = AssetDatabase.GetAssetPath(entityConfig);
            AssetDatabase.RenameAsset(assetPath, newAssetName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}