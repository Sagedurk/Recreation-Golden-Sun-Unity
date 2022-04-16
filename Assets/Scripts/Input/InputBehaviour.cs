using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputBehaviour : MonoBehaviour
{
    public GameObject inGameMenu;
    public PlayerInteraction interactionManager;
    public PlayerInput playerInput;
    public bool isInteracted;

    Vector2 movementVector;
    float movementSpeed = 10.0f;
    string previousActionMap;

    public void Psynergy1(InputAction.CallbackContext ctx)
    {   
        Debug.Log("Psy #1 called");

        if (ctx.started)
        {
            Debug.Log("Psy #1 started");
        }
        else if (ctx.performed)
        {
            Debug.Log("Psy #1 performed");
        }
        else if (ctx.canceled)
        {
            Debug.Log("Psy #1 canceled");
        }

    }

    public void Psynergy2(InputAction.CallbackContext ctx)
    {
        Debug.Log("Psy #2 called");

        if (ctx.started)
        {
            Debug.Log("Psy #2 started");
        }
        else if (ctx.performed)
        {
            Debug.Log("Psy #2 performed");
        }
        else if (ctx.canceled)
        {
            Debug.Log("Psy #2 canceled");
        }

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>(); 
        if (movementVector == Vector2.zero)
        {
            return;
        }
        float rotationDegrees = (Mathf.Atan2(movementVector.x, movementVector.y) / 3.142f);
        this.transform.eulerAngles = new Vector3(0, rotationDegrees * 180, 0);
    }

    public void OpenInGameMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Testing");
            inGameMenu.SetActive(true);
        }
    }
    
    public void CloseInGameMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Town");
            inGameMenu.SetActive(false);
        }
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            movementSpeed = 15.0f;
        }
        else if (ctx.canceled)
        {
            movementSpeed = 10.0f;
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Interactable interactable = interactionManager.CheckInteraction();

            if (interactable != null)
                interactable.Interact();
            else
                OpenInGameMenu();

        }
        else if (ctx.canceled)
        {

        }
    }

    public void Dialogue(InputAction.CallbackContext ctx)
    {
        //If in dialogue, swap to an action map that can only call this, on confirm
        //To check
        //Do we already swap action map? Or do we need to do that?


        if (ctx.started)
        {
            isInteracted = true;

        }
        else if (ctx.canceled)
        {
            isInteracted = false;
        }
    }

    
    void OpenInGameMenu()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Testing");
        inGameMenu.SetActive(true);
    }

    private void Update()
    {
        if(movementVector != Vector2.zero)
            transform.position += transform.forward * movementSpeed * Time.deltaTime;        
    }

    public void SwitchActionMap(string nextActionMap)
    {
        previousActionMap = playerInput.currentActionMap.name;
        playerInput.SwitchCurrentActionMap(nextActionMap);

    }

    public void SwitchToPreviousActionMap()
    {
        playerInput.SwitchCurrentActionMap(previousActionMap);
    }

}
