using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjinnMaster : MonoBehaviour
{

    [System.Serializable]
    class Djinn
    {
        public string name;
        public Element element;
        public State state;
        //Add combat effect
    }

    public enum Element
    {
        VENUS,      //Earth
        MARS,       //Fire
        JUPITER,    //Wind
        MERCURY     //Water
    }
    public enum State
    {
        SET,
        STANDBY,
        RECOVERY
    }



}
