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
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("listOfDialogueBoxes"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("eventDialogue"));

                List<DialogueMaster.DialogueInstance> dialogueInstanceList = interactable.listOfDialogueBoxes;



                for (int i = 0; i < interactable.listOfDialogueBoxes.Count; i++)
                {
                    DialogueMaster.DialogueInstance dialogueInstance = interactable.listOfDialogueBoxes[i];


                    if (dialogueInstance.dialogueChoice == DialogueMaster.dialogueChoices.ACTIVE)
                    {

                    }

                }


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
