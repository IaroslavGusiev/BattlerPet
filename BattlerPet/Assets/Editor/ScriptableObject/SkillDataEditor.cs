using UnityEditor;
using UnityEngine;
using Code.StaticData.Hero;

namespace Project.Editor
{
    [CustomEditor(typeof(SkillData))]
    public class SkillDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var skillData = (SkillData)target;
            GUILayout.Space(10);
            if (GUILayout.Button("Rename SkillData"))
            {
                var newName = $"{skillData.AnimAttackType.ToString()}";
                skillData.Rename(newName);
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(skillData), newName);
            }
        }
    }
}