using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMaster : MonoBehaviour
{
    public enum ChestState
    {
        UNOPENED,
        OPENED
    }
    public ChestState chestState = ChestState.UNOPENED;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugChest()
    {

        switch (chestState)
        {
            case ChestState.UNOPENED:

                //Make this into dialogue
                Debug.Log("Isaac found Sol Blade!");

                break;
            case ChestState.OPENED:

                //Make this into dialogue
                Debug.Log("Isaac checked the jar...");
                Debug.Log("but didn't find anything.");

                break;

            default:
                break;
        }
        
    }

}
