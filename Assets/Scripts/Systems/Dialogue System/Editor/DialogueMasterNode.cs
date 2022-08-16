using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;


public class DialogueMasterNode : Node
{
    public string DialogueName { get; set; }
    public List<DialogueChoiceSaveData> Choices { get; set; }
    public string Text { get; set; }
    public DialogueSystemGroup Group { get; set; }
    public int nodeID { get; set; }

    private DialogueMasterGraphView graphView;


    public void Initialize(DialogueMasterGraphView dialogueGraphView, Vector2 position)
    {
        graphView = dialogueGraphView;

        SetNodeID();
        DialogueName = nodeID.ToString();
        

        Choices = new List<DialogueChoiceSaveData>();
        Text = "Dialogue text.";

        SetPosition(new Rect(position, Vector2.zero));

        mainContainer.AddToClassList("dialogue-node__main-container");
        extensionContainer.AddToClassList("dialogue-node__extension-container");

        DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
        {
            Text = "New Choice"
        };

        Choices.Add(choiceData);
    }

    #region Overrided Methods
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
        evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());

        base.BuildContextualMenu(evt);
    }
    #endregion

    public void Draw()
    {
        /* MAIN CONTAINER */
        Button addChoiceButton = DialogueElementUtility.CreateButton("Add Choice", () =>
        {
            DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
            {
                Text = "New Choice"
            };

            Choices.Add(choiceData);

            Port choicePort = CreateChoicePort(choiceData);

            outputContainer.Add(choicePort);
        });

        addChoiceButton.AddToClassList("dialogue-node__button");

        mainContainer.Insert(1, addChoiceButton);

        /* TITLE CONTAINER */
        TextField dialogueNameTextField = DialogueElementUtility.CreateTextField(DialogueName, null, callback => 
        {
            TextField target = (TextField)callback.target;

            target.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();

            if(Group == null)
            {
                graphView.RemoveUngroupedNode(this);

                DialogueName = target.value;

                graphView.AddUngroupedNode(this);
                return;
            }

            DialogueSystemGroup currentGroup = Group;

            graphView.RemoveGroupedNode(this, Group);

            DialogueName = callback.newValue;

            graphView.AddGroupedNode(this, currentGroup);
        });

        dialogueNameTextField.AddClasses(
            "dialogue-node__textfield",
            "dialogue-node__filename-textfield",
            "dialogue-node__textfield__hidden"
            );

        titleContainer.Insert(0, dialogueNameTextField);

        /* INPUT CONTAINER */
        Port inputPort = DialogueElementUtility.CreatePort(this, "Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
        inputContainer.Add(inputPort);

        /* EXTENSION CONTAINER */
        VisualElement customDataContainer = new VisualElement();

        customDataContainer.AddToClassList("dialogue-node__custom-data-container");

        Foldout textFoldout = DialogueElementUtility.CreateFoldout("Dialogue Text");

        TextField textTextField = DialogueElementUtility.CreateTextArea(Text);

        textTextField.AddClasses(
            "dialogue-node__textfield", 
            "dialogue-node__quote-textfield"
            );

        textFoldout.Add(textTextField);

        customDataContainer.Add(textFoldout);
        extensionContainer.Add(customDataContainer);


        
        /* OUTPUT CONTAINER */

        foreach (DialogueChoiceSaveData choice in Choices)
        {
            Port choicePort = CreateChoicePort(choice);
            outputContainer.Add(choicePort);
        }


        RefreshExpandedState();
    }

    #region Elements Creation
    private Port CreateChoicePort(object userData)
    {
        Port choicePort = DialogueElementUtility.CreatePort(this);

        choicePort.userData = userData;

        DialogueChoiceSaveData choiceData = (DialogueChoiceSaveData) userData;

        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X", () =>
        {
            if (Choices.Count == 1)
                return;

            if (choicePort.connected)
            {
                graphView.DeleteElements(choicePort.connections);
            }

            Choices.Remove(choiceData);
            graphView.RemoveElement(choicePort);

        });

        deleteChoiceButton.AddToClassList("dialogue-node__button");

        TextField choiceTextField = DialogueElementUtility.CreateTextField(choiceData.Text, null, callback => 
        {
            choiceData.Text = callback.newValue;
        });

        choiceTextField.AddClasses(
            "dialogue-node__textfield",
            "dialogue-node__choice-textfield",
            "dialogue-node__textfield__hidden"
            );

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }
    #endregion


    #region Utility Methods

    public void DisconnectAllPorts()
    {
        DisconnectPorts(inputContainer);
        DisconnectPorts(outputContainer);
    }

    private void DisconnectInputPorts()
    {
        DisconnectPorts(inputContainer);
    }
    private void DisconnectOutputPorts()
    {
        DisconnectPorts(outputContainer);
    }

    private void SetNodeID()
    {
        nodeID = graphView.nodeIDs.Count;

        while (graphView.nodeIDs.Contains(nodeID))
        {
            if (nodeID == graphView.nodeIDs.Count) 
            {
                nodeID = 0;
                continue;
            }

            nodeID++;
        }

        graphView.nodeIDs.Add(nodeID);
    }

    private void DisconnectPorts(VisualElement container)
    {
        foreach (Port port in container.Children())
        {
            if (!port.connected)
                continue;

            graphView.DeleteElements(port.connections);
        }
    }

    public void SetErrorColor(Color color)
    {
        mainContainer.style.backgroundColor = color;
    }

    public void ResetColorToDefault()
    {
        mainContainer.style.backgroundColor = new Color(29f / 255, 29 / 255, 30 / 255);
    }
    #endregion
}