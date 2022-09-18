using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueEditorSaveData : ScriptableObject
{
    [HideInInspector]
    //[SerializeReference]
    public List<DialogueEditorSerializedNode> nodes = new List<DialogueEditorSerializedNode>();

    //[HideInInspector]
    public int starterNodeID = -1;



}



[System.Serializable]
public class DialogueEditorSerializedNode
{
    public int nodeID;
    public List<SerializedChoice> choices = new List<SerializedChoice>();
    public Vector2 position;
    public string dialogueText;
}



[System.Serializable]
public class SerializedChoice
{
    public string choiceName;
    public int connectedNodeID = -1;        //-1 to symbolize null value
    
    //Requirement variables
    public int requirementID;
    public int requirementValueCheck;
    public bool requirementInvertedFlagCheck;
    public string requirementFunctionName;



}
