using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoiceData
{
    [field: SerializeField] public string Text { get; set; }
    [field: SerializeField] public DialogueSO NextDialogue { get; set; }


}
