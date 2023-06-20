using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections;
using System;
using System.IO;
using UnityEditor.UIElements;
using static DialogueMaster;

public class DialogueMasterWindow : EditorWindow
{
    public static Interactable dialogueInstance;
    

    private Button saveButton;
    private Button loadButton;
    private DialogueMasterGraphView graphView;

    private string latestInstanceName;

    private Toolbar toolbar;
    StyleLength toolbarSize = 40;
    
    //Side Menu
    private Button sideMenuButton;
    private bool isSideMenuOpen = false;
    private VisualElement sideMenu;


    public static void ShowWindow()
    {
        DialogueMasterWindow window = GetWindow<DialogueMasterWindow>("Dialogue Master");


        window.rootVisualElement.Clear();
        window.CreateGUI();

        if (dialogueInstance != null)
            window.LoadAssetData(dialogueInstance.gameObject.scene.name + "_" + dialogueInstance.name);

    }

    private void CreateGUI()
    {
        
            DialogueMasterSaveData.Load();

            AddGraphView();
            AddSideMenu();
            AddToolbar();

            latestInstanceName = DialogueEditorCurrentInstanceSO.TryGetInstanceName();

            if (latestInstanceName != "")
                LoadAssetData(latestInstanceName);

        //AddStyles();
    }


    #region Elements Addition
    private void AddGraphView()
    {
        graphView = new DialogueMasterGraphView(this);
        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void AddSideMenu()
    {
        /*BACKGROUND*/
        #region BACKGROUND

        sideMenu = new VisualElement();
        rootVisualElement.Add(sideMenu);

        sideMenu.StretchToParentSize();
        sideMenu.style.width = 250;
        sideMenu.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1);   //dark grey

        VisualElement sideMenuMargin = new VisualElement();
        sideMenu.Add(sideMenuMargin);

        sideMenuMargin.StretchToParentSize();
        Vector4 margins = new Vector4(25, 0, 25, toolbarSize.value.value + 25);
        DialogueElementUtility.SetStyleMargin(ref sideMenuMargin, margins);
        #endregion

        /*ELEMENTS*/
        #region ELEMENTS

        


        //Background Image
        ObjectField dialogueBackgroundSprite = DialogueElementUtility.CreateObjectField(callback =>
        {
            DialogueMasterElements.Instance.dialogueBackground = (Sprite)callback.newValue;
            DialogueMasterSaveData.Save();

        }, DialogueMasterElements.Instance.dialogueBackground); 

        
        //Font Type
        ObjectField font = DialogueElementUtility.CreateObjectField(callback =>
        {
            DialogueMasterElements.Instance.font = (Font)callback.newValue;
            DialogueMasterSaveData.Save();

        }, DialogueMasterElements.Instance.font);


        //Font Color
        Label fontColorLabel = new Label("Font Color: ");
        ColorField fontColor = DialogueElementUtility.CreateColorField(DialogueMasterElements.Instance.fontColor, callback => 
        {
            DialogueMasterElements.Instance.fontColor = callback.newValue;
            DialogueMasterSaveData.Save();
        });


        //Font Shadow Color
        Label fontShadowColorLabel = new Label("Font Shadow Color: ");
        ColorField fontShadowColor = DialogueElementUtility.CreateColorField(DialogueMasterElements.Instance.fontShadowColor, callback =>
        {
            DialogueMasterElements.Instance.fontShadowColor = callback.newValue;
            DialogueMasterSaveData.Save();
        });

        //Font Shadow Direction
        Vec2VE fontShadowDirection = new Vec2VE();
        fontShadowDirection.vector = DialogueMasterElements.Instance.fontShadowDir;
        fontShadowDirection.InitializeNormalized("Font Shadow Direction", true, Callback => 
        {
            DialogueMasterElements.Instance.fontShadowDir = fontShadowDirection.vector;
            DialogueMasterSaveData.Save();
        });


        //Font Shadow Magnitude
        TextField shadowMagnitudeTextField = DialogueElementUtility.CreateNumField(DialogueMasterElements.Instance.fontShadowMag, "Shadow Magnitude: ", callback =>
        {
            DialogueElementUtility.RemoveCharactersNaN(callback, out DialogueMasterElements.Instance.fontShadowMag);
            DialogueMasterSaveData.Save();
        });

        //Font Shadow Toggle
        Toggle fontShadowToggle = DialogueElementUtility.CreateToggle(DialogueMasterElements.Instance.isShadowed ,"Drop Shadow", callback => 
        {
            fontShadowColor.style.display = (DisplayStyle)Convert.ToInt32(!callback.newValue);
            fontShadowColorLabel.style.display = (DisplayStyle)Convert.ToInt32(!callback.newValue);
            fontShadowDirection.foldout.style.display = (DisplayStyle)Convert.ToInt32(!callback.newValue);
            shadowMagnitudeTextField.style.display = (DisplayStyle)Convert.ToInt32(!callback.newValue);

            DialogueMasterElements.Instance.isShadowed = callback.newValue;

            DialogueMasterSaveData.Save();
        });

        
        //Font Size
        TextField fontSizeTextField = DialogueElementUtility.CreateNumField(DialogueMasterElements.Instance.fontSize, "Font Size: ", callback => 
        {
            DialogueElementUtility.RemoveCharactersNaN(callback, out DialogueMasterElements.Instance.fontSize);
            DialogueMasterSaveData.Save();
        });

        //Font Margins
        MarginsVE fontMargins = new MarginsVE();
        fontMargins.SetValues(DialogueMasterElements.Instance.fontMargins);
        fontMargins.Initialize("Font Margins", xCallback => 
        {
            DialogueMasterElements.Instance.fontMargins = fontMargins.margins;
            DialogueMasterSaveData.Save();
        });


       

        //Top - Bottom order
        sideMenuMargin.Add(dialogueBackgroundSprite);
        sideMenuMargin.Add(font);
        sideMenuMargin.Add(fontMargins.foldout);
        sideMenuMargin.Add(fontSizeTextField);
        sideMenuMargin.Add(fontColorLabel);
        sideMenuMargin.Add(fontColor);


        sideMenuMargin.Add(fontShadowColorLabel);
        sideMenuMargin.Add(fontShadowColor);
        sideMenuMargin.Add(shadowMagnitudeTextField);
        sideMenuMargin.Add(fontShadowDirection.foldout);
        sideMenuMargin.Add(fontShadowToggle);



        DialogueMasterSaveData.Save();
        #endregion

        //Side menu is by default hidden
        sideMenu.visible = false;
    }

