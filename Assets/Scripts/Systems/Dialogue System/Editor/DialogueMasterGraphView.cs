using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueMasterGraphView : GraphView
{
    private Dictionary<string, DialogueSystemNodeErrorData> ungroupedNodes;
    private Dictionary<string, DialogueSystemGroupErrorData> groups;
    private Dictionary<Group, Dictionary<string, DialogueSystemNodeErrorData>> groupedNodes;

    public List<int> nodeIDs = new List<int>();
    private int maxAmountOfNodes = int.MaxValue / 2;

    public DialogueMasterGraphView()
    {

        ungroupedNodes = new Dictionary<string, DialogueSystemNodeErrorData>();
        groups = new Dictionary<string, DialogueSystemGroupErrorData>();
        groupedNodes = new Dictionary<Group, Dictionary<string, DialogueSystemNodeErrorData>>();
        AddManipulators();
        AddGridBackground();
        
        OnElementsDeleted();
        OnGroupElementsAdded();
        OnGroupElementsRemoved();
        OnGroupRenamed();

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
        DialogueMasterNode node = null;

        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Add Node", actionEvent => CreateNode(GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
            );

        if(node != null)
            AddElement(node);

        return contextualMenuManipulator;
    }

    private IManipulator CreateGroupContextualMenu()
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
            );


        return contextualMenuManipulator;
    }

    #endregion

    #region Element Creation
    private DialogueSystemGroup CreateGroup(string title, Vector2 localMousePosition)
    {
        DialogueSystemGroup group = new DialogueSystemGroup(title, localMousePosition);

        AddGroup(group);
        AddElement(group);

        foreach(GraphElement selectedElement in selection)
        {
            if (!(selectedElement is DialogueMasterNode))
                continue;

            DialogueMasterNode node = (DialogueMasterNode)selectedElement;
            group.AddElement(node);


        }

        return group;
    }

    private DialogueMasterNode CreateNode(Vector2 position)
    {
        if (nodeIDs.Count >= maxAmountOfNodes)
            return null;

        DialogueMasterNode node = new DialogueMasterNode();

        node.Initialize(this, position);
        node.Draw();
        AddElement(node);
        AddUngroupedNode(node);

        return node;
    }




    #endregion

    #region Callbacks

    private void OnElementsDeleted()
    {
        deleteSelection = (operationName, askUser) =>
        {

            Type groupType = typeof(DialogueSystemGroup);
            Type edgeType = typeof(Edge);

            List<DialogueSystemGroup> groupsToDelete = new List<DialogueSystemGroup>();
            List<DialogueMasterNode> nodesToDelete = new List<DialogueMasterNode>();
            List<Edge> edgesToDelete = new List<Edge>();
            
            foreach(GraphElement element in selection)
            {
                if(element is DialogueMasterNode node)
                    nodesToDelete.Add(node);

                else if (element.GetType() == edgeType)
                {
                    Edge edge = (Edge)element;
                    edgesToDelete.Add(edge);
                }

                else if(element.GetType() == groupType)
                {
                    DialogueSystemGroup group = (DialogueSystemGroup) element;
                    RemoveGroup(group);
                    groupsToDelete.Add(group);
                }
            }

            foreach (DialogueSystemGroup group in groupsToDelete)
            {
                List<DialogueMasterNode> groupNodes = new List<DialogueMasterNode>();

                foreach (GraphElement groupElement in group.containedElements)
                {
                    if (!(groupElement is DialogueMasterNode))
                        continue;

                    DialogueMasterNode groupNode = (DialogueMasterNode) groupElement;
                    groupNodes.Add(groupNode);
                }

                group.RemoveElements(groupNodes);
                RemoveElement(group);
            }

            DeleteElements(edgesToDelete);

            foreach (DialogueMasterNode node in nodesToDelete)
            {
                if (node.Group != null)
                    node.Group.RemoveElement(node);

                nodeIDs.Remove(node.nodeID);
                node.DisconnectAllPorts();
                RemoveUngroupedNode(node);
                RemoveElement(node);
            }
        };
    }

    private void OnGroupElementsAdded()
    {
        elementsAddedToGroup = (group, elements) =>
        {
            foreach (GraphElement element in elements)
            {
                if (!(element is DialogueMasterNode))
                    continue;

                DialogueSystemGroup nodeGroup = (DialogueSystemGroup)group;
                DialogueMasterNode node = (DialogueMasterNode) element;

                RemoveUngroupedNode(node);
                AddGroupedNode(node, nodeGroup);

            }
        };
    }

    private void OnGroupElementsRemoved()
    {
        elementsRemovedFromGroup = (group, elements) =>
        {
            foreach (GraphElement element in elements)
            {
                if (!(element is DialogueMasterNode))
                    continue;

                DialogueMasterNode node = (DialogueMasterNode)element;

                RemoveGroupedNode(node, group);
                AddUngroupedNode(node);

            }
        };
    }

    private void OnGroupRenamed()
    {
        groupTitleChanged = (group, newTitle) =>
        {
            DialogueSystemGroup dialogueGroup = (DialogueSystemGroup)group;
            RemoveGroup(dialogueGroup);

            dialogueGroup.oldTitle = newTitle;

            AddGroup(dialogueGroup);
        };
    }

    #endregion

    #region Repeated Elements
    public void AddUngroupedNode(DialogueMasterNode node)
    {
        string nodeName = node.DialogueName;

        if (!ungroupedNodes.ContainsKey(nodeName))
        {
            DialogueSystemNodeErrorData nodeErrorData = new DialogueSystemNodeErrorData();
            nodeErrorData.Nodes.Add(node);
            ungroupedNodes.Add(nodeName, nodeErrorData);

            return;
        }

        List<DialogueMasterNode> ungroupedNodesList = ungroupedNodes[nodeName].Nodes;

        ungroupedNodesList.Add(node);

        Color errorColor = ungroupedNodes[nodeName].ErrorData.color;

        node.SetErrorColor(errorColor);

        if(ungroupedNodesList.Count == 2)
        {
            ungroupedNodesList[0].SetErrorColor(errorColor);
        }

    }

    public void AddGroupedNode(DialogueMasterNode node, DialogueSystemGroup group)
    {
        string nodeName = node.DialogueName;

        node.Group = group;

        if (!groupedNodes.ContainsKey(group))
            groupedNodes.Add(group, new Dictionary<string, DialogueSystemNodeErrorData>());

        if (!groupedNodes[group].ContainsKey(nodeName))
        {
            DialogueSystemNodeErrorData nodeErrorData = new DialogueSystemNodeErrorData();

            nodeErrorData.Nodes.Add(node);

            groupedNodes[group].Add(nodeName, nodeErrorData);

            return;
        }

        List<DialogueMasterNode> groupedNodesList = groupedNodes[group][nodeName].Nodes;

        groupedNodesList.Add(node);

        Color errorColor = groupedNodes[group][nodeName].ErrorData.color;

        node.SetErrorColor(errorColor);

        if(groupedNodesList.Count == 2)
            groupedNodesList[0].SetErrorColor(errorColor);
      
    }

    public void AddGroup(DialogueSystemGroup group)
    {
        string groupName = group.title;

        if (!groups.ContainsKey(groupName))
        {
            DialogueSystemGroupErrorData groupErrorData = new DialogueSystemGroupErrorData();

            groupErrorData.Groups.Add(group);
            groups.Add(groupName, groupErrorData);

            return;
        }

        List<DialogueSystemGroup> groupsList = groups[groupName].Groups;

        groupsList.Add(group);

        Color errorColor = groups[groupName].ErrorData.color;

        group.SetErrorStyle(errorColor);

        if(groupsList.Count == 2)
        {
            groupsList[0].SetErrorStyle(errorColor);
        }
    }

    public void RemoveUngroupedNode(DialogueMasterNode node)
    {
        string nodeName = node.DialogueName;

        List<DialogueMasterNode> ungroupedNodesList = ungroupedNodes[nodeName].Nodes;

        ungroupedNodesList.Remove(node);

        node.ResetColorToDefault();

        if(ungroupedNodesList.Count == 1)
            ungroupedNodesList[0].ResetColorToDefault();
       
        else if(ungroupedNodesList.Count == 0)
            ungroupedNodes.Remove(nodeName);
        
    }

    public void RemoveGroupedNode(DialogueMasterNode node, Group group)
    {
        string nodeName = node.DialogueName;

        node.Group = null;

        List<DialogueMasterNode> groupedNodesList = groupedNodes[group][nodeName].Nodes;

        groupedNodesList.Remove(node);

        node.ResetColorToDefault();

        if (groupedNodesList.Count == 1)
            groupedNodesList[0].ResetColorToDefault();

        else if (groupedNodesList.Count == 0)
        {
            groupedNodes[group].Remove(nodeName);

            if(groupedNodes[group].Count == 0)
                groupedNodes.Remove(group);
            
        }
    }

    private void RemoveGroup(DialogueSystemGroup group)
    {
        string oldGroupName = group.oldTitle;

        List<DialogueSystemGroup> groupsList = groups[oldGroupName].Groups;

        groupsList.Remove(group);

        group.ResetStyle();

        if (groupsList.Count == 1)
        {
            groupsList[0].ResetStyle();
        }

        else if (groupsList.Count == 0)
        {
            groups.Remove(oldGroupName);
        }


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
