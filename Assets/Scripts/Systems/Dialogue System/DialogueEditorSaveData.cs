using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialogueEditorSaveData : ScriptableObject
{
    [HideInInspector]
    public List<DialogueEditorSerializedNode> nodes = new List<DialogueEditorSerializedNode>();

    [HideInInspector]
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



[Serializable]
public class DialogueMasterElements
{
    public Sprite dialogueBackground = null;
    public Vector4 fontMargins = Vector4.zero;

    public Font font = null;
    public int fontSize = 0;
    public Color fontColor = Color.white;

    public bool isShadowed = false;
    public float fontShadowMag = 0;
    public Color fontShadowColor = Color.white;
    public Vector2 fontShadowDir = Vector2.zero;


    public static DialogueMasterElements Instance;

    public static DialogueMasterElements TryGetInstance()
    {
        if (Instance != null)
            return Instance;

        DialogueMasterSaveData.Load();
        return Instance;
    }


}