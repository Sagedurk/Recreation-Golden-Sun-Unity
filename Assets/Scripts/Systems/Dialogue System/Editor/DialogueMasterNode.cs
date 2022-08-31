using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;


public class DialogueMasterNode : Node
{
    public List<DialogueMasterNodeChoice> Choices { get; set; }
    public string Text { get; set; }
    public DialogueSystemGroup Group { get; set; }
    public int nodeID { get; set; }

    private DialogueMasterGraphView graphView;


    /*
     * Container names
     *  mainContainer           //includes all other containers
     *  titleContainer          //Title bar container
     *  titleButtonContainer    //Title bar button container. Contains the top right buttons
     *  inputContainer          //Input container used for input ports
     *  outputContainer         //Output container, used for output ports
     *  extensionContainer      //Empty Container used to display custom elements. 
     *      After adding elements, call RefreshExpandedState in order to toggle this container visibility
     */

    public void Initialize(DialogueMasterGraphView dialogueGraphView, Vector2 position)
    {
        graphView = dialogueGraphView;

        SetNodeID();
        

        Choices = new List<DialogueMasterNodeChoice>();
        Text = "Dialogue text.";

        SetPosition(new Rect(position, Vector2.zero));

        mainContainer.AddToClassList("dialogue-node__main-container");
        extensionContainer.AddToClassList("dialogue-node__extension-container");

        DialogueMasterNodeChoice choiceData = new DialogueMasterNodeChoice();

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
            DialogueMasterNodeChoice choiceData = new DialogueMasterNodeChoice();
            Choices.Add(choiceData);

            Port choicePort = CreateChoicePort(choiceData);

            ReAddToolbarToChoice();

            outputContainer.Add(choicePort);
        });

        addChoiceButton.AddToClassList("dialogue-node__button");

        mainContainer.Insert(1, addChoiceButton);

        
        //Remove minimize button from top right corner of the node
        titleButtonContainer.contentContainer.RemoveFromHierarchy();

        /* INPUT CONTAINER */
        Port inputPort = DialogueElementUtility.CreatePort(this, "Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
        inputContainer.Add(inputPort);

        /* EXTENSION CONTAINER */
        VisualElement customDataContainer = new VisualElement();

        customDataContainer.AddToClassList("dialogue-node__custom-data-container");

        Foldout textFoldout = DialogueElementUtility.CreateFoldout("Dialogue Text");

        TextField textTextField = DialogueElementUtility.CreateTextArea(Text, null, callback=>
        {
            Text = callback.newValue;
        });

        textTextField.AddClasses(
            "dialogue-node__textfield", 
            "dialogue-node__quote-textfield"
            );

        textFoldout.Add(textTextField);

        customDataContainer.Add(textFoldout);
        extensionContainer.Add(customDataContainer);


        
        /* OUTPUT CONTAINER */

        foreach (DialogueMasterNodeChoice choice in Choices)
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

        DialogueMasterNodeChoice choiceData = (DialogueMasterNodeChoice) userData;
        choiceData.SetRequirementInstance(graphView);
        choiceData.SetOwningPort(choicePort);

        choicePort.userData = userData;


        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X", () =>
        {
            //Execute action when button is pressed
            if (Choices.Count == 1)
                return;

            if (choicePort.connected)
                graphView.DeleteElements(choicePort.connections);
            

            Choices.Remove(choiceData);
            graphView.RemoveElement(choicePort);

            //If only 1 choice remains on the node after deleting a choice, remove the requirement toolbar
            if(Choices.Count == 1)
            {
                DialogueMasterNodeChoice choice = Choices[0];
                if (choice.owningPort.Contains(choice.requirementTypeToolbar))  //Safety check for requirement type toolbar
                {
                    choice.owningPort.Remove(choice.requirementTypeToolbar);
                    choice.ResetToolbarDropdown();
                }
            }

        });

        deleteChoiceButton.AddToClassList("dialogue-node__button");

        TextField choiceTextField = DialogueElementUtility.CreateTextField(choiceData.choiceName, null, callback => 
        {
            choiceData.choiceName = callback.newValue;
        });

        choiceTextField.AddClasses(
            "dialogue-node__textfield",
            "dialogue-node__choice-textfield",
            "dialogue-node__textfield__hidden"
            );

        if(Choices.Count > 1)
            choicePort.Add(choiceData.requirementTypeToolbar);

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }

    private void ReAddToolbarToChoice()
    {
        if (Choices.Count != 2)     //This function only needs to execute if the node has 2 choices
            return;

        foreach (DialogueMasterNodeChoice choice in Choices)
        {
            //If a choice's port already has the requirement toolbar, there's no need to rearrange it
            if (choice.owningPort.Contains(choice.requirementTypeToolbar))  
                continue;
            
            List<VisualElement> elementsToReAdd = new List<VisualElement>();

            foreach (VisualElement element in choice.owningPort.Children())
            {
                elementsToReAdd.Add(element);
            }

            choice.owningPort.Add(choice.requirementTypeToolbar);

            for (int i = 2; i < elementsToReAdd.Count; i++)
            {
                choice.owningPort.Remove(elementsToReAdd[i]);
                choice.owningPort.Add(elementsToReAdd[i]);
            }
            
            elementsToReAdd.Clear();

        }
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