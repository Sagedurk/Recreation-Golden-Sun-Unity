using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNodePreview : MonoBehaviour
{

    VisualElement rootElement;
    DialogueBox boxPreview = new DialogueBox();



    public class DialogueBox
    {
        public TextField textField;
        public bool isShadowed;

        public void CreateBoxPreview(string dialogueText)
        {
            textField = DialogueElementUtility.CreateTextArea(dialogueText, null, callback =>
            {
                //UnityEditor.EditorApplication.delayCall += () =>
                //{
                //    VisualElement textElement = textField;
                //    DialogueElementUtility.SetPositionInRelationToParent(ref textElement, (DialogueElementUtility.Alignment)positionDropdown.index);
                //};

            });

        }
    }

    public class DialoguePortrait
    {
        public VisualElement portrait;
        public VisualElement frame;
    }
}
