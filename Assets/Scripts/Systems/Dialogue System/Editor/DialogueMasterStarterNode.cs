using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueMasterStarterNode : Node
{
    private DialogueMasterGraphView graphView;
    DialogueMasterNode connectedNode;

    public void Initialize(DialogueMasterWindow masterWindow)
    {
        Vector2 position = new Vector2(masterWindow.position.size.x * 0.05f, masterWindow.position.size.y * 0.35f);
        SetPosition(new Rect(position, Vector2.zero));
        


        


        //mainContainer.AddToClassList("dialogue-node__main-container");
        //extensionContainer.AddToClassList("dialogue-node__extension-container");

        
    }

    public void Draw()
    {
        titleButtonContainer.contentContainer.RemoveFromHierarchy();

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
