using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMaster : MonoBehaviour
{

    public Transform partyList;
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

    //Is item an artifact?
    //Does item stack?

    //What kind of containers do we have?
    //Chest (Check for mimics)
    //Jar
    //Wooden Box
    //Barrel
    //

    //Isaac got a Small Jewel.          [NAME] got a [ITEM]
    //Isaac got a Potion.               [NAME] got a [ITEM]
    //Isaac got the Cell Key.           [NAME] got the [ITEM]
    //Isaac got some Power Bread.       [NAME] got some [ITEM]
    //Isaac found an Apple.             [NAME] found an [ITEM]
    //Isaac got 35 coins.               [NAME] got [AMOUNT] [CURRENCY NAME]
    //Isaac got a Psynergy Stone.   The party's PP is fully restored!       The Psynergy Stone disappeared...

    //But Isaac's party can't carry any more, so they left it behind.   



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
                Debug.Log(partyList.GetChild(0).name + " got some " + instance.itemName + ".");
                Debug.Log(partyList.GetChild(0).name + " found " + instance.itemName + "!");
                instance.chestState = ChestStates.OPENED;

            break;

            case ChestStates.OPENED:

                //Make this into dialogue
                //Change partyList.GetChild(0).name into PartyList.mainCharacter(.transform).name
                Debug.Log(partyList.GetChild(0).name + " checked the " + instance.containerName.ToLower() + "...");  //Format: "[NAME OF CHAR] checked the [NAME OF CONTAINER]..."
                Debug.Log("but didn't find anything."); //If not found: "but didn't find anything." //If found: "[NAME OF CHAR] found [NAME OF ITEM]!"

                break;

            default:
                break;
        }
        
    }

}
