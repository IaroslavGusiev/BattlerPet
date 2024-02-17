using UnityEditor;
using UnityEngine;
using Code.StaticData.Gameplay;

namespace Project.Editor
{
    [CustomEditor(typeof(SkillConfig))]
    public class SkillDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var skillData = (SkillConfig)target;
            GUILayout.Space(10);
            if (GUILayout.Button("Rename SkillData"))
            {
                var newName = $"{skillData.AttackType + "_" + skillData.EntityType}";
                skillData.Rename(newName);
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(skillData), newName);
                EditorUtility.SetDirty(skillData);
            }
        }
    }
}