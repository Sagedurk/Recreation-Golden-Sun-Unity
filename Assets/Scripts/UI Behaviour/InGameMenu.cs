using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameMenu : MonoBehaviour
{
    public Text selectedButtonLabel;
    public Text selectedButtonLabelShadow;
    public Button selectedButton;

    public void Psynergy()
    {
        Debug.Log("Psynergy");
    }   
    public void Djinn()
    {
        Debug.Log("Djinn");
    }   
    public void Item()
    {
        Debug.Log("Item");
    }   
    public void Status()
    {
        Debug.Log("Status");
    }

    private void OnEnable()
    {
        Transform buttonsParent = transform.GetChild(0);
        for (int i = 0; i < buttonsParent.childCount; i++)
        {
            Transform button = buttonsParent.GetChild(i);
            button.localScale = Vector3.one;
        }

        //The button animation gets reset after this
        selectedButton = buttonsParent.GetChild(0).GetComponent<Button>();
    }

}
