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
        GetComponent<Canvas>().sortingOrder = 0;
        Button thisButton = GetComponent<Button>();
        if (thisButton == menuHandler.selectedButton)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            thisButton.Select();
            StartCoroutine(delayedAnimationTrigger(0.0001f, thisButton.animationTriggers.selectedTrigger));
        }
    }

    IEnumerator delayedAnimationTrigger(float timeDelayed, string animTrigger)
    {
        //yield return new WaitForSeconds(timeDelayed);
        yield return new WaitForEndOfFrame();

        //Add a check to see if any other button is already selected. If it is, change this animation trigger to normal

        GetComponent<Animator>().SetTrigger(animTrigger);
    }

}
