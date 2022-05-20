using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMasterNode : Node
{
    public string DialogueName { get; set; }
    public List<string> Choices { get; set; }
    public string Text { get; set; }
    
    public void Initialize(Vector2 position)
    {
        DialogueName = "DialogueName";
        Choices = new List<string>();
        Text = "Dialogue text.";


        SetPosition(new Rect(position, Vector2.zero));

        mainContainer.AddToClassList("dialogue-node__main-container");
        extensionContainer.AddToClassList("dialogue-node__extension-container");
        
        Choices.Add("New Choice");
    }


    public void Draw()
    {
        /* MAIN CONTAINER */
        Button addChoiceButton = new Button()
        {
            text = "Add Choice"
        };

        addChoiceButton.AddToClassList("dialogue-node__button");

        mainContainer.Insert(1, addChoiceButton);

        /* TITLE CONTAINER */
        TextField dialogueNameTextField = new TextField()
        {
            value = DialogueName
        };

        dialogueNameTextField.AddToClassList("dialogue-node__textfield");
        dialogueNameTextField.AddToClassList("dialogue-node__filename-textfield");
        dialogueNameTextField.AddToClassList("dialogue-node__textfield__hidden");

        titleContainer.Insert(0, dialogueNameTextField);

        /* INPUT CONTAINER */
        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        //Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

        inputPort.portName = "Dialogue Connection";
        //outputPort.portName = "Next Dialogue";

        inputContainer.Add(inputPort);
        //inputContainer.Add(outputPort);



        /* EXTENSION CONTAINER */
        VisualElement customDataContainer = new VisualElement();

        customDataContainer.AddToClassList("dialogue-node__custom-data-container");

        Foldout textFoldout = new Foldout()
        {
            text = "Dialogue Text"
        };

        TextField textTextField = new TextField()
        {
            value = Text
        };

        textTextField.AddToClassList("dialogue-node__textfield");
        textTextField.AddToClassList("dialogue-node__quote-textfield");

        textFoldout.Add(textTextField);

        customDataContainer.Add(textFoldout);
        extensionContainer.Add(customDataContainer);


        
        /* OUTPUT CONTAINER */

        foreach (string choice in Choices)
        {
            Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

            choicePort.portName = "";

            Button deleteChoiceButton = new Button()
            {
                text = "X"
            };

            deleteChoiceButton.AddToClassList("dialogue-node__button");

            TextField choiceTextField = new TextField()
            {
                value = choice
            };

            choiceTextField.AddToClassList("dialogue-node__textfield");
            choiceTextField.AddToClassList("dialogue-node__choice-textfield");
            choiceTextField.AddToClassList("dialogue-node__textfield__hidden");

            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceButton);

            outputContainer.Add(choicePort);
        }


        RefreshExpandedState();
    }
}
