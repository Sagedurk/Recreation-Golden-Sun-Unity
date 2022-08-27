using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMasterNodeChoice
{
    public string choiceText = "New Choice";
    public DialogueMasterNode owningNode;               //The node that owns this NodeChoice instance
    public DialogueMasterNode connectedNode = null;     //The node this NodeChoice instance is connected to
    public bool choiceEnabled = true;

    public DialogueMasterNodeChoice(DialogueMasterNode node)
    {
        owningNode = node;
    }


    public void SetConnectedNode(DialogueMasterNode node)
    {
        connectedNode = node;

        if(connectedNode != null)
        Debug.Log(choiceText + " is connected to node " + connectedNode.nodeID);
        else
        Debug.Log(choiceText + " is not connected");
    }

}
