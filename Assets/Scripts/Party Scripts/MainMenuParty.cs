using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuParty : MonoBehaviour
{
    [SerializeField] private GameObject partyInformation;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("CHECK PARTY STATS");

        for (int i = 0; i < transform.childCount; i++)
        {
            if((i + 1) > partyInformation.transform.childCount)
                return;     //If party has less than 4 members


            CharacterData currentCharacter =
                partyInformation.transform.GetChild(i).GetComponent<CharacterData>();

            Transform characterUI = transform.GetChild(i);

            Transform UIContainerName = characterUI.GetChild(0);
            Transform UIContainerHP = characterUI.GetChild(1);
            Transform UIContainerPP = characterUI.GetChild(2);



        //Name variables
            Text UITextNameShadow   = UIContainerName.GetChild(0).GetComponent<Text>();
            Text UITextName         = UIContainerName.GetChild(1).GetComponent<Text>();


        //HP variables
            Slider UIBarHP      = UIContainerHP.GetChild(0).GetComponent<Slider>();
            Text UITextHP       = UIContainerHP.GetChild(2).GetChild(1).GetComponent<Text>();
            Text UITextHPShadow = UIContainerHP.GetChild(2).GetChild(0).GetComponent<Text>();


        //PP variables
            Slider UIBarPP      = UIContainerPP.GetChild(0).GetComponent<Slider>();
            Text UITextPP       = UIContainerPP.GetChild(2).GetChild(1).GetComponent<Text>();
            Text UITextPPShadow = UIContainerPP.GetChild(2).GetChild(0).GetComponent<Text>();




        //Name assignation
            UITextName.text         = currentCharacter.name;
            UITextNameShadow.text   = currentCharacter.name;

        //HP assignation
            UIBarHP.maxValue    = currentCharacter.stats.maximumHP;
            UIBarHP.value       = currentCharacter.stats.currentHP;

            UITextHP.text       = UIBarHP.value.ToString();
            UITextHPShadow.text = UIBarHP.value.ToString();


        //PP assignation
            UIBarPP.maxValue    = currentCharacter.stats.maximumPP;
            UIBarPP.value       = currentCharacter.stats.currentPP;

            UITextPP.text       = UIBarPP.value.ToString();
            UITextPPShadow.text = UIBarPP.value.ToString();
        }
    }

}
