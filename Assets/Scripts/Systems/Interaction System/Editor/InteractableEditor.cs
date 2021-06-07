using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor 
{

    List<bool> listFoldouts = new List<bool>();

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
                        while(listFoldouts.Count < i + 1)
                        {
                            listFoldouts.Add(false);
                        }

                        SerializedProperty dialogueInstance = dialogueList.GetArrayElementAtIndex(i);

                        SerializedProperty dialogueBox = dialogueInstance.FindPropertyRelative("dialogueBox");    
                        SerializedProperty portrait = dialogueInstance.FindPropertyRelative("portrait");
                        SerializedProperty dialogueChoice = dialogueInstance.FindPropertyRelative("dialogueChoice");
                        SerializedProperty dialogueSubInstance = dialogueInstance.FindPropertyRelative("subInstances");




                        List<DialogueMaster.choice> listOfSubInstances = interactable.listOfDialogueBoxes[i].subInstances;




                        listFoldouts[i] = EditorGUILayout.Foldout(listFoldouts[i], "Element" + i.ToString());
                       


                    //If instance is shown 
                        if(listFoldouts[i])
                        {
                            EditorGUI.indentLevel = 1;
                            EditorGUILayout.PropertyField(dialogueBox);
                            EditorGUILayout.PropertyField(portrait);
                            EditorGUILayout.PropertyField(dialogueChoice);

                            if (dialogueChoice.enumValueIndex == (int)DialogueMaster.dialogueChoices.ACTIVE)
                            {

                                EditorGUILayout.PropertyField(dialogueSubInstance);
                            //add code here for showing dialogue option list
                                for (int j = 0; j < listOfSubInstances.Count; j++)
                                {
                                    

                                }
                                    

                                Debug.Log("DIALOGUE CHOICE #" + i.ToString() + " IS ACTIVE!");
                            }
                            
                            EditorGUI.indentLevel = 0;
                        }

                    }

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("");

                    GUIStyle buttonStyle = new GUIStyle("button");
                    buttonStyle.fontSize = 19;
                    buttonStyle.fontStyle = FontStyle.Bold;
                    buttonStyle.alignment = TextAnchor.MiddleCenter;

                    if (GUILayout.Button("+", buttonStyle, GUILayout.Width(23), GUILayout.Height(23)))
                    {
                        interactable.listOfDialogueBoxes.Add(new DialogueMaster.DialogueInstance());
                    }

                    if (GUILayout.Button("-", buttonStyle, GUILayout.Width(23), GUILayout.Height(23)))
                    {
                        //Remove Last Element
                        interactable.listOfDialogueBoxes.RemoveAt(interactable.listOfDialogueBoxes.Count - 1);
                        listFoldouts.RemoveAt(listFoldouts.Count - 1);
                    }
                    EditorGUILayout.EndHorizontal();


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
