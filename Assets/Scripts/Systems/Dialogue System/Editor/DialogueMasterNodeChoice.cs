using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueMasterNodeChoice
{
    public string choiceName = "New Choice";    
    public DialogueMasterNode connectedNode;    //The node to proceed to after choosing this choice
    public Port owningPort;                     //The port that owns this NodeChoice instance
    public ToolbarMenu requirementTypeToolbar;      //Toolbar menu for requirement type
    public ToolbarMenu requirementToolbar = null;             //Toolbar menu for choosing requirement method
    public bool choiceEnabled = true;
    public DialogueMasterChoiceRequirements requirementInstance;
    public int requirementValueCheck;
    public bool requirementInvertedFlagCheck;

    private string[] requirementSelections = { "None", "Check Flag", "Check Value" };
    enum requirementTypes
    {
        NONE,
        FLAG,
        VALUE
    }
    requirementTypes requirementType = requirementTypes.NONE;

    #region Node Connections
    public DialogueMasterNodeChoice()
    {
        //requirementToolbar = CreateRequirementToolbar(new List<string>());
        //Debug.Log(requirementToolbar);
        ResetToolbarDropdown();
    }

    public void SetOwningPort(Port port)
    {
        owningPort = port;
        
    }

    public void SetRequirementInstance(DialogueMasterGraphView graphView)
    {
        requirementInstance = graphView.requirementInstance;
    }

    public void SetConnectedNode(DialogueMasterNode node)
    {
        connectedNode = node;

        if(connectedNode != null)
        Debug.Log(choiceName + " is connected to node " + connectedNode.nodeID);
        else
        Debug.Log(choiceName + " is not connected");
    }
    #endregion

    #region Requirements

    private ToolbarMenu CreateRequirementTypeToolbar()
    {
        ToolbarMenu toolbar = new ToolbarMenu();
        string toolbarPrefix = "Requirement: ";
        toolbar.text = toolbarPrefix + "None";


        foreach (string stringText in requirementSelections)
        {
            AddRequirementType(toolbar, stringText, toolbarPrefix);
        }
        return toolbar;
    }

    private ToolbarMenu CreateRequirementToolbar(List<string> functionNamesList)
    {
        ToolbarMenu toolbar = new ToolbarMenu();
        string toolbarPrefix = "";
        toolbar.text = toolbarPrefix + "Choose Requirement";
        string[] functionNames = functionNamesList.ToArray();

        foreach (string functionName in functionNames)
        {
            AddRequirement(toolbar, functionName);
        }
        return toolbar;
    }

    private void AddRequirementType(ToolbarMenu toolbar, string name, string toolbarPrefix = "")
    {
        toolbar.menu.AppendAction(name, callback => {
            toolbar.text = toolbarPrefix + name;
            SetRequirementType(name);
        });
    }
    public void AddRequirement(ToolbarMenu toolbar, string functionName)
    {
        toolbar.menu.AppendAction(functionName, callback => {
            toolbar.text = functionName;
            SetRequirement(functionName);
        });
    }

    private void SetRequirement(string functionName)
    {
        Debug.Log(functionName);
    }

    private void SetRequirementType(string name)
    {
        switch (name)
        {
            case "None":
                requirementType = requirementTypes.NONE;
                RemoveRequirementToolbarToPort();
                break;

            case "Check Flag":
                requirementType = requirementTypes.FLAG;
                RemoveRequirementToolbarToPort();
                requirementToolbar = CreateRequirementToolbar(requirementInstance.flagFunctionNames);
                AddRequirementToolbarToPort();
                break;

            case "Check Value":
                requirementType = requirementTypes.VALUE;
                RemoveRequirementToolbarToPort();
                requirementToolbar = CreateRequirementToolbar(requirementInstance.valueFunctionNames);
                AddRequirementToolbarToPort();
                break;

            default:
                break;
        }
    }
    public void ResetToolbarDropdown()
    {
        SetRequirementType("None");
        requirementTypeToolbar = CreateRequirementTypeToolbar();
    }

    public void SetRequirementInstance()
    {
        switch (requirementType)
        {
            case requirementTypes.NONE:
                requirementInstance.flags.choiceInstance = null;
                requirementInstance.values.choiceInstance = null;
                break;

            case requirementTypes.FLAG:
                requirementInstance.flags.choiceInstance = this;
                break;

            case requirementTypes.VALUE:
                requirementInstance.values.choiceInstance = this;
                break;

            default:
                break;
        }
    }

    #endregion

    #region Port Restructuring

    private void AddRequirementToolbarToPort()
    {
        if (owningPort.Contains(requirementToolbar))
            return;

        List<VisualElement> elementsToReAdd = new List<VisualElement>();

        foreach (VisualElement element in owningPort.Children())
        {
            elementsToReAdd.Add(element);
        }

        owningPort.Add(requirementToolbar);

        for (int i = 2; i < elementsToReAdd.Count; i++)
        {
            owningPort.Remove(elementsToReAdd[i]);
            owningPort.Add(elementsToReAdd[i]);
        }

        elementsToReAdd.Clear();
    }

    private void RemoveRequirementToolbarToPort()
    {
        if (requirementToolbar == null)
            return;

        if (owningPort.Contains(requirementToolbar))
            owningPort.Remove(requirementToolbar);

    }

    #endregion



}
