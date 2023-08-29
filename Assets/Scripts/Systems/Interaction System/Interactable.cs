using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    public string interactableTest;
    public Dictionary<int, DialogueEditorSerializedNode> dialogueNodeDictionary = new Dictionary<int, DialogueEditorSerializedNode>();
    public int starterNodeID;


    public ChestMaster.ChestInstance chestInstance = new ChestMaster.ChestInstance();
    public DialogueEvent eventDialogue;
    public DialogueEvent nodeEventDialogue;
    public ChestEvent eventChest;
    public ShopEvent eventShop;
    public DjinnEvent eventDjinn;
    public InteractionType interactionType;


    public enum InteractionType{
        DIALOGUE,
        CHEST,
        SHOP,
        DJINN,
        PSYNERGY_STONE

    }

    private void Start()
    {
        //DEBUG PURPOSES!
        //nodeEventDialogue.Invoke(dialogueNodeDictionary, starterNode);
        LoadDialogueData();
        
    }


    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.DIALOGUE:
                //eventDialogue.Invoke(listOfDialogueBoxes);
                nodeEventDialogue.Invoke(dialogueNodeDictionary, starterNodeID);
                break;

            case InteractionType.CHEST:
                eventChest.Invoke(chestInstance);
                break;

            case InteractionType.SHOP:

                break;

            case InteractionType.DJINN:

                break;

            default:

                break;
        }
    }


    public void LoadDialogueData()
    {
        //Find better way to fucking find the damn file path
        string path = "Systems/Dialogue System/Data/";
        //string path = "Assets/Scripts/Systems/Dialogue System/Editor/SaveData/";

        string assetName = gameObject.scene.name + "_" + gameObject.name;// + ".asset";


        DialogueEditorSaveData saveData = Resources.Load<DialogueEditorSaveData>(path + assetName);
        //DialogueEditorSaveData saveData = AssetDatabase.LoadAssetAtPath<DialogueEditorSaveData>(path + assetName);

        if (saveData == null)
            return;


        foreach (DialogueEditorSerializedNode node in saveData.nodes)
        {
            dialogueNodeDictionary.Add(node.nodeID, node);
        }

        starterNodeID = saveData.starterNodeID;
    }






}


[System.Serializable]
 public class DialogueEvent : UnityEvent<Dictionary<int, DialogueEditorSerializedNode>, int> { }

[System.Serializable]
 public class ChestEvent : UnityEvent<ChestMaster.ChestInstance> { }

[System.Serializable]
 public class ShopEvent : UnityEvent { }

[System.Serializable]
 public class DjinnEvent : UnityEvent { }
