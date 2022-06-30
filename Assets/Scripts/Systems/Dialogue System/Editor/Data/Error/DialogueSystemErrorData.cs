using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystemErrorData
{
    public Color color { get; set; }

    public DialogueSystemErrorData()
    {
        GenerateRandomColor();
    }
    private void GenerateRandomColor()
    {
        color = Color.red;


        //Random.Range(min, max + 1)
        //color = new Color32((byte)Random.Range(65, 256), (byte)Random.Range(50, 176), (byte)Random.Range(50, 176), 255);
    }



}
