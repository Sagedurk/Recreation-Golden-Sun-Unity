using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraphSaveDataSO : ScriptableObject
{
    [field: SerializeField]public string FileName { get; set; }
    [field: SerializeField]public List<DialogueGroupSaveData> Groups { get; set; }
    [field: SerializeField]public List<DialogueNodeSaveData> Nodes { get; set; }
    [field: SerializeField]public List<string> OldGroupNames { get; set; }


    public void Initialize(string fileName)
    {
        FileName = fileName;
        Groups = new List<DialogueGroupSaveData>();
        Nodes = new List<DialogueNodeSaveData>();
    }
}
