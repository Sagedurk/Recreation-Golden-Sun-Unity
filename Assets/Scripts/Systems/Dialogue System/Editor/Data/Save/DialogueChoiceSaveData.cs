using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoiceSaveData
{
    [field: SerializeField] public string Text { get; set; }
    [field: SerializeField] public int NodeID { get; set; }
}
