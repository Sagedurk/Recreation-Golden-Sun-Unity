using UnityEngine;
using UnityEditor;
using System.Collections;

public class DialogueMasterWindow : EditorWindow
{
        [MenuItem("Window/SageSys/Dialogue Master")]

        public static void ShowWindow()
        {
            EditorWindow.GetWindow<DialogueMasterWindow>("Dialogue Master");
        }

        private void OnGUI()
        {


            
        }
}
