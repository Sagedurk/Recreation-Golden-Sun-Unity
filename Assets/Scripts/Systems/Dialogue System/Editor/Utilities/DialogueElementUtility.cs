using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class DialogueElementUtility      //Rename class, make it into a Utility for UIElements/VisualElements
{


   public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textField = new TextField()
        {
            value = value,
            label = label
        };

        if(onValueChanged != null)
        {
            textField.RegisterValueChangedCallback(onValueChanged);
        }

        return textField;
    }

    public static TextField CreateNumField(float value, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField numField = CreateTextField(value.ToString(), label, onValueChanged);

        numField.maxLength = 9;     //limit to not be able to exceed int.MaxValue (2 147 483 647)

        numField.labelElement.style.minWidth = 0;

        return numField;
    }

    public static Foldout CreateVec2Field(string fieldName, float xValue, float yValue, EventCallback<ChangeEvent<string>> xValueChanged = null, EventCallback<ChangeEvent<string>> yValueChanged = null)
    {
        Foldout foldout = CreateFoldout(fieldName, true);

        TextField xField = CreateNumField(xValue, "X: ", xValueChanged);
        TextField yField = CreateNumField(yValue, "Y: ", yValueChanged);

        foldout.Add(xField);
        foldout.Add(yField);

        return foldout;
    }

    public static Image CreateImage(Rect rect, ScaleMode mode = ScaleMode.ScaleToFit, Sprite imageTex = null)
    {
        Image image = new Image();

        image.sprite = imageTex;
        image.sourceRect = rect;
        image.scaleMode = mode;

        return image;
    }

    public static ObjectField CreateObjectField<T>(EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null) where T : UnityEngine.Object
    {
        ObjectField objectField = new ObjectField();
        objectField.objectType = typeof(T);


        if(onValueChanged != null)
        {
            objectField.RegisterValueChangedCallback(onValueChanged);
        }

        return objectField;
    }

    public static Toggle CreateToggle(bool value = true, string label = null, EventCallback<ChangeEvent<bool>> onValueChanged = null)
    {
        Toggle toggle = new Toggle()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            toggle.RegisterValueChangedCallback(onValueChanged);
        }

        return toggle;
    }

    public static TextField CreateTextArea(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textArea = CreateTextField(value, label, onValueChanged);

        textArea.multiline = true;

        return textArea;
    }
    public static Foldout CreateFoldout(string title, bool collapsed = false)
    {
        Foldout foldout = new Foldout()
        {
            text = title,
            value = !collapsed
        };

        return foldout;
    }

    public static Button CreateButton(string text, Action onClick = null)
    {
        Button button = new Button(onClick)
        {
            text = text
        };

        return button;
    }

    public static Port CreatePort(DialogueMasterNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
    {
        Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

        port.portName = portName;

        return port;

    }




    #region Utility Functions

    public static void RemoveCharactersNaN(ChangeEvent<string> callback, out int parsedValue)
    {
        //Feels dirty, but it works

        string intString = "";

        //Go through all characters the user has input, only copy numbers
        for (int i = 0; i < callback.newValue.Length; i++)
        {
            char chr = callback.newValue[i];

            if (callback.previousValue == "0")
            {
                if (chr > '0' && chr <= '9')
                    intString += chr;

                continue;
            }

            if (chr >= '0' && chr <= '9')
                intString += chr;
        }

        if (!int.TryParse(intString, out parsedValue))
            intString = "0";

        //Update the inputfield's string
        TextField field = callback.target as TextField;
        field.SetValueWithoutNotify(intString);
    }
    public static void RemoveCharactersNaN(ChangeEvent<string> callback, out float parsedValue)
    {
        //Feels dirty, but it works

        string intString = "";

        //Go through all characters the user has input, only copy numbers
        for (int i = 0; i < callback.newValue.Length; i++)
        {
            char chr = callback.newValue[i];

            if (callback.previousValue == "0")
            {
                if (chr > '0' && chr <= '9')
                    intString += chr;

                continue;
            }

            if (chr >= '0' && chr <= '9')
                intString += chr;
        }

        if (!float.TryParse(intString, out parsedValue))
            intString = "0";

        //Update the inputfield's string
        TextField field = callback.target as TextField;
        field.SetValueWithoutNotify(intString);
    }

    #endregion


}

public class Vec2VE
{
    public float x;
    public float y;

    public Foldout foldout;

    public void Initialize(string foldoutName)
    {
        foldout = DialogueElementUtility.CreateVec2Field(foldoutName, x, y,
            xCallback => { DialogueElementUtility.RemoveCharactersNaN(xCallback, out x); },
            yCallback => { DialogueElementUtility.RemoveCharactersNaN(yCallback, out y); });
    }

    public void SetValues(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }


}