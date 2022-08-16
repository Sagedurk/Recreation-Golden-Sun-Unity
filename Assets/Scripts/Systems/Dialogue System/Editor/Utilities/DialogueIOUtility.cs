using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DialogueIOUtility
{
    private static string graphFileName;
    private static string containerFolderPath;
    private static DialogueMasterGraphView graphView;

    private static List<DialogueSystemGroup> groups;
    private static List<DialogueMasterNode> nodes;

    public static void Initialize(DialogueMasterGraphView masterGraphview, string graphName)
    {
        graphView = masterGraphview;
        graphFileName = graphName;
        containerFolderPath = "Assets/Scripts/Systems/Dialogue System/SaveData/Dialogues/" + graphFileName;

        groups = new List<DialogueSystemGroup>();
        nodes = new List<DialogueMasterNode>();
    }


    #region Save Methods
    public static void Save()
    {
        CreateStaticFolders();

        GetElementsFromGraphView();

    }

    #endregion

    #region Creation Methods
    private static void CreateStaticFolders()
    {
        CreateFolder("Assets/Scripts/Systems/Dialogue System", "SaveData");
        CreateFolder("Assets/Scripts/Systems/Dialogue System/SaveData", "Editor");
        CreateFolder("Assets/Scripts/Systems/Dialogue System/SaveData/Editor", "Graphs");
        CreateFolder("Assets/Scripts/Systems/Dialogue System/SaveData", "Dialogues");

        CreateFolder("Assets/Scripts/Systems/Dialogue System/SaveData/Dialogues", graphFileName);
        CreateFolder(containerFolderPath, "Global");
        CreateFolder(containerFolderPath, "Groups");
        CreateFolder(containerFolderPath + "/Global", "Dialogues");

    }
    #endregion

    #region 'Get' Methods
    private static void GetElementsFromGraphView()
    {
        Type groupType = typeof(DialogueSystemGroup);

        graphView.graphElements.ForEach(graphElement => 
        {
            if(graphElement is DialogueMasterNode node)
            {
                nodes.Add(node);
            }

            else if(graphElement.GetType() == groupType)
            {
                DialogueSystemGroup group = (DialogueSystemGroup)graphElement;

                groups.Add(group);
            }

        });
    }
    #endregion

    #region Utility Methods
    private static void CreateFolder(string path, string folderName)
    {
       

        if (AssetDatabase.IsValidFolder(path + "/" + folderName))
            return;

        AssetDatabase.CreateFolder(path, folderName);
    }
    #endregion




}
