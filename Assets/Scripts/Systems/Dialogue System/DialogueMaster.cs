using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueMaster : MonoBehaviour
{

    public List<GameObject> choicePromptContainers = new List<GameObject>();
    public InputBehaviour inputBehaviour;
    public Image dialogueBackground;
    public Image dialoguePortraitFrame;

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
                ShowChoicePromt(currentNode.choices, currentNode.choicePromptIndex);

                while (!inputBehaviour.isInteracted)
                    yield return null;


                //if input is given this frame
                nextNodeID = promptIndex;
                HideChoicePromt(currentNode.choicePromptIndex);
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


    private void ShowChoicePromt(List<SerializedChoice> choices, int index)
    {
        choicePromptContainers[index].SetActive(true);

        for (int i = 0; i < choices.Count; i++)
        {
            if (i >= choicePromptContainers[index].transform.GetChild(0).childCount)
                break;

            Button button = choicePromptContainers[index].transform.GetChild(0).GetChild(i).GetComponent<Button>();

            button.name = choices[i].choiceName;
            button.onClick.RemoveAllListeners();
            int nodeID = choices[i].connectedNodeID;
            button.onClick.AddListener(() => ChoosePrompt(nodeID));

            button.gameObject.SetActive(true);
        }

        //choicePromptContainer.transform.localPosition = position;
    }

    private void HideChoicePromt(int index)
    {

        for (int i = 0; i < choicePromptContainers[index].transform.GetChild(0).childCount; i++)
        {
            choicePromptContainers[index].transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        inputBehaviour.isInteracted = false;
        choicePromptContainers[index].SetActive(false);
    }

    private void ShowPortrait(PortraitData portraitData)
    {
        if(portraitData.sprite == null)
        {
            HidePortrait();
            return;
        }

        dialoguePortraitFrame.gameObject.SetActive(true);
        dialoguePortraitFrame.rectTransform.anchoredPosition = portraitData.position;

        dialoguePortrait.sprite = portraitData.sprite;
    }

    //Next Step, fix position and size of dialogue box & portrait

    
    private void HidePortrait()
    {
        dialoguePortraitFrame.gameObject.SetActive(false);
    }

    public void ChoosePrompt(int returnIndex)
    {
        inputBehaviour.isInteracted = true;
        promptIndex = returnIndex;
    }
    


}