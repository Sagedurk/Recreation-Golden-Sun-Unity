using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueMasterGraphView : GraphView
{
    private DialogueMasterWindow dialogueWindow;

    private Dictionary<int, DialogueMasterNode> ungroupedNodes = new Dictionary<int, DialogueMasterNode>();
    private Dictionary<string, DialogueSystemGroupErrorData> groups;
    private Dictionary<Group, Dictionary<string, DialogueSystemNodeErrorData>> groupedNodes;
    public DialogueMasterChoiceRequirements requirementInstance;
    public DialogueMasterStarterNode starterNode;

    public List<int> nodeIDs = new List<int>();
    private int maxAmountOfNodes = int.MaxValue / 2;


    private int repeatedNamesAmount;
    public int amountOfRepeatedNames
    {
        get
        {
            return repeatedNamesAmount;
        }

        set
        {
            repeatedNamesAmount = value;

            if (repeatedNamesAmount == 0)
                dialogueWindow.EnableSaving();

            else if (repeatedNamesAmount == 1)
                dialogueWindow.DisableSaving();

        }
    }


    public DialogueMasterGraphView(DialogueMasterWindow window)
    {
        dialogueWindow = window;
        ungroupedNodes = new Dictionary<int, DialogueMasterNode>();
        groups = new Dictionary<string, DialogueSystemGroupErrorData>();
        groupedNodes = new Dictionary<Group, Dictionary<string, DialogueSystemNodeErrorData>>();
        AddManipulators();
        AddGridBackground();

        starterNode = CreateStarterNode();

        OnElementsDeleted();
        OnGroupElementsAdded();
        OnGroupElementsRemoved();
        OnGroupRenamed();
        OnGraphViewChanged();

        AddStyles();

        requirementInstance = new DialogueMasterChoiceRequirements();
        requirementInstance.Instantiate();
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

    public DialogueMasterStarterNode CreateStarterNode()
    {
        DialogueMasterStarterNode node = new DialogueMasterStarterNode();
        node.Initialize(dialogueWindow);
        node.Draw();
        AddElement(node);

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

            dialogueGroup.title = newTitle.RemoveWhitespaces().RemoveSpecialCharacters();

            RemoveGroup(dialogueGroup);

            dialogueGroup.OldTitle = dialogueGroup.title;
            AddGroup(dialogueGroup);
        };
    }

    private void OnGraphViewChanged()
    {
        graphViewChanged = (changes) =>
        {
            if(changes.edgesToCreate != null)   //When edge(s) are created
            {
                foreach (Edge edge in changes.edgesToCreate)
                {
                    //Cast attempts
                    DialogueMasterStarterNode starterNode = edge.output.node as DialogueMasterStarterNode;
                    DialogueMasterNode  nextNode = edge.input.node as DialogueMasterNode;
                    DialogueMasterNodeChoice nodeChoice = edge.output.userData as DialogueMasterNodeChoice;

                    if (starterNode != null)    //If starter node
                    {
                        starterNode.SetStarterNode(nextNode);
                        SetEdgeOutputColor(edge, Color.green);
                        SetEdgeInputColor(edge, Color.green);

                        continue;
                    }

                    //If regular node, link next node to nodeChoice data

                    if (nodeChoice == null)  //Fail safe, if cast was unsuccessful
                        continue;

                    nodeChoice.SetConnectedNode(nextNode);

                    DialogueMasterNode node = nodeChoice.owningPort.node as DialogueMasterNode;

                    if (node.Choices.Count > 1) //If the current node has choices
                    {
                        SetEdgeInputColor(edge, Color.cyan);
                        SetEdgeOutputColor(edge, Color.cyan);
                    }
                }
            }

            if(changes.elementsToRemove != null)
            {
                foreach (GraphElement element in changes.elementsToRemove)
                {
                    Edge edge = element as Edge;
                    if (edge == null)   //If element is not edge, check next element
                        continue;

                    //If element is edge
                    SetEdgeInputColor(edge, Color.white);
                    SetEdgeOutputColor(edge, Color.white);


                    //check if output port contains NodeChoice data, if it does, nullify connected node
                    DialogueMasterNodeChoice nodeChoice = edge.output.userData as DialogueMasterNodeChoice;
                    if (nodeChoice != null)
                        nodeChoice.SetConnectedNode(null);
                }
            }

            return changes;
        };
    }



    #endregion

    #region Repeated Elements
    public void AddUngroupedNode(DialogueMasterNode node)
    {
        if (!ungroupedNodes.ContainsKey(node.nodeID))
            ungroupedNodes.Add(node.nodeID, node);

    }

    public void AddGroupedNode(DialogueMasterNode node, DialogueSystemGroup group)
    {
        string nodeID = node.nodeID.ToString();

        node.Group = group;

        if (!groupedNodes.ContainsKey(group))
            groupedNodes.Add(group, new Dictionary<string, DialogueSystemNodeErrorData>());

        if (!groupedNodes[group].ContainsKey(nodeID))
        {
            DialogueSystemNodeErrorData nodeErrorData = new DialogueSystemNodeErrorData();

            nodeErrorData.Nodes.Add(node);

            groupedNodes[group].Add(nodeID, nodeErrorData);

            return;
        }

        List<DialogueMasterNode> groupedNodesList = groupedNodes[group][nodeID].Nodes;

        groupedNodesList.Add(node);

        Color errorColor = groupedNodes[group][nodeID].ErrorData.color;

        node.SetErrorColor(errorColor);

        if(groupedNodesList.Count == 2)
        {
            amountOfRepeatedNames++;
            groupedNodesList[0].SetErrorColor(errorColor);
        }
      
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
            amountOfRepeatedNames++;
            groupsList[0].SetErrorStyle(errorColor);
        }
    }

    public void RemoveUngroupedNode(DialogueMasterNode node)
    {
        ungroupedNodes.Remove(node.nodeID);        
    }

    public void RemoveGroupedNode(DialogueMasterNode node, Group group)
    {
        string nodeID = node.nodeID.ToString();

        node.Group = null;

        List<DialogueMasterNode> groupedNodesList = groupedNodes[group][nodeID].Nodes;

        groupedNodesList.Remove(node);

        node.ResetColorToDefault();

        if (groupedNodesList.Count == 1)
        {
            amountOfRepeatedNames--;
            groupedNodesList[0].ResetColorToDefault();
        }

        else if (groupedNodesList.Count == 0)
        {
            groupedNodes[group].Remove(nodeID);

            if(groupedNodes[group].Count == 0)
                groupedNodes.Remove(group);
            
        }
    }

    private void RemoveGroup(DialogueSystemGroup group)
    {
        string oldGroupName = group.OldTitle;

        List<DialogueSystemGroup> groupsList = groups[oldGroupName].Groups;

        groupsList.Remove(group);

        group.ResetStyle();

        if (groupsList.Count == 1)
        {
            amountOfRepeatedNames--;
            groupsList[0].ResetStyle();
        }

        else if (groupsList.Count == 0)
        {
            groups.Remove(oldGroupName);
        }


    }

    #endregion

    #region Utilities

    private void SetEdgeOutputColor(Edge edge, Color newColor)
    {
        edge.output.portColor = newColor;
        edge.output.elementTypeColor = newColor;
    } 
    private void SetEdgeInputColor(Edge edge, Color newColor)
    {
        edge.input.portColor = newColor;
        edge.input.elementTypeColor = newColor;
        edge.input.node.RefreshPorts();
    }

    public Vector2 GetLocalMousePosition(Vector2 mousePosition)
    {
        Vector2 worldMousePosition = mousePosition;
        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
        return localMousePosition;
    }

    #endregion

    #region Save & Load

    public void SaveGraphViewData(string sceneName, string instanceName, string path)
    {
        //Check if asset already exists


        DialogueEditorSaveData saveData = ScriptableObject.CreateInstance(typeof(DialogueEditorSaveData)) as DialogueEditorSaveData;

        ConvertDataSave(ref saveData);

        AssetDatabase.CreateAsset(saveData, path + sceneName + "_" + instanceName + ".asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraphViewData(string sceneName, string instanceName, string path)
    {
        //Check if asset already exists

        string loadPath = path + sceneName + "_" + instanceName + ".asset";
        DialogueEditorSaveData loadData = AssetDatabase.LoadAssetAtPath<DialogueEditorSaveData>(loadPath);
        Debug.Log("loadData state: " + loadData);

        ConvertDataLoad(loadData);
    }


    private void ConvertDataSave(ref DialogueEditorSaveData saveData)
    {
        DialogueMasterNode[] nodes = new DialogueMasterNode[ungroupedNodes.Count];
        ungroupedNodes.Values.CopyTo(nodes, 0);

        foreach (DialogueMasterNode node in nodes)
        {
            saveData.ungroupedNodes.Add(node);
        }

    }
    private void ConvertDataLoad(DialogueEditorSaveData savedData)
    {
        for (int i = 0; i < savedData.ungroupedNodes.Count; i++)
        {
            ungroupedNodes.Add(savedData.ungroupedNodes[i].nodeID, savedData.ungroupedNodes[i]);
        }

        DialogueMasterNode[] nodes = new DialogueMasterNode[ungroupedNodes.Count];
        ungroupedNodes.Values.CopyTo(nodes, 0);

        foreach (DialogueMasterNode node in nodes)
        {
            //node.Draw();
            AddElement(node);
        }


        //saveData.SaveNodePositions();
    }


    #endregion

}
