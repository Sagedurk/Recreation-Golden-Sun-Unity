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
        AddStyles();
    }

    #region Overridden Methods
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

    #endregion

    #region Element Addition
    private void AddGridBackground()
    {
        GridBackground gridBackground = new GridBackground();

        gridBackground.StretchToParentSize();
        
        Insert(0, gridBackground);

    }
    private void AddStyles()
    {
        this.AddStyleSheets(
            "Dialogue SageSys/DialogueGraphViewStyles.uss", 
            "Dialogue SageSys/DialogueNodeStyles.uss"
            );

    }
    #endregion

    #region Manipulators
    private void AddManipulators()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(CreateNodeContextualMenu());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(CreateGroupContextualMenu());

    }

    private IManipulator CreateNodeContextualMenu()
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Add Node", actionEvent => AddElement(CreateNode(GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );


        return contextualMenuManipulator;
    }

    private IManipulator CreateGroupContextualMenu()
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );


        return contextualMenuManipulator;
    }

    #endregion

    #region Element Creation
    private Group CreateGroup(string title, Vector2 localMousePosition)
    {
        Group group = new Group()
        {
            title = title
        };

        group.SetPosition(new Rect(localMousePosition, Vector2.zero));

        return group;
    }

    private DialogueMasterNode CreateNode(Vector2 position)
    {
        DialogueMasterNode node = new DialogueMasterNode();

        node.Initialize(position);
        node.Draw();
        AddElement(node);

        return node;
    }
    #endregion

    #region Utilities

    public Vector2 GetLocalMousePosition(Vector2 mousePosition)
    {
        Vector2 worldMousePosition = mousePosition;
        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
        return localMousePosition;
    }

    #endregion

}
