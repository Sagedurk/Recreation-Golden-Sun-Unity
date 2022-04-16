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

    int promptIndex;
    int lastSubIndex;

    [System.Serializable]
    public class DialogueInstance{

        //dialogue box
        public DialogueBox dialogueBox;
        
        //portrait
        public CharacterPortrait portrait;

        //dialogue options
        public Prompt dialoguePrompt;

        public List<string> listOfOptionNames = new List<string>();
        public List<choice> listOfOptions = new List<choice>();
        public int optionListIndex = 0; 
    }

    [System.Serializable]
    public class choice
    {
        public List<SubInstance> dialogueChoiceSubInstances = new List<SubInstance>();
        public bool isLoopingLastSubInstance = false;
        
    }
    [System.Serializable]
    public class Prompt
    {
        public Vector3 position = new Vector3(0, 0, 0);
        public dialogueChoices dialogueChoice;
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


    public enum dialogueChoices
    {
        INACTIVE,
        ACTIVE,
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DebugAllDialogue(List<DialogueInstance> instanceList)
    {
        StartCoroutine(Dialogue(instanceList));
    }

    private IEnumerator Dialogue(List<DialogueInstance> instanceList)
    {
        ShowDialogueBackground();
        for (int i = 0; i < instanceList.Count; i++)
        {
            DialogueInstance currentInstance = instanceList[i];

            ShowDialogueInstance(currentInstance);

            if (currentInstance.dialoguePrompt.dialogueChoice == dialogueChoices.ACTIVE)
            {
                ShowChoicePromt(currentInstance.dialoguePrompt.position);


                while (!inputBehaviour.isInteracted)
                {

                    yield return null;
                }

                HideChoicePromt();
                inputBehaviour.isInteracted = false;


                lastSubIndex = -1;
                currentInstance.optionListIndex = promptIndex;


                for (int k = 0; k < currentInstance.listOfOptions[currentInstance.optionListIndex].dialogueChoiceSubInstances.Count; k++)
                {
                    ShowDialogueSubInstance(currentInstance.listOfOptions[currentInstance.optionListIndex].dialogueChoiceSubInstances[k]);
                    lastSubIndex++;

                    while (!inputBehaviour.isInteracted)
                    {

                        yield return null;
                    }

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

                        ShowDialogueSubInstance(currentInstance.listOfOptions[currentInstance.optionListIndex].dialogueChoiceSubInstances[lastSubIndex]);
                    

                    }   
                }

                //if yes, just keep going   (essentially, do nothing)
                //if no/isLoopingThisBranch, check if there's any sub-branches, otherwise loop until yes is pressed

            }
            else
            {
                while (!inputBehaviour.isInteracted)
                {

                    yield return null;
                }

                inputBehaviour.isInteracted = false;
            }
        }

        HideDialogue();
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
        inputBehaviour.SwitchToPreviousActionMap();
    }
    private void ShowDialogueBackground()
    {
        dialogueBackground.gameObject.SetActive(true);


        inputBehaviour.SwitchActionMap("Dialogue");
    }


    void ShowChoicePromt(Vector3 position)
    {
        choicePromptContainer.SetActive(true);
        choicePromptContainer.transform.localPosition = position;
    }
    
    void HideChoicePromt()
    {

        inputBehaviour.isInteracted = false;
        choicePromptContainer.SetActive(false);
    }

    void ShowPortrait(DialogueInstance instanceToShow)
    {
        dialoguePortraitFrame.SetActive(true);
        dialoguePortraitFrame.transform.position = instanceToShow.portrait.portraitBoxPosition;

        dialoguePortrait.sprite = instanceToShow.portrait.portraitImage;
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