using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNodeSaveData
{
    [field: SerializeField] public int ID { get; set; }
    [field: SerializeField] public string Text { get; set; }
    [field: SerializeField] public List<DialogueChoiceSaveData> Choices { get; set; }
    [field: SerializeField] public string GroupID { get; set; }
    [field: SerializeField] public Vector2 Position { get; set; }


}
