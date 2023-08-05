using UnityEditor;
using Code.Services.JSONSaver;

namespace Project.Editor
{
    public class ClearDataTool : EditorWindow
    {
        [MenuItem("Tools/Clear Saved Files")]
        public static void ClearAllSavedFiles() => 
            JsonSaver.ClearAllData();
    }
}