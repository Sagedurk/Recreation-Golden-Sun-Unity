using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMaster : MonoBehaviour
{

    Text textToShow;

    [System.Serializable]
    public struct DialogueInstance{

        //dialogue box
        public DialogueBox dialogue;
        
        //portrait
        public CharacterPortrait portrait;

        //dialogue options
        public dialogueChoices dialogueChoice;
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
        public Image portraitImage;
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
        public Font dialogueFont;
        public FontStyle dialogueFontStyle;
        public int dialogueFontSize;
        public float dialogueLineSpacing;
        public bool dialogueRichText;

        [Header("Paragraph")]
        public TextAnchor dialogueAlignment;
        public bool dialogueAlignByGeometry;
        public HorizontalWrapMode dialogueHorizontalOverflow;
        public VerticalWrapMode dialogueVerticalOverflow;
        public bool dialogueBestFit;

        [Header("")]
        public Color dialogueColor;
        public Material dialogueMaterial;
        public bool dialogueRaycastTarget;
        public Vector4 dialogueRaycastPadding;
        public bool dialogueMaskable;

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
        for (int i = 0; i < instanceList.Count; i++)
        {
            DialogueInstance currentInstance = instanceList[i];

            Debug.Log(currentInstance.dialogue.dialogueText.dialogueString);
        }
    }


    void ConvertDialogueTextToTextUI(DialogueText dialogueText)
    {
        textToShow.text = dialogueText.dialogueString;

        //character
        textToShow.font = dialogueText.dialogueFont;
        textToShow.fontStyle = dialogueText.dialogueFontStyle;
        textToShow.fontSize = dialogueText.dialogueFontSize;
        textToShow.lineSpacing = dialogueText.dialogueLineSpacing;
        textToShow.supportRichText = dialogueText.dialogueRichText;

        //paragraph
        textToShow.alignment = dialogueText.dialogueAlignment;
        textToShow.alignByGeometry = dialogueText.dialogueAlignByGeometry;
        textToShow.horizontalOverflow = dialogueText.dialogueHorizontalOverflow;
        textToShow.verticalOverflow = dialogueText.dialogueVerticalOverflow;
        textToShow.resizeTextForBestFit = dialogueText.dialogueBestFit;

        //Standalone
        textToShow.color = dialogueText.dialogueColor;
        textToShow.material = dialogueText.dialogueMaterial;
        textToShow.raycastTarget = dialogueText.dialogueRaycastTarget;
        textToShow.raycastPadding = dialogueText.dialogueRaycastPadding;
        textToShow.maskable = dialogueText.dialogueMaskable;
    }

}
