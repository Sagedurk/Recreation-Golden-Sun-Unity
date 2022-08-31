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
    

    private readonly string defaultFileName = "";
    private TextField fileNameTextField;
    private Button generateRuntimeLink;
    private DialogueMasterGraphView graphView;

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
        graphView = new DialogueMasterGraphView(this);
        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void AddToolbar()
    {
        Toolbar toolbar = new Toolbar();
        fileNameTextField = DialogueElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
        {

            
            //fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
        });

        generateRuntimeLink = DialogueElementUtility.CreateButton("Generate Runtime Link", () => 
        {
            GenerateRuntimeLink();
        });

        toolbar.Add(fileNameTextField);
        toolbar.Add(generateRuntimeLink);

        toolbar.AddStyleSheets("Dialogue SageSys/DialogueToolbarStyles.uss");

        rootVisualElement.Add(toolbar);
        
    }

    private void GenerateRuntimeLink()
    {
        if(dialogueInstance == null)
        {
            Debug.LogError("Instance has not been set! Use the 'Open Dialogue Editor' button on the dialogue interactable!");
            return;
        }

        //Flush the dictionary, so that data can be updated without having to write a system around that
        dialogueInstance.dialogueNodeDictionary = new System.Collections.Generic.Dictionary<int, DialogueMaster.NodeInstance>();


        //Start from the node connected to starterNode
        DialogueMasterNode[] nodes = { graphView.starterNode.connectedNode };


        for (int i = 0; i < nodes.Length; i++)
        {
            DialogueMasterNode currentNode = nodes[i];

            if (currentNode == null)
                continue;

            foreach (DialogueMasterNodeChoice choice in currentNode.Choices)
            {
                if (choice.connectedNode == null)
                    continue;

                AppendToArray(ref nodes, choice.connectedNode);
            }

            if(currentNode == graphView.starterNode.connectedNode)
                ConvertNodeToRuntime(currentNode, true);
            else
                ConvertNodeToRuntime(currentNode);

        }

    }

    private void AppendToArray<T>(ref T[] array, T elementToAppend)
    {
        Array.Resize(ref array, array.Length + 1);
        array.SetValue(elementToAppend, array.Length - 1);

    }

    private DialogueMaster.NodeInstance ConvertNodeToRuntime(DialogueMasterNode nodeToConvert, bool isStarterNode = false)
    {
        if (nodeToConvert == null)
            return null;

        DialogueMaster.NodeInstance convertedNode;
        
        //If dictionary already contains this converted node, simply return instead of converting and adding it again
        if (dialogueInstance.dialogueNodeDictionary.TryGetValue(nodeToConvert.nodeID, out convertedNode))
            return convertedNode;

        convertedNode = new DialogueMaster.NodeInstance();
        convertedNode.nodeID = nodeToConvert.nodeID;
        convertedNode.testDialogueString = nodeToConvert.Text;

        foreach (DialogueMasterNodeChoice choice in nodeToConvert.Choices)
        {
            if (choice.connectedNode == null)
                continue;

            convertedNode.connectedNodesIDs.Add(choice.connectedNode.nodeID);
        }

        dialogueInstance.dialogueNodeDictionary.Add(convertedNode.nodeID, convertedNode);
        if (isStarterNode)
            dialogueInstance.starterNode = convertedNode;

        Debug.Log("NODE CONVERTED: " + dialogueInstance.dialogueNodeDictionary.Count);
        return convertedNode;
    }

    private void AddStyles()
    {
        rootVisualElement.AddStyleSheets("Dialogue SageSys/DialogueVariables.uss");

    }

    #endregion


    #region Utility Methods
    public void EnableSaving()
    {
        generateRuntimeLink.SetEnabled(true);
    }

    public void DisableSaving()
    {
        generateRuntimeLink.SetEnabled(false);
    }

    #endregion


}
