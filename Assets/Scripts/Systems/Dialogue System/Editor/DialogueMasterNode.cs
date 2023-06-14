using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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


    private DialogueMasterGraphView graphView;
    private Image image = DialogueElementUtility.CreateImage(new Rect(Vector2.zero, Vector2.one * 32));
    private VisualElement dialoguePreview = new VisualElement();
    private TextField dialoguePreviewText;

    Vec2VE portraitPosition = new Vec2VE();
    Vec2VE dialogueBoxPosition = new Vec2VE();
    Vec2VE dialogueBoxSize = new Vec2VE();





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

        /*Preview*/
        #region Preview

        dialoguePreview.focusable = false;
        dialoguePreview.style.width = 1920;
        dialoguePreview.style.height = 1080;

        dialoguePreviewText = DialogueElementUtility.CreateTextArea(DialogueText, null, callback =>
        {
            TextField previewText = callback.currentTarget as TextField;

            Vector2 textSize = previewText.MeasureTextSize(callback.newValue, 0, MeasureMode.Undefined, previewText.contentRect.height, MeasureMode.Exactly);

            Vector2 textBoundaries = GetTextBoundaries(callback.newValue, previewText.style.unityFont.value, DialogueMasterElements.Instance.fontSize);

            Debug.Log(textBoundaries);
            Debug.Log(previewText.contentRect.size);
        });

        dialoguePreviewText.focusable = false;
        dialoguePreviewText.style.unityFont = (Font)AssetDatabase.LoadAssetAtPath("Assets/Fonts/Golden Sun Italics.ttf",typeof(Font));
        dialoguePreviewText.style.backgroundImage = new StyleBackground(DialogueMasterElements.Instance.dialogueBackground);
        //"Assets/Fonts/" + DialogueMasterElements.Font "

        //dialoguePreview.style.overflow = Overflow.Hidden;

        DialogueElementUtility.SetTextStyle(ref dialoguePreviewText, fontSize, previewMargins, Color.clear, textColor);

        if(isShadowed)
            DialogueElementUtility.CreateDropShadow(ref dialoguePreviewText, shadowColor, shadowDirection, fontSize * shadowMagnitude * 0.02f);



        //previewText.style.overflow = Overflow.Hidden;

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
            dialoguePreviewText.value = callback.newValue;
            if (isShadowed)
            {
                TextField previewShadow = (TextField)dialoguePreviewText.contentContainer[0];
                previewShadow.value = callback.newValue;
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
        Foldout boxFoldout = DialogueElementUtility.CreateFoldout("Dialogue Box", true);
        dialogueBoxPosition.Initialize("Position");
        
        dialogueBoxSize.Initialize("Size", 
            xCallback =>
            {
                DialogueElementUtility.RemoveCharactersNaN(xCallback, out dialogueBoxSize.vector.x);
                DialogueElementUtility.SetStyleSize(ref dialoguePreviewText, dialogueBoxSize.vector.x, dialoguePreviewText.style.height.value.value);
            },
            yCallback =>
            {
                DialogueElementUtility.RemoveCharactersNaN(yCallback, out dialogueBoxSize.vector.y);
                DialogueElementUtility.SetStyleSize(ref dialoguePreviewText, dialoguePreviewText.style.width.value.value, dialogueBoxSize.vector.y);
            });

        boxFoldout.Add(dialogueBoxPosition.foldout);
        boxFoldout.Add(dialogueBoxSize.foldout);

        customDataContainer.Add(boxFoldout);
        #endregion

        /* PORTRAIT */
        #region PORTRAIT
        Foldout portraitFoldout = DialogueElementUtility.CreateFoldout("Portrait", true);

        ObjectField spriteField = DialogueElementUtility.CreateObjectField<Sprite>(callback => 
        {
            Sprite newSprite = callback.newValue as Sprite;

            dialoguePreviewText.style.backgroundImage = new StyleBackground(newSprite);

            DialogueElementUtility.SetStyleSlice(ref dialoguePreviewText, newSprite.border);

            image.sprite = newSprite;

            if (image.sprite == null)
                DialogueElementUtility.SetStyleSize(ref image, 0, 0);
            else 
            { 
                DialogueElementUtility.SetStyleSize(ref image, 128, 128);
            }
        });

        portraitPosition.Initialize("Position");


        portraitFoldout.Add(portraitPosition.foldout);
        portraitFoldout.Add(spriteField);
        portraitFoldout.Add(image);

        customDataContainer.Add(portraitFoldout);
        #endregion

        /* PREVIEW */
        #region PREVIEW

        Foldout previewFoldout = DialogueElementUtility.CreateFoldout("Preview", true);

        dialoguePreview.Add(dialoguePreviewText);
      
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
        image.sprite = node.portrait.sprite;

        portraitPosition.SetValues(node.portrait.position);
        dialogueBoxPosition.SetValues(node.dialogueBox.position);
        dialogueBoxSize.SetValues(node.dialogueBox.size);
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
        serializedNode.position = GetPosition().position;
        serializedNode.portrait.sprite = image.sprite;
        serializedNode.portrait.position = portraitPosition.vector;
        serializedNode.dialogueBox.position = dialogueBoxPosition.vector;
        serializedNode.dialogueBox.size = dialogueBoxSize.vector;

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

 
}

