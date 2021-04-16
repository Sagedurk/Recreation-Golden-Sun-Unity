using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public InGameMenu menuHandler;
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<Canvas>().sortingOrder = 1;
        menuHandler.selectedButtonLabel.text = name;
        menuHandler.selectedButtonLabelShadow.text = name;
    }    
    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Canvas>().sortingOrder = 0;
        //transform.localScale = Vector3.one;
    }
}
