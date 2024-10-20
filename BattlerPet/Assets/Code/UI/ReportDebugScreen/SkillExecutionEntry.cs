using TMPro;
using UnityEngine;

namespace Code.UI.DebugSpace
{
    public class SkillExecutionEntry : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI EntityName { get; set; }
        [field: SerializeField] public TextMeshProUGUI SkillName { get; set; }
        [field: SerializeField] public TextMeshProUGUI TargetsLine { get; set; }
    }
}