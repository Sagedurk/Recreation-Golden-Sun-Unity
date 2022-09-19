using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueMaster : MonoBehaviour
{

    public GameObject choicePromptContainer;
    public InputBehaviour inputBehaviour;
    public Image dialogueBackground;
    public GameObject dialoguePortraitFrame;

    public Image dialoguePortrait;
    public Text dialogueText;
    public Text dialogueTextShadow;

    public DialogueActions dialogueActions;

    int promptIndex;
    int lastSubIndex;

    [System.Serializable]
    public class DialogueInstance
    {

        //dialogue box
        public DialogueBox dialogueBox;

        //portrait
        public CharacterPortrait portrait;

        //dialogue options
        public Prompt dialoguePrompt;

        public DialogueAction dialogueAction;
        public string actionName;

        public List<string> listOfOptionNames = new List<string>();
        public List<choice> listOfOptions = new List<choice>();
        public int optionListIndex = 0;


    }
    public class NodeInstance
    {
        public int nodeID;
        public List<int> connectedNodesIDs = new List<int>();
        public string testDialogueString;


        //dialogue box
        public DialogueBox dialogueBox;

        //portrait
        public CharacterPortrait portrait;

        //dialogue options
        public Prompt dialoguePrompt;

        public DialogueAction dialogueAction;
        public string actionName;

        public List<string> listOfOptionNames = new List<string>();
        public List<choice> listOfOptions = new List<choice>();
        public int optionListIndex = 0;


    }

    [System.Serializable]
    public class choice
    {
        public List<DialogueInstance> dialogueChoiceSubInstances = new List<DialogueInstance>();
        public bool isLoopingLastSubInstance = false;
        
    }
    [System.Serializable]
    public class Prompt
    {
        public Vector3 position = new Vector3(0, 0, 0);
        public DialogueChoices dialogueChoice;
    }

    [System.Serializable]
    public struct SubInstance
    {
        //dialogue box
        public DialogueBox dialogueBox;

        //portrait
        public CharacterPortrait portrait;
    }



    [System.Serializable]
    public struct DialogueBox
    {
        public Vector2 boxPosition;
        public Vector2 boxSize;
        public DialogueText dialogueText;
    }


    [System.Serializable]
    public struct CharacterPortrait
    {
        public Sprite portraitImage;
        public Object portraitObject;
        public Vector2 portraitBoxPosition;
        public bool isPortraitShown;
    }

    [System.Serializable]
    public class DialogueText
    {
        //text
        [TextArea(3, 5)]
        public string dialogueString;

        [Header("Character")]
        public Font dialogueFont ;
        public FontStyle dialogueFontStyle = FontStyle.Normal;
        public int dialogueFontSize = 200;
        public float dialogueLineSpacing = 1;
        public bool dialogueRichText = true;

        [Header("Paragraph")]
        public TextAnchor dialogueAlignment = TextAnchor.MiddleLeft;
        public bool dialogueAlignByGeometry = false;
        public HorizontalWrapMode dialogueHorizontalOverflow = HorizontalWrapMode.Wrap;
        public VerticalWrapMode dialogueVerticalOverflow = VerticalWrapMode.Truncate;
        public bool dialogueBestFit = false;

        [Header("")]
        public Color dialogueColor = Color.white;
        public Material dialogueMaterial = null;
        public bool dialogueRaycastTarget = true;
        public Vector4 dialogueRaycastPadding = Vector4.zero;
        public bool dialogueMaskable = true;

    }


    public enum DialogueChoices
    {
        INACTIVE,
        ACTIVE,
    }

    public enum DialogueAction
    {
        NONE,
        BEFORE_DIALOGUE,
        AFTER_DIALOGUE
    }


    // Start is called before the first frame update
    void Start()
    {
        //ShowChoicePromt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DebugAllDialogue(List<DialogueInstance> instanceList)
    {
        StartCoroutine(Dialogue(instanceList));
    } 
    
    public void DebugAllNodes(Dictionary<int, DialogueEditorSerializedNode> instanceDictionary, int starterNodeID)
    {
        StartCoroutine(NodeDialogue(instanceDictionary, starterNodeID));
    }

    private IEnumerator Dialogue(List<DialogueInstance> instanceList)
    {
        ShowDialogueBackground();
        for (int i = 0; i < instanceList.Count; i++)
        {
            DialogueInstance currentInstance = instanceList[i];

            //If action should happen before the dialogue instance
            if (currentInstance.dialogueAction == DialogueAction.BEFORE_DIALOGUE)
            {
                HideDialogue();
                if (!dialogueActions.isRunning)
                {
                    dialogueActions.StartCoroutine(currentInstance.actionName);
                }

                while (dialogueActions.isRunning)

                    yield return null;

                ShowDialogueBackground(false);
            }



            //Show next dialogue
            ShowDialogueInstance(currentInstance);

            //If yes/no prompt
            if (currentInstance.dialoguePrompt.dialogueChoice == DialogueChoices.ACTIVE)
            {
                ShowChoicePromt(currentInstance.dialoguePrompt.position);

                //Wait for input
                while (!inputBehaviour.isInteracted)
                    yield return null;



                //if input is given this frame

                HideChoicePromt();
                inputBehaviour.isInteracted = false;


                lastSubIndex = -1;
                currentInstance.optionListIndex = promptIndex;


                //Loop through the sub instances
                for (int k = 0; k < currentInstance.listOfOptions[currentInstance.optionListIndex].dialogueChoiceSubInstances.Count; k++)
                {
                    ShowDialogueInstance(currentInstance.listOfOptions[currentInstance.optionListIndex].dialogueChoiceSubInstances[k]);
                    lastSubIndex++;

                    //Wait for input
                    while (!inputBehaviour.isInteracted)
                        yield return null;


                    inputBehaviour.isInteracted = false;
                }

                //check if the last subInstance is looping.
                if (currentInstance.listOfOptions[currentInstance.optionListIndex].isLoopingLastSubInstance)
                {
                    //Loop indefinitely if the prompt chosen equals the prompt that caused the looping
                    //Needs to process button input

                    while (promptIndex == currentInstance.optionListIndex)
                    {
                        ShowChoicePromt(currentInstance.dialoguePrompt.position);

                        while (!inputBehaviour.isInteracted)
                        {

                            yield return null;
                        }

                        HideChoicePromt();


                        inputBehaviour.isInteracted = false;
                        //currentInstance.optionListIndex = promptIndex;

                        ShowDialogueInstance(currentInstance.listOfOptions[currentInstance.optionListIndex].dialogueChoiceSubInstances[lastSubIndex]);


                    }
                }

                //if yes, just keep going   (essentially, do nothing)
                //if no/isLoopingThisBranch, check if there's any sub-branches, otherwise loop until yes is pressed

            }
            else
            {
                //Wait for input
                while (!inputBehaviour.isInteracted)
                    yield return null;


                inputBehaviour.isInteracted = false;
            }


            //If action should happen after the dialogue instance
            if (currentInstance.dialogueAction == DialogueAction.AFTER_DIALOGUE)
            {
                HideDialogue();
                if (!dialogueActions.isRunning)
                    dialogueActions.StartCoroutine(currentInstance.actionName);

                while (dialogueActions.isRunning)
                    yield return null;

                ShowDialogueBackground(false);
            }


        }

        EndDialogue();
    }


    private DialogueEditorSerializedNode FindNodeByID(Dictionary<int, DialogueEditorSerializedNode> instanceDictionary, int nodeID)
    {
        DialogueEditorSerializedNode node = null;
        instanceDictionary.TryGetValue(nodeID, out node);
        return node;
    }

    private IEnumerator NodeDialogue(Dictionary<int, DialogueEditorSerializedNode> instanceDictionary, int starterNodeID)
    {
        ShowDialogueBackground();
        //HideDialogue();
        DialogueEditorSerializedNode currentNode = FindNodeByID(instanceDictionary, starterNodeID);

        while (currentNode != null)
        {
            inputBehaviour.isInteracted = false;
            Debug.Log(currentNode.dialogueText);
            ShowDialogueInstance(currentNode);
            

            int nextNodeID;

            if (currentNode.choices.Count == 1)
            {
                nextNodeID = currentNode.choices[0].connectedNodeID;
                while (!inputBehaviour.isInteracted)
                    yield return null;
            }
            else    //If more than one choice, run prompt logic
            {
                ShowChoicePromt(currentNode.choices);

                while (!inputBehaviour.isInteracted)
                    yield return null;


                //if input is given this frame
                nextNodeID = promptIndex;
                HideChoicePromt();
                //inputBehaviour.isInteracted = false;

            }


            if (!instanceDictionary.TryGetValue(nextNodeID, out currentNode))
            {
                currentNode = null;
            }
        }

        EndDialogue();
    }

    private void ShowDialogueInstance(DialogueInstance instanceToShow)
    {
        ConvertDialogueTextToTextUI(instanceToShow.dialogueBox.dialogueText, dialogueText);
        ConvertDialogueTextToTextUI(instanceToShow.dialogueBox.dialogueText, dialogueTextShadow, false);

        dialogueBackground.rectTransform.position = instanceToShow.dialogueBox.boxPosition;
        dialogueBackground.rectTransform.sizeDelta = instanceToShow.dialogueBox.boxSize;


        if (instanceToShow.portrait.isPortraitShown && instanceToShow.portrait.portraitImage != null)
        {
            ShowPortrait(instanceToShow);
        }
        else
        {
            HidePortrait();
        }

    }
    private void ShowDialogueInstance(DialogueEditorSerializedNode nodeToShow)
    {
        dialogueText.text = nodeToShow.dialogueText;
        dialogueTextShadow.text = nodeToShow.dialogueText;

        
        dialogueBackground.rectTransform.position = nodeToShow.dialogueBox.position;
        dialogueBackground.rectTransform.sizeDelta = nodeToShow.dialogueBox.size;

        ShowPortrait(nodeToShow.portrait);
    }

    private void ShowDialogueSubInstance(SubInstance instanceToShow)
    {
        ConvertDialogueTextToTextUI(instanceToShow.dialogueBox.dialogueText, dialogueText);
        ConvertDialogueTextToTextUI(instanceToShow.dialogueBox.dialogueText, dialogueTextShadow, false);
        if (instanceToShow.portrait.isPortraitShown && instanceToShow.portrait.portraitImage != null)
        {
            ShowPortrait(instanceToShow);
            dialoguePortrait.sprite = instanceToShow.portrait.portraitImage;
        }
        else
        {
            HidePortrait();
        }

    }

    private void HideDialogue()
    {
        dialogueText.text = "";
        dialogueTextShadow.text = "";
        dialogueBackground.gameObject.SetActive(false);
        HidePortrait();
    }
    private void EndDialogue()
    {
        Debug.Log("Dialogue Ended!");
        dialogueText.text = "";
        dialogueTextShadow.text = "";
        dialogueBackground.gameObject.SetActive(false);
        HidePortrait();
        inputBehaviour.SwitchToPreviousActionMap();
    }
    private void ShowDialogueBackground(bool switchActionMap = true)
    {
        dialogueBackground.gameObject.SetActive(true);

        if(switchActionMap)
            inputBehaviour.SwitchActionMap("Dialogue");
    }


    void ShowChoicePromt(Vector3 position, int choiceAmount = 0)
    {
        choicePromptContainer.SetActive(true);

        for (int i = 0; i < choiceAmount; i++)
        {
            if (i >= choicePromptContainer.transform.GetChild(0).childCount)
                break;

            choicePromptContainer.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }


        //choicePromptContainer.transform.localPosition = position;
    }
    void ShowChoicePromt(List<SerializedChoice> choices)
    {
        choicePromptContainer.SetActive(true);

        for (int i = 0; i < choices.Count; i++)
        {
            if (i >= choicePromptContainer.transform.GetChild(0).childCount)
                break;

            Button button = choicePromptContainer.transform.GetChild(0).GetChild(i).GetComponent<Button>();

            button.name = choices[i].choiceName;
            button.onClick.RemoveAllListeners();
            int nodeID = choices[i].connectedNodeID;
            button.onClick.AddListener(() => ChoosePrompt(nodeID));

            button.gameObject.SetActive(true);
        }

        //choicePromptContainer.transform.localPosition = position;
    }

    void HideChoicePromt()
    {

        for (int i = 0; i < choicePromptContainer.transform.GetChild(0).childCount; i++)
        {
            choicePromptContainer.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        inputBehaviour.isInteracted = false;
        choicePromptContainer.SetActive(false);
    }

    void ShowPortrait(DialogueInstance instanceToShow)
    {
        dialoguePortraitFrame.SetActive(true);
        dialoguePortraitFrame.transform.position = instanceToShow.portrait.portraitBoxPosition;

        dialoguePortrait.sprite = instanceToShow.portrait.portraitImage;
    }
    void ShowPortrait(PortraitData portraitData)
    {
        if(portraitData.sprite == null)
        {
            HidePortrait();
            return;
        }

        dialoguePortraitFrame.SetActive(true);
        dialoguePortraitFrame.transform.position = portraitData.position;

        dialoguePortrait.sprite = portraitData.sprite;
    }

    //Next Step, fix position and size of dialogue box & portrait

    void ShowPortrait(SubInstance instanceToShow)
    {
        dialoguePortraitFrame.SetActive(true);
        dialoguePortraitFrame.transform.position = instanceToShow.portrait.portraitBoxPosition;

        dialoguePortrait.sprite = instanceToShow.portrait.portraitImage;
    }
    
    void HidePortrait()
    {
        dialoguePortraitFrame.SetActive(false);
    }



    void ConvertDialogueTextToTextUI(DialogueText dialogueTextToConvert, Text textComponent, bool changeFontColor = true)
    {
        
        textComponent.text = dialogueTextToConvert.dialogueString;

        //character
        if(dialogueTextToConvert.dialogueFont != null)
            textComponent.font = dialogueTextToConvert.dialogueFont;
        textComponent.fontStyle = dialogueTextToConvert.dialogueFontStyle;
        if(dialogueTextToConvert.dialogueFontSize > 0)
            textComponent.fontSize = dialogueTextToConvert.dialogueFontSize;
        textComponent.lineSpacing = dialogueTextToConvert.dialogueLineSpacing;
        textComponent.supportRichText = dialogueTextToConvert.dialogueRichText;

        //paragraph
        textComponent.alignment = dialogueTextToConvert.dialogueAlignment;
        textComponent.alignByGeometry = dialogueTextToConvert.dialogueAlignByGeometry;
        textComponent.horizontalOverflow = dialogueTextToConvert.dialogueHorizontalOverflow;
        textComponent.verticalOverflow = dialogueTextToConvert.dialogueVerticalOverflow;
        textComponent.resizeTextForBestFit = dialogueTextToConvert.dialogueBestFit;

        //Standalone
        if(changeFontColor)
            textComponent.color = dialogueTextToConvert.dialogueColor;
        textComponent.material = dialogueTextToConvert.dialogueMaterial;
        textComponent.raycastTarget = dialogueTextToConvert.dialogueRaycastTarget;
        textComponent.raycastPadding = dialogueTextToConvert.dialogueRaycastPadding;
        textComponent.maskable = dialogueTextToConvert.dialogueMaskable;
    }

    public void ChoosePrompt(int returnIndex)
    {
        inputBehaviour.isInteracted = true;
        promptIndex = returnIndex;
    }
    




}