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

    private int promptIndex;


    // Start is called before the first frame update
    void Start()
    {
        //ShowChoicePromt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DebugAllNodes(Dictionary<int, DialogueEditorSerializedNode> instanceDictionary, int starterNodeID)
    {
        StartCoroutine(NodeDialogue(instanceDictionary, starterNodeID));
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

    private void ShowDialogueInstance(DialogueEditorSerializedNode nodeToShow)
    {
        dialogueText.text = nodeToShow.dialogueText;
        dialogueTextShadow.text = nodeToShow.dialogueText;

        
        dialogueBackground.rectTransform.sizeDelta = nodeToShow.dialogueBox.size;
        dialogueBackground.rectTransform.anchoredPosition = nodeToShow.dialogueBox.position;


        ShowPortrait(nodeToShow.portrait);
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


    private void ShowChoicePromt(List<SerializedChoice> choices)
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

    private void HideChoicePromt()
    {

        for (int i = 0; i < choicePromptContainer.transform.GetChild(0).childCount; i++)
        {
            choicePromptContainer.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        inputBehaviour.isInteracted = false;
        choicePromptContainer.SetActive(false);
    }

    private void ShowPortrait(PortraitData portraitData)
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

    
    private void HidePortrait()
    {
        dialoguePortraitFrame.SetActive(false);
    }

    public void ChoosePrompt(int returnIndex)
    {
        inputBehaviour.isInteracted = true;
        promptIndex = returnIndex;
    }
    


}