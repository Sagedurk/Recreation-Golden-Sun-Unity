using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    public string interactableTest;
    public List<DialogueMaster.DialogueInstance> listOfDialogueBoxes = new List<DialogueMaster.DialogueInstance>();
    public Dictionary<int, DialogueMaster.NodeInstance> dialogueNodeDictionary;
    public DialogueMaster.NodeInstance starterNode = null;


    public ChestMaster.ChestInstance chestInstance = new ChestMaster.ChestInstance();
    public DialogueEvent eventDialogue;
    public DialogueNodeEvent nodeEventDialogue;
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
        nodeEventDialogue.Invoke(dialogueNodeDictionary, starterNode);
        
    }


    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.DIALOGUE:
                //eventDialogue.Invoke(listOfDialogueBoxes);
                nodeEventDialogue.Invoke(dialogueNodeDictionary, starterNode);
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









}
[System.Serializable]
 public class DialogueEvent : UnityEvent< List<DialogueMaster.DialogueInstance> > { }

[System.Serializable]
 public class DialogueNodeEvent : UnityEvent<Dictionary<int, DialogueMaster.NodeInstance>, DialogueMaster.NodeInstance> { }

[System.Serializable]
 public class ChestEvent : UnityEvent<ChestMaster.ChestInstance> { }

[System.Serializable]
 public class ShopEvent : UnityEvent { }

[System.Serializable]
 public class DjinnEvent : UnityEvent { }
