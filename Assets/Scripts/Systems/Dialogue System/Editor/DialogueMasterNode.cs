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
        Button addChoiceButton = DialogueElementUtility.CreateButton("Add Choice", () =>
        {
            Port choicePort = CreateChoicePort("New Choice");
            Choices.Add("New Choice");

            outputContainer.Add(choicePort);
        });

        addChoiceButton.AddToClassList("dialogue-node__button");

        mainContainer.Insert(1, addChoiceButton);

        /* TITLE CONTAINER */
        TextField dialogueNameTextField = DialogueElementUtility.CreateTextField(DialogueName);

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

        foreach (string choice in Choices)
        {
            Port choicePort = CreateChoicePort(choice);
            outputContainer.Add(choicePort);
        }


        RefreshExpandedState();
    }

    #region Elements Creation
    private Port CreateChoicePort(string choice)
    {
        Port choicePort = DialogueElementUtility.CreatePort(this);

        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X");

        deleteChoiceButton.AddToClassList("dialogue-node__button");

        TextField choiceTextField = DialogueElementUtility.CreateTextField(choice);

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
}
