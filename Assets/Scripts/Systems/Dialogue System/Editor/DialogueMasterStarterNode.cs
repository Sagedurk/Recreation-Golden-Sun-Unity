using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMasterStarterNode : Node
{
    private DialogueMasterGraphView graphView;
    DialogueMasterNode connectedNode;

    public void Initialize(DialogueMasterGraphView dialogueGraphView)
    {
        graphView = dialogueGraphView;

        SetPosition(new Rect(Vector2.zero, Vector2.zero));
        
        


        //mainContainer.AddToClassList("dialogue-node__main-container");
        //extensionContainer.AddToClassList("dialogue-node__extension-container");

        
    }

    public void Draw()
    {

        Port port = CreatePort("Starter Node");
        outputContainer.Add(port);
    }




    private Port CreatePort(string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
    {
        Port port = InstantiatePort(orientation, direction, capacity, typeof(bool));
        port.portName = portName;

        return port;

    }


    public void SetStarterNode(DialogueMasterNode node)
    {
        connectedNode = node;

        Debug.Log("Starter node ID: " + connectedNode.nodeID);
    }

}
