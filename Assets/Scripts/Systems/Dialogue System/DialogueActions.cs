using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActions : MonoBehaviour
{

    public bool isRunning = false;

    IEnumerator RunAction()
    {
        isRunning = true;

        Debug.Log("Lesgo");

        yield return new WaitForSeconds(3);
        Debug.Log("We done");



        isRunning = false;
    }



}
