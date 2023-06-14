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

            List<DialogueSystemGroup> groupsToDelete = new List<DialogueSystemGroup>();
            List<DialogueMasterNode> nodesToDelete = new List<DialogueMasterNode>();
            List<Edge> edgesToDelete = new List<Edge>();
            
            //Loop through all elements to delete
            foreach(GraphElement element in selection)
            {
                if(element is DialogueMasterNode node)
                    nodesToDelete.Add(node);

                else if (element is Edge edge)
                {
                    edgesToDelete.Add(edge);

                    if(edge.output.node is DialogueMasterStarterNode startNode)
                    {
                        startNode.SetStarterNode(null);
                    }
                }

                else if (element is DialogueSystemGroup group)
                {
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

                nodeIDs.Remove(node.NodeID);
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
                        SetEdgeInputAndOutputColor(edge, Color.green);

                        continue;
                    }

                    //If regular node, link next node to nodeChoice data

                    if (nodeChoice == null)  //Fail safe, if cast was unsuccessful
                        continue;

                    nodeChoice.SetConnectedNode(nextNode);

                    DialogueMasterNode node = nodeChoice.owningPort.node as DialogueMasterNode;

                    if(nodeChoice.requirementType != DialogueMasterNodeChoice.requirementTypes.NONE)
                    {
                        SetEdgeInputAndOutputColor(edge, Color.yellow);
                        Debug.Log(nodeChoice.requirementType);
                    }
                        //else
                        //    SetEdgeInputAndOutputColor(edge, Color.white);

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
                    SetEdgeInputAndOutputColor(edge, Color.white);


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
        if (!ungroupedNodes.ContainsKey(node.NodeID))
            ungroupedNodes.Add(node.NodeID, node);
    }

    public void AddGroupedNode(DialogueMasterNode node, DialogueSystemGroup group)
    {
        string nodeID = node.NodeID.ToString();

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
        ungroupedNodes.Remove(node.NodeID);        
    }

    public void RemoveGroupedNode(DialogueMasterNode node, Group group)
    {
        string nodeID = node.NodeID.ToString();

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
    }

    public void SetEdgeInputAndOutputColor(Edge edge, Color newColor)
    {
        SetEdgeInputColor(edge, newColor);
        SetEdgeOutputColor(edge, newColor);
    }

    public Vector2 GetLocalMousePosition(Vector2 mousePosition)
    {
        Vector2 worldMousePosition = mousePosition;
        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
        return localMousePosition;
    }

    public DialogueMasterNode FindNodeByID(int nodeID)
    {
        ungroupedNodes.TryGetValue(nodeID, out DialogueMasterNode node);
        return node; 
    }

    #endregion

    #region Save & Load

    public void SaveGraphViewData(string sceneName, string instanceName, string path)
    {
        //Check if asset already exists
        string loadPath = path + sceneName + "_" + instanceName + ".asset";
        DialogueEditorSaveData saveData = AssetDatabase.LoadAssetAtPath<DialogueEditorSaveData>(loadPath);

        if (saveData == null)
        {
            saveData = ScriptableObject.CreateInstance(typeof(DialogueEditorSaveData)) as DialogueEditorSaveData;

            ConvertDataSave(ref saveData);
            AssetDatabase.CreateAsset(saveData, path + sceneName + "_" + instanceName + ".asset");
        }
        else
        {
            saveData.nodes.Clear();    //Clear the loaded data, this way, deleted nodes won't be reloaded

            ConvertDataSave(ref saveData);
            EditorUtility.SetDirty(saveData);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Data has been saved!");
    }

    public void LoadGraphViewData(string sceneName, string instanceName, string path)
    {
        string loadPath = path + sceneName + "_" + instanceName + ".asset";
        DialogueEditorSaveData loadData = AssetDatabase.LoadAssetAtPath<DialogueEditorSaveData>(loadPath);

        ConvertDataLoad(loadData);
    }


    private void ConvertDataSave(ref DialogueEditorSaveData saveData)
    {
        DialogueMasterNode[] nodes = new DialogueMasterNode[ungroupedNodes.Count];
        ungroupedNodes.Values.CopyTo(nodes, 0);

        foreach (DialogueMasterNode editorNode in nodes)
        {
            DialogueEditorSerializedNode serializableNode = editorNode.ConvertToSerializedNode();

            saveData.nodes.Add(serializableNode);
        }

        if (starterNode.connectedNode != null)
            saveData.starterNodeID = starterNode.connectedNode.NodeID;
        else
            saveData.starterNodeID = -1;

    }

    private void ConvertDataLoad(DialogueEditorSaveData savedData)
    {
        //Remove any leftover nodes and connections
        foreach (DialogueMasterNode node in ungroupedNodes.Values)
        {
            if (Contains(node))
            {
                List<Edge> edges = new List<Edge>();
                foreach (Edge edge in node.inputPort.connections)
                {
                    edges.Add(edge);
                }

                DeleteElements(edges);
                RemoveElement(node);
            }
        }
        ungroupedNodes.Clear();

        //Load Nodes
        for (int i = 0; i < savedData.nodes.Count; i++)
        {
            DialogueMasterNode node = new DialogueMasterNode();

            node.dialogueTextInitializer = savedData.nodes[i].dialogueText;
            node.Initialize(this, Vector2.zero);
            node.ConvertToEditorNode(savedData.nodes[i]);


            if (!ungroupedNodes.ContainsKey(node.NodeID))
            {
                ungroupedNodes.Add(node.NodeID, node);
                nodeIDs.Add(node.NodeID);
            }
        }

        //Load Choices
        foreach (DialogueEditorSerializedNode savedNode in savedData.nodes)
        {
            ungroupedNodes.TryGetValue(savedNode.nodeID, out DialogueMasterNode editorNode);
            editorNode.Choices.Clear();


            foreach (SerializedChoice savedChoice in savedNode.choices)
            {

                DialogueMasterNodeChoice editorChoice = new DialogueMasterNodeChoice();
                editorChoice.SetRequirementInstance(this);
                editorChoice.ConvertToEditorChoice(savedChoice);
                editorNode.Choices.Add(editorChoice);
            }
        }

        //Draw Loaded Data
        DialogueMasterNode[] nodes = new DialogueMasterNode[ungroupedNodes.Count];
        ungroupedNodes.Values.CopyTo(nodes, 0);

        foreach (DialogueMasterNode node in nodes)
        {
            node.Draw();
            AddElement(node);
        }


        //Redo connections between ports
        foreach (DialogueMasterNode node in nodes)
        {
            if (node.NodeID == savedData.starterNodeID)
                starterNode.ConnectToNode(this, node);

            foreach (DialogueMasterNodeChoice choice in node.Choices)
            {
                choice.UpdateRequirementType(choice.requirementType);

                if (choice.connectedNode == null)
                    continue;

                Edge edge = choice.owningPort.ConnectTo(choice.connectedNode.inputPort);


                if (choice.requirementType != DialogueMasterNodeChoice.requirementTypes.NONE)
                    SetEdgeInputAndOutputColor(edge, Color.yellow);
                    

                AddElement(edge);
            }

        }




    }


    #endregion


}
