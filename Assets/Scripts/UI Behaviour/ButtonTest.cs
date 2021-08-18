using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public InGameMenu menuHandler;
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<Canvas>().sortingOrder = 1;
        menuHandler.selectedButtonLabel.text = name;
        menuHandler.selectedButtonLabelShadow.text = name;

        GetComponent<Animator>().SetTrigger("Selected");
    }    
    public void OnDeselect(BaseEventData eventData)
    {
       GetComponent<Canvas>().sortingOrder = 0;
    }

    private void OnEnable()
    {
        Button thisButton = GetComponent<Button>();
        if (thisButton == menuHandler.selectedButton)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            thisButton.Select();
            GetComponent<Animator>().SetTrigger("Selected");
        }
    }
}
