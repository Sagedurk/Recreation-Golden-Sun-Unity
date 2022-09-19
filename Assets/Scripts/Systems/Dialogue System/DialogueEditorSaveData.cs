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



[Serializable]
public class DialogueEditorSerializedNode
{
    public int nodeID;
    public List<SerializedChoice> choices = new List<SerializedChoice>();
    public Vector2 position;
    public string dialogueText;
    public PortraitData portrait = new PortraitData();
    public Rect dialogueBox = new Rect();

}



[Serializable]
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

[Serializable]
public class PortraitData
{
    public Sprite sprite;
    public Vector2 position;
}

