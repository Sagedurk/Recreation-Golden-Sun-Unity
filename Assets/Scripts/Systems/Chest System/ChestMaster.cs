using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMaster : MonoBehaviour
{
    //CONTAINER MASTER
    public enum ChestStates
    {
        UNOPENED,
        OPENED
    }
    
    //MAKE AN ITEM DATABASE TO CHECK AGAINST!
    //Equipment - Weapons, Body Armor, Hand Armor, Head Armor, Supplemental                 gender-specific equipment
    //Misc. - Consumables, Stat-boosting items, Lucky Medals, Utility Psynergy items
    //Key Items - Important items, Rare items

    //Does item stack?

    [System.Serializable]
    public class ChestInstance{

        public ChestStates chestState = ChestStates.UNOPENED;

        public string characterName;
        public string itemName;
        public string containerName;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugChest(ChestInstance instance)
    {

        switch (instance.chestState)
        {
            case ChestStates.UNOPENED:

                //Make this into dialogue
                Debug.Log(instance.characterName + " got some " + instance.itemName + ".");
                Debug.Log(instance.characterName + " found " + instance.itemName + "!");
                instance.chestState = ChestStates.OPENED;

            break;

            case ChestStates.OPENED:

                //Make this into dialogue
                Debug.Log(instance.characterName + " checked the " + instance.containerName.ToLower() + "...");  //Format: "[NAME OF CHAR] checked the [NAME OF CONTAINER]..."
                Debug.Log("but didn't find anything."); //If not found: "but didn't find anything." //If found: "[NAME OF CHAR] found [NAME OF ITEM]!"

                break;

            default:
                break;
        }
        
    }

}
