using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    public string interactableTest;
    public List<DialogueMaster.DialogueInstance> listOfDialogueBoxes = new List<DialogueMaster.DialogueInstance>();
    public DialogueEvent eventDialogue;
    public ChestEvent eventChest;
    public ShopEvent eventShop;
    public DjinnEvent eventDjinn;
    public InteractionType interactionType;


    public enum InteractionType{
        DIALOGUE,
        CHEST,
        SHOP,
        DJINN,


    }


    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.DIALOGUE:
                eventDialogue.Invoke(listOfDialogueBoxes);
                break;

            case InteractionType.CHEST:

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
 public class ChestEvent : UnityEvent { }

[System.Serializable]
 public class ShopEvent : UnityEvent { }

[System.Serializable]
 public class DjinnEvent : UnityEvent { }
