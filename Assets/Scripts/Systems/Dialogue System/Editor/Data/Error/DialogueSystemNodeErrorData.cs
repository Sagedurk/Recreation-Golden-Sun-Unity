using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueSystemNodeErrorData
{
    public DialogueSystemErrorData ErrorData { get; set; }
    public List<DialogueMasterNode> Nodes { get; set; }

    public DialogueSystemNodeErrorData()
    {
        ErrorData = new DialogueSystemErrorData();
        Nodes = new List<DialogueMasterNode>();
    }
}
