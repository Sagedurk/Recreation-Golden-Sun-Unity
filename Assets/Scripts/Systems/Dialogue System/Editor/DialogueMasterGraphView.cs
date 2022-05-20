using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueMasterGraphView : GraphView
{
    public DialogueMasterGraphView()
    {
        AddManipulators();
        AddGridBackground();
        //CreateNode();
        AddStyles();


    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();

        ports.ForEach(port => 
        { 
            if(startPort == port || startPort.node == port.node || startPort.direction == port.direction)
                return;

            compatiblePorts.Add(port);
                
        });


        return compatiblePorts;
    }

    private void AddGridBackground()
    {
        GridBackground gridBackground = new GridBackground();

        gridBackground.StretchToParentSize();
        
        Insert(0, gridBackground);

    }
    private void AddStyles()
    {
        StyleSheet graphStyleSheet = (StyleSheet)EditorGUIUtility.Load("Dialogue SageSys/DialogueGraphViewStyles.uss");
        StyleSheet nodeStyleSheet = (StyleSheet)EditorGUIUtility.Load("Dialogue SageSys/DialogueNodeStyles.uss");

        styleSheets.Add(graphStyleSheet);
        styleSheets.Add(nodeStyleSheet);
    }

    private void AddManipulators()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(CreateNodeContextualMenu());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    
    }

    private IManipulator CreateNodeContextualMenu()
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator( 
            menuEvent => menuEvent.menu.AppendAction("Add Node", actionEvent => AddElement(CreateNode(actionEvent.eventInfo.localMousePosition))) 
            );


        return contextualMenuManipulator;
    }

    private DialogueMasterNode CreateNode(Vector2 position)
    {
        DialogueMasterNode node = new DialogueMasterNode();

        node.Initialize(position);
        node.Draw();
        AddElement(node);

        return node;
    }
}
