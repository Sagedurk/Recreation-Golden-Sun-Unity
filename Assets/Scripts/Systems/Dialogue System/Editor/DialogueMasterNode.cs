using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DialogueMasterNode : Node
{
    /* VARIABLE DECLARATION */
    #region VARIABLE DECLARATION
    public List<DialogueMasterNodeChoice> Choices { get; set; }
    public string DialogueText { get; set; }
    public DialogueSystemGroup Group { get; set; }
    public int NodeID { get; set; }
    

    public Port inputPort = null;

    public string dialogueTextInitializer = "Dialogue Text.";

    public int choicePromptIndex = 0;


    private DialogueMasterGraphView graphView;
    private Image image = DialogueElementUtility.CreateImage(new Rect(Vector2.zero, Vector2.one * 32));
    private VisualElement dialoguePreview = new VisualElement();
    private TextField dialoguePreviewText;
    private VisualElement dialoguePortraitFramePreview = new VisualElement();
    private Image dialoguePortraitPreview = DialogueElementUtility.CreateImage(new Rect(Vector2.zero, Vector2.one * 32));
    private DropdownField positionDropdown;
    private DropdownField choicePromptDropdown;


    Vec2VE dialogueBoxSize = new Vec2VE();

    private enum ChoicePrompts
    {
        TOP,
        BOTTOM
    }



    //Port to DialogueMaster!
    int fontSize = 75;
    Vector4 previewMargins = new Vector4(28, 25, 28, 28);       //Left, Bottom, Right, Top
    Color textColor = Color.white;
    Sprite backgroundImage;

        //Shadow
        bool isShadowed = true;
        Color shadowColor = Color.black;
        Vector2 shadowDirection = Vector2.down + Vector2.right;
        float shadowMagnitude = 3.025f;


    /* END VARIABLE DECLARATION */
    #endregion




    public void Initialize(DialogueMasterGraphView dialogueGraphView, Vector2 position)
    {
        graphView = dialogueGraphView;

        SetNodeID();
        

        Choices = new List<DialogueMasterNodeChoice>();
        DialogueText = dialogueTextInitializer;

        SetPosition(new Rect(position, Vector2.zero));

        mainContainer.AddToClassList("dialogue-node__main-container");
        extensionContainer.AddToClassList("dialogue-node__extension-container");


        if(Choices.Count < 1)
        {
            DialogueMasterNodeChoice choiceData = new DialogueMasterNodeChoice();
            Choices.Add(choiceData);
        }

        string[] choicePromptArray = Enum.GetNames(typeof(ChoicePrompts));

        List<string> choicePromptList = new List<string>();
        foreach (string enumName in choicePromptArray)
        {
            choicePromptList.Add(enumName);
        }
        
        choicePromptDropdown = DialogueElementUtility.CreateDropdown(choicePromptList, 0, callback =>
        {
            DropdownField dropdown = callback.target as DropdownField;

            choicePromptIndex = dropdown.index;
            Debug.Log(choicePromptIndex);
        });

        /*Preview*/
        #region Preview

        //dialoguePreview.focusable = false;
        dialoguePreview.style.width = 1920;
        dialoguePreview.style.height = 1080;
        //dialoguePreview.style.backgroundColor = new StyleColor(Color.green);
        dialoguePreview.style.alignItems = Align.FlexStart;
        dialoguePreview.style.alignContent = Align.FlexStart;
        DialogueElementUtility.SetBorderColor(ref dialoguePreview, Color.white);
        DialogueElementUtility.SetBorderWidth(ref dialoguePreview, 1);

        #region Box
        dialoguePreviewText = DialogueElementUtility.CreateTextArea(DialogueText, null, callback => 
        {
            Vector2 oldSize = Vector2.right * dialoguePreviewText.resolvedStyle.width + Vector2.up * dialoguePreviewText.resolvedStyle.height;
            UnityEditor.EditorApplication.delayCall += () =>
            {
                VisualElement textElement = dialoguePreviewText;
                DialogueElementUtility.SetPositionInRelationToParent(ref textElement, (DialogueElementUtility.Alignment)positionDropdown.index);
                
            };

        });


        //dialoguePreviewText.delegatesFocus = false;
        dialoguePreviewText.focusable = false;
        dialoguePreviewText.pickingMode = PickingMode.Position;
        dialoguePreviewText.style.unityFontDefinition = FontDefinition.FromFont(DialogueMasterElements.Instance.font);
        dialoguePreviewText.style.fontSize = DialogueMasterElements.Instance.fontSize;
        dialoguePreviewText.style.backgroundImage = new StyleBackground(DialogueMasterElements.Instance.dialogueBackground);
        dialoguePreviewText.style.position = Position.Absolute;
        //dialoguePreviewText.style.alignSelf = Align.FlexStart;

        VisualElement previewText = dialoguePreviewText;
        //DialogueElementUtility.SetBorderColor(ref previewText, Color.red);
        //DialogueElementUtility.SetBorderWidth(ref previewText, 1);


        DialogueElementUtility.SetTextStyle(ref dialoguePreviewText, DialogueMasterElements.Instance.fontSize, previewMargins, Color.clear, textColor);

        if(DialogueMasterElements.Instance.isShadowed)
            DialogueElementUtility.CreateDropShadow(ref dialoguePreviewText, shadowColor, shadowDirection, DialogueMasterElements.Instance.fontSize * shadowMagnitude * 0.02f);

        dialoguePreview.Add(dialoguePreviewText);

        DragAndDropManipulator boxManipulator = new DragAndDropManipulator(dialoguePreviewText, dialoguePreview);


        #endregion

        dialoguePortraitFramePreview.style.backgroundImage = new StyleBackground(DialogueMasterElements.Instance.portraitFrame);
        dialoguePortraitFramePreview.style.width = DialogueMasterElements.Instance.portraitFrame.rect.width;
        dialoguePortraitFramePreview.style.height = DialogueMasterElements.Instance.portraitFrame.rect.height;
        dialoguePortraitFramePreview.style.position = Position.Absolute;
        DragAndDropManipulator portraitManipulator = 
            new DragAndDropManipulator(dialoguePortraitFramePreview, dialoguePreview, dialoguePreviewText, DragAndDropManipulator.DragAndDropType.OUTSIDE);


        dialoguePortraitFramePreview.Add(dialoguePortraitPreview);
        dialoguePortraitPreview.transform.position += (Vector3.up + Vector3.right) * 16;
        DialogueElementUtility.SetStyleSize(ref dialoguePortraitPreview, 128, 128);

        dialoguePortraitFramePreview.visible = false;


        boxManipulator.AddManipulatorToGroup(portraitManipulator);

        #endregion

        
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
        #region MAIN CONTAINER
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
        #endregion

        /* TITLE BUTTON CONTAINER */
        #region TITLE BUTTON CONTAINER

        //Remove minimize button from top right corner of the node
        titleButtonContainer.contentContainer.RemoveFromHierarchy();

        #endregion

        /* INPUT CONTAINER */
        #region INPUT CONTAINER
        inputPort = DialogueElementUtility.CreatePort(this, "Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
        inputContainer.Add(inputPort);
        #endregion

        /* EXTENSION CONTAINER */
        #region EXTENSION CONTAINER

        VisualElement customDataContainer = new VisualElement();

        customDataContainer.AddToClassList("dialogue-node__custom-data-container");

        /* DIALOGUE TEXT */
        #region DIALOGUE TEXT
        Foldout textFoldout = DialogueElementUtility.CreateFoldout("Dialogue Text");

        TextField dialogueText = DialogueElementUtility.CreateTextArea(DialogueText, null, callback =>
        {
            DialogueText = callback.newValue;

           
            dialoguePreviewText.value = RemoveTextBetweenCharacters(callback.newValue, '<', '>');

            if (isShadowed)
            {
                TextField previewShadow = (TextField)dialoguePreviewText.contentContainer[0];
                previewShadow.value = dialoguePreviewText.value;
            }

            var textArea = callback.target as TextField;


            UnityEditor.EditorApplication.delayCall += () =>
            {

                Foldout previewFoldout = dialoguePreview.parent as Foldout;

                if (!previewFoldout.value)
                {
                    previewFoldout.value = true;

                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        dialogueBoxSize.SetValues(dialoguePreviewText.contentRect.size);
                        previewFoldout.value = false;
                    };
                }
                else
                {
                    dialogueBoxSize.SetValues(dialoguePreviewText.contentRect.size);
                }

            };

        });

        dialogueText.AddClasses("dialogue-node__textfield", "dialogue-node__quote-textfield");

        textFoldout.Add(dialogueText);


        customDataContainer.Add(textFoldout);
        #endregion

        /* DIALOGUE BOX */
        #region DIALOGUE BOX
        Foldout boxFoldout = DialogueElementUtility.CreateFoldout("Position", true);


        string[] enumNames = Enum.GetNames(typeof(DialogueElementUtility.Alignment));
        List<string> dropdownOptions = new List<string>();
        foreach (string name in enumNames)
        {
            dropdownOptions.Add(name);
        }

        positionDropdown = DialogueElementUtility.CreateDropdown(dropdownOptions, 0, callback => 
        {
            DropdownField dropdown = callback.target as DropdownField;

            VisualElement previewText = dialoguePreviewText;
            DialogueElementUtility.SetPositionInRelationToParent(ref previewText, (DialogueElementUtility.Alignment) dropdown.index);
        });

        boxFoldout.Add(positionDropdown);

        customDataContainer.Add(boxFoldout);
        #endregion

        /* PORTRAIT */
        #region PORTRAIT
        Foldout portraitFoldout = DialogueElementUtility.CreateFoldout("Portrait", true);

        ObjectField spriteField = DialogueElementUtility.CreateObjectField<Sprite>(callback =>
        {
            Sprite newSprite = callback.newValue as Sprite;
            image.sprite = newSprite;
            dialoguePortraitPreview.sprite = newSprite;

            if (image.sprite == null)
            {
                DialogueElementUtility.SetStyleSize(ref image, 0, 0);
                dialoguePortraitFramePreview.visible = false;
                dialoguePortraitPreview.visible = false;
            }
            else
            {
                DialogueElementUtility.SetStyleSize(ref image, 128, 128);
                dialoguePortraitFramePreview.visible = true;
                dialoguePortraitPreview.visible = true;
                
            }
        });


        portraitFoldout.Add(spriteField);
        portraitFoldout.Add(image);

        customDataContainer.Add(portraitFoldout);
        #endregion

        /* PREVIEW */
        #region PREVIEW

        Foldout previewFoldout = DialogueElementUtility.CreateFoldout("Preview", true, callback => 
        {
            dialoguePreviewText.MarkDirtyRepaint();

        });

        
        dialoguePreview.Add(dialoguePortraitFramePreview);

        previewFoldout.Add(dialoguePreview);
        customDataContainer.Add(previewFoldout);

        #endregion
        extensionContainer.Add(customDataContainer);
        #endregion


        /* OUTPUT CONTAINER */
        #region OUTPUT CONTAINER
        foreach (DialogueMasterNodeChoice choice in Choices)
        {
            Port choicePort = CreateChoicePort(choice);
            outputContainer.Add(choicePort);
        }
        #endregion


        //Needed for extension container
        RefreshExpandedState();
    }


    /* ELEMENT CREATION */
    #region ELEMENT CREATION
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

                if(extensionContainer.Contains(choicePromptDropdown))
                    extensionContainer.Remove(choicePromptDropdown);
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
        {
            choicePort.Add(choiceData.requirementTypeToolbar);


            extensionContainer.Add(choicePromptDropdown);
        }

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

    /* UTILITY FUNCTIONS */
    #region UTILITY FUNCTIONS 

    public void DisconnectAllPorts()
    {
        DisconnectInputPorts();
        DisconnectOutputPorts();
    }

    private void DisconnectInputPorts()
    {
        //Check if node is connected to starterNode, and if, remove starterNode connection
        foreach (Edge edge in inputPort.connections)
        {
            if (edge.output.node is DialogueMasterStarterNode node)
            {
                node.SetStarterNode(null);
            }
        }

        DisconnectPorts(inputContainer);
    }
    private void DisconnectOutputPorts()
    {
        DisconnectPorts(outputContainer);
    }

    private void SetNodeID()
    {
        NodeID = graphView.nodeIDs.Count;

        while (graphView.nodeIDs.Contains(NodeID))
        {
            if (NodeID == graphView.nodeIDs.Count) 
            {
                NodeID = 0;
                continue;
            }

            NodeID++;
        }

        graphView.nodeIDs.Add(NodeID);
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

    /*CONVERSION FOR SAVING */
    #region CONVERSION FOR SAVING

    public void ConvertToEditorNode(DialogueEditorSerializedNode node)
    {
        SetPosition(new Rect(node.position, Vector2.zero));

        NodeID = node.nodeID;


        DialogueText = node.dialogueText;

        DialogueText = DialogueText.Replace("<link=\"shake\">", "<shake>");
        DialogueText = DialogueText.Replace("</link>", "</shake>");

        image.sprite = node.portrait.sprite;
        dialoguePortraitPreview.sprite = node.portrait.sprite;

        if(dialoguePortraitPreview.sprite != null)
            dialoguePortraitFramePreview.visible = true;

        dialoguePortraitFramePreview.transform.position = node.portrait.position * (Vector2.right + Vector2.down);
        dialoguePreviewText.transform.position = node.dialogueBox.position * (Vector2.right + Vector2.down);
        dialogueBoxSize.SetValues(node.dialogueBox.size);
        choicePromptIndex = node.choicePromptIndex;
    }

    public DialogueEditorSerializedNode ConvertToSerializedNode()
    {
        DialogueEditorSerializedNode serializedNode = new DialogueEditorSerializedNode();

        serializedNode.nodeID = NodeID;

        foreach (DialogueMasterNodeChoice choice in Choices)
        {
            SerializedChoice serializedChoice = choice.ConvertToSerializedChoice();

            serializedNode.choices.Add(serializedChoice);
        }


        serializedNode.dialogueText = DialogueText;


        serializedNode.dialogueText = serializedNode.dialogueText.Replace("<shake>", "<link=\"shake\">");
        serializedNode.dialogueText = serializedNode.dialogueText.Replace("</shake>", "</link>");

        serializedNode.position = GetPosition().position;
        serializedNode.portrait.sprite = dialoguePortraitPreview.sprite;
        serializedNode.portrait.position = dialoguePortraitFramePreview.transform.position * (Vector2.right + Vector2.down);
        serializedNode.dialogueBox.position = dialoguePreviewText.transform.position * (Vector2.right + Vector2.down);

        serializedNode.dialogueBox.size = dialogueBoxSize.vector;

        serializedNode.choicePromptIndex = choicePromptIndex;

        return serializedNode;
    }

    public void UpdateNodeData(ref DialogueEditorSerializedNode oldData, DialogueEditorSerializedNode newData)
    {
        oldData.nodeID = newData.nodeID;
        oldData.choices = newData.choices;
        oldData.position = newData.position;
        oldData.portrait = newData.portrait;
        oldData.dialogueText = newData.dialogueText;
    }

    #endregion

    public Vector2 GetTextBoundaries(string text, Font font, int fontSize)
    {


        TextGenerationSettings settings = new TextGenerationSettings();
        settings.color = Color.white;
        settings.textAnchor = TextAnchor.MiddleCenter;
        settings.verticalOverflow = VerticalWrapMode.Overflow;
        settings.horizontalOverflow = HorizontalWrapMode.Overflow;

        settings.updateBounds = true;
        settings.fontStyle = FontStyle.Normal;

        settings.font = font;
        settings.fontSize = fontSize;
        settings.richText = true;

        //settings.textAnchor = TextAnchor.UpperLeft;
        //settings.generationExtents = new Vector2(1920, 1080);
        settings.pivot = Vector2.zero;

        TextGenerator generator = new TextGenerator();
        generator.Invalidate();
        generator.Populate(text, settings);

        //return generator.rectExtents.size;


        float measuredWidth = generator.GetPreferredWidth(text, settings);
        float measuredHeight = generator.GetPreferredHeight(text, settings);

        return new Vector2(measuredWidth, measuredHeight);
    }


    string RemoveTextBetweenCharacters(string input, char startChar, char endChar)
    {
        int startIndex = input.IndexOf(startChar);
        int endIndex = input.IndexOf(endChar, startIndex + 1);

        while (startIndex != -1 && endIndex != -1)
        {
            input = input.Remove(startIndex, endIndex - startIndex + 1);
            startIndex = input.IndexOf(startChar, startIndex);
            endIndex = input.IndexOf(endChar, startIndex + 1);
        }

        return input;
    }


}

