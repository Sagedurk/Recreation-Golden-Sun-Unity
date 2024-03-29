using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialogueMasterSaveData : ScriptableObject
{
    //[HideInInspector]
    public DialogueMasterElements dialogueMasterElements;

    static string folderName = "Data";
    static string assetName = "Diamaster";
    static string resourceFolderName = "Resources";

    
    public static void Save()
    {
#if UNITY_EDITOR
        DialogueMasterSaveData saveData = CreateInstance<DialogueMasterSaveData>();

        saveData.dialogueMasterElements = DialogueMasterElements.Instance;


        if (!AssetDatabase.IsValidFolder("Assets/" + resourceFolderName))
            AssetDatabase.CreateFolder("Assets", resourceFolderName);

        if (!AssetDatabase.IsValidFolder("Assets/" + resourceFolderName + "/" + folderName))
            AssetDatabase.CreateFolder("Assets/" + resourceFolderName, folderName);

        AssetDatabase.CreateAsset(saveData, "Assets/" + resourceFolderName + "/" + folderName + "/" + assetName + ".asset");
        AssetDatabase.SaveAssets();
#endif
    }

    public static void Load()
    {
        if (DialogueMasterElements.Instance != null)
            return;

        DialogueMasterSaveData loadedData = Resources.Load<DialogueMasterSaveData>(folderName + "/" + assetName);
        Debug.Log(loadedData);


        if (loadedData == null)
            return;

        DialogueMasterElements.Instance = loadedData.dialogueMasterElements;
    }

}