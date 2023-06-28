using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor 
{

    List<bool> listFoldouts = new List<bool>();
    List<int> selectedOption = new List<int>();


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Interactable interactable = (Interactable)target;

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Interaction Type", GUILayout.Width(100));
            interactable.interactionType = (Interactable.InteractionType)EditorGUILayout.EnumPopup(interactable.interactionType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(15.0f);

            switch (interactable.interactionType)
            {
                case Interactable.InteractionType.DIALOGUE:

                    OnDialogue(interactable);

                break;

                case Interactable.InteractionType.CHEST:

                    SerializedProperty chestInstance = serializedObject.FindProperty("chestInstance");
                    SerializedProperty chestState = chestInstance.FindPropertyRelative("chestState");
                    EditorGUILayout.PropertyField(chestInstance);

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("eventChest"));
                    serializedObject.ApplyModifiedProperties();

                break;

                case Interactable.InteractionType.SHOP:
                    Debug.Log("Shop");
                    break;

                case Interactable.InteractionType.DJINN:
                    Debug.Log("Djinn");
                    break;
            }
        

        EditorGUILayout.Space();
    }


    void OnDialogue(Interactable interactable)
    {
        if (GUILayout.Button("Open Dialogue Editor"))
        {
            DialogueMasterWindow.dialogueInstance = interactable;
            DialogueMasterWindow.ShowWindow();
        }

        SerializedProperty dialogueList = serializedObject.FindProperty("listOfDialogueBoxes");

        EditorGUILayout.Space(10.0f);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("nodeEventDialogue"));
        serializedObject.ApplyModifiedProperties();
    }

}
