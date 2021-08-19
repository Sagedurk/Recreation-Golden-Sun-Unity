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
                        while(selectedOption.Count < i + 1)
                        {
                            selectedOption.Add(0);
                        }


                        SerializedProperty dialogueInstance = dialogueList.GetArrayElementAtIndex(i);

                        SerializedProperty dialogueBox = dialogueInstance.FindPropertyRelative("dialogueBox");    
                        SerializedProperty portrait = dialogueInstance.FindPropertyRelative("portrait");
                        SerializedProperty dialogueChoice = dialogueInstance.FindPropertyRelative("dialogueChoice");
                        SerializedProperty dialogueOptionList = dialogueInstance.FindPropertyRelative("listOfOptions");
                        SerializedProperty dialogueOptionNames = dialogueInstance.FindPropertyRelative("listOfOptionNames");
                        SerializedProperty dialogueOptionIndex = dialogueInstance.FindPropertyRelative("optionListIndex");



                        List<string> listOfOptionNames = interactable.listOfDialogueBoxes[i].listOfOptionNames;
                        List<DialogueMaster.choice> listOfOptions = interactable.listOfDialogueBoxes[i].listOfOptions;




                        listFoldouts[i] = EditorGUILayout.Foldout(listFoldouts[i], "Dialogue #" + (i + 1).ToString());
                       


                        //If instance is shown 
                        if(listFoldouts[i])
                        {
                            EditorGUI.indentLevel = 1;
                            EditorGUILayout.PropertyField(dialogueBox);
                            EditorGUILayout.PropertyField(portrait);
                            EditorGUILayout.PropertyField(dialogueChoice);

                            if (dialogueChoice.enumValueIndex == (int)DialogueMaster.dialogueChoices.ACTIVE)
                            {
                                EditorGUI.indentLevel = 3;
                                EditorGUILayout.PropertyField(dialogueOptionNames); //Make an option with images

                                EditorGUILayout.PropertyField(dialogueOptionList);  //Make this into a custom ListOfOptions drawer??


                                if(listOfOptionNames.Count > 0)
                                {
                                    GUIContent[] contents = new GUIContent[listOfOptionNames.Count];
                                    for (int j = 0; j < contents.Length; j++)
                                    {
                                        contents[j] = new GUIContent();
                                        contents[j].text = listOfOptionNames[j];
                                    }

                                    selectedOption[i] = EditorGUILayout.Popup(selectedOption[i], contents);

                                    dialogueOptionIndex.intValue = selectedOption[i];
                                    serializedObject.ApplyModifiedProperties();
                                    
                                    SerializedProperty subInstances = dialogueOptionList.GetArrayElementAtIndex(selectedOption[i]);
                                    SerializedProperty subInstanceList = subInstances.FindPropertyRelative("dialogueChoiceSubInstances");

                                    for (int j = 0; j < listOfOptions[selectedOption[i]].dialogueChoiceSubInstances.Count; j++)
                                    {
                                        
                                        //Evolve This 

                                        SerializedProperty subInstance = subInstanceList.GetArrayElementAtIndex(j);
                                        SerializedProperty subBox = subInstance.FindPropertyRelative("dialogueBox");
                                        SerializedProperty subPortrait = subInstance.FindPropertyRelative("portrait");
                                        
                                        EditorGUILayout.PropertyField(subBox);
                                        EditorGUILayout.PropertyField(subPortrait);
                                    }


                                }
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

                    SerializedProperty chestMaster = serializedObject.FindProperty("chestMaster");
                    EditorGUILayout.PropertyField(chestMaster);
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


}
