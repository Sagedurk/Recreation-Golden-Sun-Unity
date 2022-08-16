using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSO : ScriptableObject
{

    [field: SerializeField] public int DialogueID { get; set; }
    [field: SerializeField] [field: TextArea()] public string DialogueText { get; set; }
    [field: SerializeField] public List<DialogueChoiceData> Choices { get; set; }
    [field: SerializeField] public bool IsStartingDialogue { get; set; }


    public void Initialize(int dialogueID, string dialogueText, List<DialogueChoiceData> choices, bool isStartingDialogue)
    {
        DialogueID = dialogueID;
        DialogueText = dialogueText;
        Choices = choices;
        IsStartingDialogue = isStartingDialogue;
    }

}
