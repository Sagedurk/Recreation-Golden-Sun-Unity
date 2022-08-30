using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections;
using System;
using UnityEditor.UIElements;

public class DialogueMasterWindow : EditorWindow
{
    public static Interactable dialogueInstance;

    private readonly string defaultFileName = "DialogueFileName";
    private TextField fileNameTextField;
    private Button saveButton;

    [MenuItem("Window/SageSys/Dialogue Master")]
    public static void ShowWindow()
    {
        GetWindow<DialogueMasterWindow>("Dialogue Master");
    }

    private void CreateGUI()
    {
        
        AddGraphView();
        AddToolbar();

        
        //Debug.Log(rootVisualElement.localBound);
        //AddStyles();
    }

    

    private void OnGUI()
    {

        //if(dialogueInstance != null)
        //    GUILayout.Label("Name of instance: " + dialogueInstance.name);


        //if (GUILayout.Button("Clear Instance"))
        //{
        //    dialogueInstance = null;
        //}            
    }

    #region Elements Addition
    private void AddGraphView()
    {
        DialogueMasterGraphView graphView = new DialogueMasterGraphView(this);
        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);

    }

    private void AddToolbar()
    {
        Toolbar toolbar = new Toolbar();
        fileNameTextField = DialogueElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
        {
            fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();

        });

        saveButton = DialogueElementUtility.CreateButton("Save");

        toolbar.Add(fileNameTextField);
        toolbar.Add(saveButton);

        toolbar.AddStyleSheets("Dialogue SageSys/DialogueToolbarStyles.uss");

        rootVisualElement.Add(toolbar);
        
    }

    private void AddStyles()
    {
        rootVisualElement.AddStyleSheets("Dialogue SageSys/DialogueVariables.uss");

    }

    #endregion


    #region Utility Methods
    public void EnableSaving()
    {
        saveButton.SetEnabled(true);
    }

    public void DisableSaving()
    {
        saveButton.SetEnabled(false);
    }

    #endregion


}
