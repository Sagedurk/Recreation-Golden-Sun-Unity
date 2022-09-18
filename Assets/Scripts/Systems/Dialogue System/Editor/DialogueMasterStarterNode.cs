using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueMasterStarterNode : Node
{
    //private DialogueMasterGraphView graphView;
    public DialogueMasterNode connectedNode = null;
    private Port port;

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

        port = CreatePort("Starter Node");
        outputContainer.Add(port);
    }

    public void ConnectToNode(DialogueMasterGraphView graphView ,DialogueMasterNode node)
    {
        Edge edge = port.ConnectTo(node.inputPort);
        graphView.SetEdgeInputAndOutputColor(edge, Color.green);

        graphView.AddElement(edge);

        SetStarterNode(node);
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
    }

}