    private void AddToolbar()
    {
        toolbar = new Toolbar();

        string instanceName = "INSTANCE NOT FOUND";
        string sceneName = "";

        if (dialogueInstance != null)
        {
            sceneName = dialogueInstance.gameObject.scene.name;
            instanceName = sceneName + "_" + dialogueInstance.name;
        }
        else
            instanceName = latestInstanceName;
        


        Label instanceLabel = new Label("Asset Name: " + instanceName);


        saveButton = DialogueElementUtility.CreateButton("Save Data", () =>
        {
            if (dialogueInstance == null && latestInstanceName == "")
            {
                Debug.LogError("Instance not found, can't save data!");
                return;
            }

            string path = GetAssetFilePath(this) + "/SaveData/";
            graphView.SaveGraphViewData(instanceName, path);
        });


        loadButton = DialogueElementUtility.CreateButton("Load Data", () =>
        {
            if (dialogueInstance == null && latestInstanceName == "")
            {
                Debug.LogError("Instance not found, can't load data!");
                return;
            }

            LoadAssetData(instanceName);

        });

        sideMenuButton = DialogueElementUtility.CreateButton("Open Side Menu", () =>
        {
            ToggleSideMenu();

        });

        toolbar.Add(instanceLabel);
        //toolbar.Add(fileNameTextField);
        toolbar.Add(saveButton);
        toolbar.Add(loadButton);
        toolbar.Add(sideMenuButton);
        instanceLabel.AddClasses("Window-toolbar-label");
        toolbar.AddStyleSheets("Dialogue SageSys/DialogueToolbarStyles.uss");

        rootVisualElement.Add(toolbar);

        toolbar.style.height = toolbarSize;
    }

    void LoadAssetData(string instanceName)
    {
        string path = GetAssetFilePath(this) + "/SaveData/";
        graphView.LoadGraphViewData(instanceName, path);

        DialogueEditorCurrentInstanceSO.Save(instanceName);
    }

 
    void ToggleSideMenu()
    {
        isSideMenuOpen = !isSideMenuOpen;

        switch (isSideMenuOpen)
        {
            //Side Menu is open
            case true:
                sideMenuButton.text = "Close Side Menu";

                break;

            //Side Menu is closed
            case false:
                sideMenuButton.text = "Open Side Menu";

                break;
        }


        sideMenu.visible = isSideMenuOpen;
    }


    #endregion


    #region Utility Methods
    public void EnableSaving()
    {
        saveButton.SetEnabled(true);
    }

    public void DisableSaving()
    {
        saveButton.SetEnabled(false);
    }
    private void AddStyles()
    {
        rootVisualElement.AddStyleSheets("Dialogue SageSys/DialogueVariables.uss");

    }

    #endregion


    #region IO

    string GetAssetFilePath(ScriptableObject asset)
    {
        MonoScript ms = MonoScript.FromScriptableObject(asset);
        string scriptFilePath = AssetDatabase.GetAssetPath(ms);
        string folderPath = scriptFilePath.Remove(scriptFilePath.Length - 1 - (ms.name + ".cs").Length);
        return folderPath;
    }

    #endregion

}

