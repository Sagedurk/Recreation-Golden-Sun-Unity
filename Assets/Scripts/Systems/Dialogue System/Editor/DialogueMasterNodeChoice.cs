using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class DialogueMasterNodeChoice
{
    public string choiceName = "New Choice";    
    public DialogueMasterNode connectedNode = null;     //The node to proceed to after choosing this choice
    public Port owningPort;                             //The port that owns this NodeChoice instance
    public bool choiceEnabled = true;
    public DialogueMasterGraphView graphView;

    //Requirement variables
    public ToolbarMenu requirementTypeToolbar;          //Toolbar menu for requirement type
    public ToolbarMenu requirementToolbar = null;       //Toolbar menu for choosing requirement method
    TextField valueField;
    Toggle checkBox;
    public DialogueMasterChoiceRequirements requirementInstance;
    public string requirementFunctionName; 
    public int requirementValueCheck;
    public bool requirementInvertedFlagCheck;



    private string[] requirementSelections = { "None", "Check Flag", "Check Value" };
    public enum requirementTypes
    {
        NONE,
        FLAG,
        VALUE
    }
    public requirementTypes requirementType = requirementTypes.NONE;

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

    private void SetGraphView(DialogueMasterGraphView masterGraph)
    {
        graphView = masterGraph;
    }

    public void SetRequirementInstance(DialogueMasterGraphView masterGraph)
    {
        SetGraphView(masterGraph);
        requirementInstance = graphView.requirementInstance;
    }

    public void SetConnectedNode(DialogueMasterNode node)
    {
        connectedNode = node;
        
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
            requirementFunctionName = functionName;
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
                SetEdgeColor(Color.white);
                requirementType = requirementTypes.NONE;
                RemoveRequirementToolbarToPort();
                break;

            case "Check Flag":
                SetEdgeColor(Color.yellow);
                requirementType = requirementTypes.FLAG;
                RemoveRequirementToolbarToPort();
                requirementToolbar = CreateRequirementToolbar(requirementInstance.flagFunctionNames);
                AddFlagRequirementToolbarToPort();
                break;

            case "Check Value":
                SetEdgeColor(Color.yellow);
                requirementType = requirementTypes.VALUE;
                RemoveRequirementToolbarToPort();
                requirementToolbar = CreateRequirementToolbar(requirementInstance.valueFunctionNames);
                AddValueRequirementToolbarToPort();
                break;

            default:
                break;
        }
    }

    public void UpdateRequirementType(requirementTypes types)
    {
        string requirementType = "";
        switch (types)
        {
            case requirementTypes.NONE:
                requirementType = "None";
                break;
            case requirementTypes.FLAG:
                requirementType = "Check Flag";
                
                break;
            case requirementTypes.VALUE:
                requirementType = "Check Value";
                break;
            default:
                break;
        }

        SetRequirementType(requirementType);
        if (requirementTypeToolbar != null)
            requirementTypeToolbar.text = "Requirement: " + requirementType;

        if (requirementToolbar != null)
        {
            requirementToolbar.text = requirementFunctionName;

            if (requirementToolbar.text == "")
                requirementToolbar.text = "Choose Requirement";
        }
    }


    public void ResetToolbarDropdown()
    {
        UpdateRequirementType(requirementTypes.NONE);
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


    private void SetEdgeColor(Color newColor)
    {
        if (owningPort == null)
            return;

        foreach (Edge edge in owningPort.connections)
        {
            graphView.SetEdgeInputAndOutputColor(edge, newColor);
        }
    }
    #endregion

    #region Port Restructuring

    private void AddFlagRequirementToolbarToPort()
    {
        RemoveValueField();
        List<VisualElement> elementsToReAdd = DialogueElementUtility.GetChildElements(owningPort);

        //Add any new elements, rightmost is added first
        AddCheckBox();

        owningPort.Add(requirementToolbar);

        DialogueElementUtility.ReAddElements(ref owningPort, elementsToReAdd, 2);
    }
    private void AddValueRequirementToolbarToPort()
    {
        RemoveCheckBox();
        List<VisualElement> elementsToReAdd = DialogueElementUtility.GetChildElements(owningPort);

        //Add any new elements, rightmost is added first
        AddValueField();

        owningPort.Add(requirementToolbar);


        DialogueElementUtility.ReAddElements(ref owningPort, elementsToReAdd, 2);
    }

    private void RemoveRequirementToolbarToPort()
    {
        RemovePortElement(requirementToolbar);
        RemovePortElement(checkBox);
        RemovePortElement(valueField);
    }

    #region Restructuring Utilities

    private void RemovePortElement(VisualElement element)
    {
        if (element == null)
            return;

        if (owningPort.Contains(element))
            owningPort.Remove(element);
    }

    
    private void AddValueField()
    {
        if (owningPort.Contains(valueField))
            return;
        
        valueField = DialogueElementUtility.CreateNumField(requirementValueCheck, "Value:  ", callback =>
        {
            DialogueElementUtility.RemoveCharactersNaN(callback, out requirementValueCheck);
        });
        owningPort.Add(valueField);
    }

    private void AddCheckBox()
    {
        if (owningPort.Contains(checkBox))
            return;

        checkBox = DialogueElementUtility.CreateToggle(requirementInvertedFlagCheck, "Invert:  ", callback => 
        {
            requirementInvertedFlagCheck = callback.newValue;
        });

        checkBox.labelElement.style.minWidth = 0;
        //Fix label spacing with USS, I suppose
        
        owningPort.Add(checkBox);
    }

    private void RemoveValueField()
    {
        if (owningPort.Contains(valueField))
            owningPort.Remove(valueField);

    }
    private void RemoveCheckBox()
    {
        if (owningPort.Contains(checkBox))
            owningPort.Remove(checkBox);
    }

    

    #endregion

    #endregion




    public SerializedChoice ConvertToSerializedChoice()
    {
        SerializedChoice serializedChoice = new SerializedChoice();

        serializedChoice.choiceName = choiceName;
        serializedChoice.requirementID = (int)requirementType;

        if (connectedNode != null)
            serializedChoice.connectedNodeID = connectedNode.NodeID;

        serializedChoice.requirementValueCheck = requirementValueCheck;
        serializedChoice.requirementInvertedFlagCheck = requirementInvertedFlagCheck;
        serializedChoice.requirementFunctionName = requirementFunctionName;

        return serializedChoice;
    }

    public void ConvertToEditorChoice(SerializedChoice serializedChoice)
    {
        choiceName = serializedChoice.choiceName;

        if (serializedChoice.connectedNodeID >= 0)
            connectedNode = graphView.FindNodeByID(serializedChoice.connectedNodeID);

        requirementType = (requirementTypes)serializedChoice.requirementID;
        requirementValueCheck = serializedChoice.requirementValueCheck;
        requirementInvertedFlagCheck = serializedChoice.requirementInvertedFlagCheck;
        requirementFunctionName = serializedChoice.requirementFunctionName;

    }





}

