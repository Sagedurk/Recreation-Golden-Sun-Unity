using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Playable Character", order = 1)]
public class CharacterData : MonoBehaviour
{
    public int characterID;
    //public PlayerClass class;
    //public StatusEffect status;
    public int level;
    public int experienceAmount;
    public Statistics stats;
  
    //public Djinni djinni;

}

[System.Serializable]
public class Statistics
{
    [Tooltip("Current amount of Hit Points")]
    public int currentHP;
    [Tooltip("Maximum amount of Hit Points")]
    public int maximumHP;

    [Tooltip("Current amount of Psynergy Points")]
    public int currentPP;
    [Tooltip("Maximum amount of Psynergy Points")]
    public int maximumPP;

    public int attack;
    public int defense;
    public int agility;
    public int luck;
}