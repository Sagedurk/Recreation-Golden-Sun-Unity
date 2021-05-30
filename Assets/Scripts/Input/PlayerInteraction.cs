using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform player;
    Interactable interactableTriggerEnter = null;
    Interactable interactableTriggerExit = null;

    List<Interactable> listOfInteractables = new List<Interactable>();
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = transform.parent;
    }

    private void OnTriggerEnter(Collider obj)
    {
        interactableTriggerEnter = obj.GetComponent<Interactable>();

        if(interactableTriggerEnter != null)
        {
            listOfInteractables.Add(interactableTriggerEnter);
            interactableTriggerEnter = null;
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        interactableTriggerExit = obj.GetComponent<Interactable>();

        if (interactableTriggerExit != null)
        {
            listOfInteractables.Remove(interactableTriggerExit);
            interactableTriggerExit = null;
        }
    }



    /// <summary>
    /// <u><b>Check if there's something nearby, to the left or right, to interact with</b></u>
    /// <br />
    /// If there is, interact with it
    /// <br />
    /// If not, open the <i>pause</i> menu
    /// </summary>
    public Interactable CheckInteraction()
    {
        if (listOfInteractables.Count == 0)
            return null;

        listOfInteractables.OrderBy(interactable => (interactable.transform.position - transform.parent.position).sqrMagnitude).ToList();
        return listOfInteractables[0];
        
    }


    /*
        Interaction system:
            
            Check for interaction:
                WIDE CONE (seems to be 90° in total), between 1-2 units range (sqrt of 2 (1.414213562374095)? Between sqrt of 2 & 2?)
                OnTriggerEnter, if Interactable, add to list
                OnTriggerExit, if Interactable, remove from list

                When pressing A, check said list
                if 0, open "pause" menu
                if > 1, check distances between list entries & player

     

            Interactable script:
            
                When calling Interactable, invoke a unity event.
                Build a system for using different types of unity events? (i.e. Event<Dialogue>, Event<Canvas>, Event<Shop>, Event<Chest>, etc.)
     
     */


}
