using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueSystemGroupErrorData
{
    public DialogueSystemErrorData ErrorData { get; set; }

    public List<DialogueSystemGroup> Groups { get; set; }

    public DialogueSystemGroupErrorData()
    {
        ErrorData = new DialogueSystemErrorData();
        Groups = new List<DialogueSystemGroup>();
    }    
}
