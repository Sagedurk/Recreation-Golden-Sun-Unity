using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueInstance))]
public class DialogueChainEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //DialogueInstance instance = (DialogueInstance)target;

        //SerializedProperty dialogueInstance = serializedObject.FindProperty("dialogueInstance");
        //SerializedProperty dialogueBox = dialogueInstance.FindPropertyRelative("dialogueBox");
        //SerializedProperty CharacterPortrait = dialogueInstance.FindPropertyRelative("portrait");
        //SerializedProperty dialogueChoice = dialogueInstance.FindPropertyRelative("dialogueChoice");


        //if(dialogueChoice.enumValueIndex == 1)
        //{
        //    dialogueChoice.enumValueIndex = (int)(DialogueMaster.dialogueChoices)EditorGUILayout.EnumPopup
        //        ("Dialogue Choices", (DialogueMaster.dialogueChoices)System.Enum.GetValues(typeof(DialogueMaster.dialogueChoices)).GetValue(dialogueChoice.enumValueIndex));

        //}
        
    }

}
