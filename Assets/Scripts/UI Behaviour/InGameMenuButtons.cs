using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameMenuButtons : MonoBehaviour, ISelectHandler, IDeselectHandler
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
    }

    private void OnEnable()
    {
        GetComponent<Canvas>().sortingOrder = 0;
        Button thisButton = GetComponent<Button>();
        if (thisButton == menuHandler.selectedButton)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            //thisButton.Select();
            StartCoroutine(delayedAnimationTrigger(thisButton.animationTriggers.selectedTrigger));
        }
    }

    IEnumerator delayedAnimationTrigger(string animTrigger)
    {
        //yield return new WaitForSeconds(timeDelayed);
        yield return new WaitForEndOfFrame();


        //DOES NOT WORK
        Transform buttonParent = menuHandler.transform.GetChild(0);
        for (int i = 0; i < buttonParent.childCount; i++)
        {
            if (buttonParent.GetChild(i).gameObject == gameObject)
                continue;
            
            
            if(buttonParent.GetChild(i).GetComponent<Canvas>().sortingOrder > 0)
            {
                EventSystem.current.SetSelectedGameObject(buttonParent.GetChild(i).gameObject);
                GetComponent<Canvas>().sortingOrder = 0;
            }
        }
        //Add a check to see if any other button is already selected. If it is, change this animation trigger to normal

        GetComponent<Animator>().SetTrigger(animTrigger);
    }

}
