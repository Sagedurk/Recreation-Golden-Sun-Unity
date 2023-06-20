using UnityEditor;
using UnityEngine;

public class DialogueEditorCurrentInstanceSO : ScriptableObject
{
    public string instanceName;

    static string folderName = "Data";
    static string assetName = "DialogueEditorInstance";
    static string resourceFolderName = "Resources";


    public static string TryGetInstanceName()
    {
        DialogueEditorCurrentInstanceSO instance = Load();

        if (instance == null)
            return "";

        return instance.instanceName;
    }

    private static DialogueEditorCurrentInstanceSO Load()
    {
        DialogueEditorCurrentInstanceSO loadedData = Resources.Load<DialogueEditorCurrentInstanceSO>(folderName + "/" + assetName);
        return loadedData;
    }

    public static void Save(string backupName)
    {
        DialogueEditorCurrentInstanceSO saveData = CreateInstance<DialogueEditorCurrentInstanceSO>();

        saveData.instanceName = backupName;


        if (!AssetDatabase.IsValidFolder("Assets/" + resourceFolderName))
            AssetDatabase.CreateFolder("Assets", resourceFolderName);

        if (!AssetDatabase.IsValidFolder("Assets/" + resourceFolderName + "/" + folderName))
            AssetDatabase.CreateFolder("Assets/" + resourceFolderName, folderName);

        AssetDatabase.CreateAsset(saveData, "Assets/" + resourceFolderName + "/" + folderName + "/" + assetName + ".asset");
        AssetDatabase.SaveAssets();
    }

}
