using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputBehaviour : MonoBehaviour
{
    public GameObject inGameMenu;

    Vector2 movementVector;
    float movementSpeed = 10.0f;
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
        Debug.Log(movementVector);
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

    private void Update()
    {
        if(movementVector != Vector2.zero)
            this.transform.position += this.transform.forward * movementSpeed * Time.deltaTime;        
    }
}
