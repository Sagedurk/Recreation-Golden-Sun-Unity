using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Text selectedButtonLabel;
    public Text selectedButtonLabelShadow;

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
            Debug.Log(button.name + "onEnable");
            button.localScale = Vector3.one;
 
        }
        buttonsParent.GetChild(0).GetComponent<Button>().Select();
    }

}
