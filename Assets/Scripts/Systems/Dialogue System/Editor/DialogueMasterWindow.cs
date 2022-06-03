using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections;
using System;

public class DialogueMasterWindow : EditorWindow
{
    public static Interactable dialogueInstance;
   


    [MenuItem("Window/SageSys/Dialogue Master")]
    public static void ShowWindow()
    {
        GetWindow<DialogueMasterWindow>("Dialogue Master");
    }

    private void CreateGUI()
    {
        AddGraphView();
        //AddStyles();
    }

    

    private void OnGUI()
    {
        if(dialogueInstance != null)
            GUILayout.Label("Name of instance: " + dialogueInstance.name);


        if (GUILayout.Button("Clear Instance"))
        {
            dialogueInstance = null;
        }            
    }

    #region Elements Addition
    private void AddGraphView()
    {
        DialogueMasterGraphView graphView = new DialogueMasterGraphView();
        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
        

    }
    private void AddStyles()
    {
        rootVisualElement.AddStyleSheets("Dialogue SageSys/DialogueVariables.uss");

    }

    #endregion
}
