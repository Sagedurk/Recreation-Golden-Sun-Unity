using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections;
using System;
using System.IO;
using UnityEditor.UIElements;

public class DialogueMasterWindow : EditorWindow
{
    public static Interactable dialogueInstance;
    

    private Button saveButton;
    private Button loadButton;
    private DialogueMasterGraphView graphView;

    [MenuItem("Window/SageSys/Dialogue Master")]
    public static void ShowWindow()
    {
        DialogueMasterWindow window = GetWindow<DialogueMasterWindow>("Dialogue Master");

        window.rootVisualElement.Clear();
        window.CreateGUI();
    }

    private void CreateGUI()
    {
        
        AddGraphView();
        AddToolbar();


        
        //Debug.Log(rootVisualElement.localBound);
        //AddStyles();
    }


    #region Elements Addition
    private void AddGraphView()
    {
        graphView = new DialogueMasterGraphView(this);
        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void AddToolbar()
    {
        Toolbar toolbar = new Toolbar();

        string instanceName = "INSTANCE NOT FOUND";
        string sceneName = "";

        if (dialogueInstance != null)
        {
            sceneName = dialogueInstance.gameObject.scene.name;
            instanceName = sceneName + "_" + dialogueInstance.name;
        }

        Label instanceLabel = new Label("File Name: " + instanceName);


        saveButton = DialogueElementUtility.CreateButton("Save Data", () =>
        {
            if (dialogueInstance == null)
            {
                Debug.LogError("Instance not found, can't save data!");
                return;
            }

            string path = GetAssetFilePath(this) + "/SaveData/";
            graphView.SaveGraphViewData(sceneName, dialogueInstance.name, path);
        }); 
        

        loadButton = DialogueElementUtility.CreateButton("Load Data", () =>
        {
            if (dialogueInstance == null)
            {
                Debug.LogError("Instance not found, can't load data!");
                return;
            }

            string path = GetAssetFilePath(this) + "/SaveData/";
            graphView.LoadGraphViewData(sceneName, dialogueInstance.name, path);

        });

        toolbar.Add(instanceLabel);
        //toolbar.Add(fileNameTextField);
        toolbar.Add(saveButton);
        toolbar.Add(loadButton);
        instanceLabel.AddClasses("Window-toolbar-label");
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


    #region IO

    string GetAssetFilePath(ScriptableObject asset)
    {
        MonoScript ms = MonoScript.FromScriptableObject(asset);
        string scriptFilePath = AssetDatabase.GetAssetPath(ms);
        string folderPath = scriptFilePath.Remove(scriptFilePath.Length - 1 - (ms.name + ".cs").Length);
        return folderPath;
    }

    #endregion

}
