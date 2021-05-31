using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueInstance))]
public class DialogueChainEditor : Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        DialogueInstance instance = (DialogueInstance)target;

        SerializedProperty dialogueInstance = serializedObject.FindProperty("dialogueInstance");
        SerializedProperty dialogueBox = dialogueInstance.FindPropertyRelative("dialogueBox");
        SerializedProperty CharacterPortrait = serializedObject.FindProperty("portrait");
        SerializedProperty dialogueChoice = serializedObject.FindProperty("dialogueChoice");

        dialogueChoice = (DialogueMaster.dialogueChoices)EditorGUILayout.EnumPopup(dialogueChoice);

    }

}
