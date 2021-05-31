using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Interactable interactable = (Interactable)target;

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Interaction Type", GUILayout.Width(100));
            interactable.interactionType = (Interactable.InteractionType)EditorGUILayout.EnumPopup(interactable.interactionType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

            switch (interactable.interactionType)
            {
                case Interactable.InteractionType.DIALOGUE:

                    SerializedProperty dialogueList = serializedObject.FindProperty("listOfDialogueBoxes");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("List Of Dialogue Boxes", GUILayout.MinWidth(150));
                    int listCount = EditorGUILayout.IntField(interactable.listOfDialogueBoxes.Count, GUILayout.Width(50));
                    EditorGUILayout.EndHorizontal();

                    for (int i = 0; i < interactable.listOfDialogueBoxes.Count; i++)
                    {
                        SerializedProperty dialogueInstance = dialogueList.GetArrayElementAtIndex(i);
                        
                        //Create Property Drawer and add dialogueInstance variables inside
                        
                        EditorGUILayout.PropertyField(dialogueInstance);

                    }

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add"))
                {
                    interactable.listOfDialogueBoxes.Add(new DialogueMaster.DialogueInstance());
                }
                if (GUILayout.Button("Remove"))
                {
                    //Remove Last Element
                    interactable.listOfDialogueBoxes.RemoveAt(interactable.listOfDialogueBoxes.Count - 1);
                }
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("eventDialogue"));
                    serializedObject.ApplyModifiedProperties();


                    break;

                case Interactable.InteractionType.CHEST:
                    Debug.Log("Chest");
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


}
