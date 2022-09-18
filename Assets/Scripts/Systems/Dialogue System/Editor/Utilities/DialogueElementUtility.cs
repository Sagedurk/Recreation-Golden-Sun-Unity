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

    public static Image CreateImage(Rect rect, ScaleMode mode = ScaleMode.ScaleAndCrop, Texture imageTex = null)
    {
        Image image = new Image();

        image.image = imageTex;
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

}
