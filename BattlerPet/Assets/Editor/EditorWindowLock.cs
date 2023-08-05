using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Project.Editor
{
    public class EditorWindowLock
    {
        private static EditorWindow _activeProject;

        [MenuItem("Hot actions/Toggle Lock %SPACE")]
        private static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        [MenuItem("Hot actions/Toggle Project Lock %#SPACE")]
        private static void ToggleProjectLock()
        {
            if (_activeProject == null)
            {
                Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.ProjectBrowser");
                Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                _activeProject = (EditorWindow) findObjectsOfTypeAll[0];
            }

            if (_activeProject != null && _activeProject.GetType().Name == "ProjectBrowser")
            {
                Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.ProjectBrowser");
                PropertyInfo propertyInfo = type.GetProperty("isLocked", BindingFlags.Instance |BindingFlags.NonPublic |BindingFlags.Public);
                var value = (bool) propertyInfo.GetValue(_activeProject, null);
                propertyInfo.SetValue(_activeProject, !value, null);
                _activeProject.Repaint();
            }
        }
    }
}