using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class DialogueElementUtility      //Rename class, make it into a Utility for UIElements/VisualElements
{
    public enum Alignment
    {
        TOP_LEFT,
        TOP_CENTER,
        TOP_RIGHT,
        MID_LEFT,
        MID_CENTER,
        MID_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_CENTER,
        BOTTOM_RIGHT
    }

    public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textField = new TextField()
        {
            value = value,
            label = label

        };

        if (onValueChanged != null)
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

    public static Foldout CreateVec2Field(string fieldName, string[] labels, Vector2 vector, EventCallback<ChangeEvent<string>> xValueChanged = null, EventCallback<ChangeEvent<string>> yValueChanged = null)
    {
        Foldout foldout = CreateFoldout(fieldName, true);

        TextField xField = CreateNumField(vector.x, labels[0], xValueChanged);
        TextField yField = CreateNumField(vector.y, labels[1], yValueChanged);

        foldout.Add(xField);
        foldout.Add(yField);

        return foldout;
    }

    public static Foldout CreateVec4Field(string fieldName, string[] labels, Vector4 vector, EventCallback<ChangeEvent<string>> xValueChanged = null, EventCallback<ChangeEvent<string>> yValueChanged = null, EventCallback<ChangeEvent<string>> zValueChanged = null, EventCallback<ChangeEvent<string>> wValueChanged = null)
    {
        Foldout foldout = CreateFoldout(fieldName, true);

        TextField xField = CreateNumField(vector.x, labels[0], xValueChanged);
        TextField yField = CreateNumField(vector.y, labels[1], yValueChanged);
        TextField zField = CreateNumField(vector.z, labels[2], zValueChanged);
        TextField wField = CreateNumField(vector.w, labels[3], wValueChanged);

        foldout.Add(xField);
        foldout.Add(yField);
        foldout.Add(zField);
        foldout.Add(wField);

        return foldout;
    }
    

    public static TextField CreateDropShadow(ref TextField textElement, Color shadowColor, Vector2 direction, float magnitude)
    {
        TextField temporaryField = CreateTextArea(textElement.value);
        SetTextStyle(ref temporaryField, DialogueMasterElements.Instance.fontSize, GetStyleMargin(textElement.contentContainer[0]), Color.clear, shadowColor);

        Vector3 shadowOffset = new Vector3(direction.x, -direction.y) * magnitude;
        Debug.Log("Shadow Offset: " + shadowOffset);

        textElement.Add(temporaryField);
        temporaryField.StretchToParentWidth();
        temporaryField.transform.position -= Vector3.right * 3 + Vector3.up * 1;
        temporaryField.transform.position += shadowOffset;
        temporaryField.focusable = false;

        InvertChildOrder(ref textElement);


        return temporaryField;
    }

    public static void SetTextStyle(ref TextField textElement, StyleLength fontSize, Vector4 margins, Color backgroundColor, Color textColor, PickingMode pickingMode = PickingMode.Ignore)
    {
        VisualElement text = textElement.contentContainer[0];

        SetStyleMargin(ref text, margins);              //Set margins

        text.style.flexGrow = 0;

        #region Set Font Size
        //TextField - Set Font Size
        if(!textElement.multiline)
            text.contentContainer[0].style.fontSize = fontSize;

        //TextArea - Set Font Size
        for (int j = 0; j < text.contentContainer[0].childCount; j++)
        {
            text.contentContainer[0].contentContainer[j].style.fontSize = fontSize;
        }

        #endregion

        text.style.backgroundColor = Color.clear;       //Remove background
        text.style.color = textColor;
        text.pickingMode = pickingMode;          //Remove pickability

        //'Remove' focus/picking border
        SetBorderColor(ref text, backgroundColor);

        text.focusable = false;
        //text.style.flexDirection = FlexDirection.ColumnReverse;
        //text.style.alignItems = Align.Auto;
    }

    public static void SetBorderColor(ref VisualElement element, Color color)
    {
        element.style.borderLeftColor = color;
        element.style.borderBottomColor = color;
        element.style.borderRightColor = color;
        element.style.borderTopColor = color;
    }
    public static void SetBorderWidth(ref VisualElement element, float width)
    {

        element.style.borderLeftWidth = width;
        element.style.borderBottomWidth = width;
        element.style.borderRightWidth = width;
        element.style.borderTopWidth = width;
    }

    public static ColorField CreateColorField(Color color, EventCallback<ChangeEvent<Color>> onValueChanged = null)
    {
        ColorField colorField = new ColorField()
        {
            value = color
        };

        if (onValueChanged != null)
        {
            colorField.RegisterValueChangedCallback(onValueChanged);
        }

        return colorField;
    }

    public static Image CreateImage(Rect rect, ScaleMode mode = ScaleMode.StretchToFill, Sprite imageTex = null)
    {
        Image image = new Image();

        image.sprite = imageTex;
        image.sourceRect = rect;
        image.scaleMode = mode;

        return image;
    }

    public static ObjectField CreateObjectField<T>(EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null, T objectValue = null) where T : UnityEngine.Object
    {
        ObjectField objectField = new ObjectField();
        objectField.objectType = typeof(T);
        objectField.value = objectValue;

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
    public static Foldout CreateFoldout(string title, bool collapsed = false, EventCallback<ChangeEvent<bool>> onValueChanged = null)
    {
        Foldout foldout = new Foldout()
        {
            text = title,
            value = !collapsed
        };

        if (onValueChanged != null)
        {
            foldout.RegisterValueChangedCallback(onValueChanged);
        }

        return foldout;
    }

    public static DropdownField CreateDropdown(List<string> options, int defaultIndex  = 0, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        DropdownField dropdown = new DropdownField()
        {
            choices = options,
            index = defaultIndex
        };

        if (onValueChanged != null)
        {
            dropdown.RegisterValueChangedCallback(onValueChanged);
        }

        return dropdown;
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

        string numString = "";

        //Go through all characters the user has input, only copy numbers
        for (int i = 0; i < callback.newValue.Length; i++)
        {
            char chr = callback.newValue[i];

            if (callback.previousValue == "0")
            {
                if (chr > '0' && chr <= '9')
                    numString += chr;
                else if (chr == '-')
                    numString += chr + callback.previousValue;

                continue;
            }


            if (chr >= '0' && chr <= '9')
                numString += chr;
            else if (chr == '-')
                numString.Insert(0, chr.ToString());
        }


        //Attempt to parse the new value, if not possible, parse the previous value
        if (!int.TryParse(numString, NumberStyles.AllowTrailingSign | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out parsedValue))
        {
            if (callback.newValue != "")
            {
                numString = callback.previousValue;
                int.TryParse(numString, NumberStyles.AllowTrailingSign | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out parsedValue);

            }
        }

        numString = parsedValue.ToString();

        //Update the inputfield's string
        TextField field = callback.target as TextField;
        field.SetValueWithoutNotify(numString);
    }

    private static bool ContainsEither(this string checkedString, string[] subStrings)
    {
        foreach (string strng in subStrings)
        {
            if (checkedString.Contains(strng))
                return true;
        }

        return false;
    }

    public static void RemoveCharactersNaN(ChangeEvent<string> callback, out float parsedValue, bool allowNegatives = false)
    {
        //Feels dirty, but it works
        parsedValue = 0;
        string numString = "";
        string[] stringEndExceptions = { };
        NumberStyles numberStyle = NumberStyles.Float; //NumberStyles.AllowDecimalPoint |NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign

        //Go through all characters the user has input, only allow numbers, decimals, and negative
        for (int i = 0; i < callback.newValue.Length; i++)
        {
            char chr = callback.newValue[i];

            if (callback.previousValue == "0")
            {
                if (chr > '0' && chr <= '9')
                    numString += chr;

                else if (chr == '.')
                {
                    numString += callback.previousValue + chr;
                }
                else if (chr == '-' && allowNegatives)
                    numString += chr + callback.previousValue;

                continue;
            }

            if ((chr >= '0' && chr <= '9') || (chr == '.' && !numString.Contains(chr.ToString())))
                numString += chr;
            else if (chr == '-' && allowNegatives && !numString.Contains(chr.ToString()))
                numString = chr + numString;
        }

        bool isTryingParse = true;
        foreach (string subString in stringEndExceptions)
        {
            if (numString.EndsWith(subString))
            {
                isTryingParse = false;
            }
        }

        if (isTryingParse)
        {
            //Attempt to parse the new value, if not possible, parse the previous value
            if (!float.TryParse(numString, numberStyle, CultureInfo.InvariantCulture, out parsedValue))
            {
                if (callback.newValue != "")
                {
                    numString = callback.previousValue;
                    float.TryParse(numString, numberStyle, CultureInfo.InvariantCulture, out parsedValue);
                }
            }

            numString = parsedValue.ToString();
        }

        //Update the inputfield's string
        TextField field = callback.target as TextField;

        if (field.maxLength != 7)
            field.maxLength = 7;

        field.SetValueWithoutNotify(numString);
    }
    public static string[] RemoveCharactersNaN(ChangeEvent<string> callback, out float parsedValue, ref string numString, bool allowNegatives = false)
    {
        //Feels dirty, but it works
        parsedValue = 0;
        numString = "";
        string[] stringEndExceptions = {"-0",".", "-0."};
        NumberStyles numberStyle = NumberStyles.Float; //NumberStyles.AllowDecimalPoint |NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign

        Debug.Log("prev: " + callback.previousValue);
        Debug.Log("prev: " + callback.newValue);

        //Go through all characters the user has input, only allow numbers, decimals, and negative
        for (int i = 0; i < callback.newValue.Length; i++)
        {
            char chr = callback.newValue[i];

            if (callback.previousValue == "0")
            {
                if (chr > '0' && chr <= '9')
                    numString += chr;

                else if (chr == '.')
                {
                    numString += callback.previousValue + chr;
                }
                else if (chr == '-' && allowNegatives)
                    numString += chr + callback.previousValue;

                continue;
            }

            if ((chr >= '0' && chr <= '9') || (chr == '.' && !numString.Contains(chr.ToString())))
                numString += chr;
            else if (chr == '-' && allowNegatives && !numString.Contains(chr.ToString()))
                numString = chr + numString;
        }
        Debug.Log(numString);

        bool isTryingParse = true;
        foreach (string subString in stringEndExceptions)
        {
            if(numString.EndsWith(subString))
            {
                isTryingParse = false;
            }
        }

        Debug.Log(isTryingParse);

        if (isTryingParse)
        {
            //Attempt to parse the new value, if not possible, parse the previous value
            if (!float.TryParse(numString, numberStyle, CultureInfo.InvariantCulture, out parsedValue))
            {
                if (callback.newValue != "")
                {
                    numString = callback.previousValue;
                    float.TryParse(numString, numberStyle, CultureInfo.InvariantCulture, out parsedValue);
                }
            }
            
            numString = parsedValue.ToString();
        }

        //Update the inputfield's string
        TextField field = callback.target as TextField;

        if(field.maxLength != 7)
            field.maxLength = 7;

        field.SetValueWithoutNotify(numString);
        return stringEndExceptions;
    }

    public static void Normalize(ChangeEvent<string> callback, out float normalizedValue, bool allowNegatives)
    {

        string inputString = "";
        TextField field = callback.target as TextField;
        string [] exceptions = RemoveCharactersNaN(callback, out normalizedValue, ref inputString, allowNegatives);


        bool isNormalizing = true;
        foreach (string subString in exceptions)
        {
            if (inputString.EndsWith(subString))
            {
                isNormalizing = false;
            }
        }

        if (!isNormalizing) 
        {
            field.SetValueWithoutNotify(inputString);
            return;
        }


        if (normalizedValue > 1)
            normalizedValue = 1.0f;

        else if (normalizedValue < 0 && !allowNegatives)  //If negatives aren't allowed
            normalizedValue = -0.0f;

        else if (normalizedValue < -1 && allowNegatives) //If negatives are allowed
            normalizedValue = -1.0f;


        field.SetValueWithoutNotify(normalizedValue.ToString());
    }


    public static List<VisualElement> GetChildElements(VisualElement visualElement)
    {
        List<VisualElement> elementsToReAdd = new List<VisualElement>();

        foreach (VisualElement element in visualElement.Children())
        {
            elementsToReAdd.Add(element);
        }

        return elementsToReAdd;
    }

    public static void ReAddElements<T>(ref T containerElement, List<VisualElement> elements, int startIndex) where T : VisualElement
    {
        for (int i = startIndex; i < elements.Count; i++)
        {
            containerElement.Remove(elements[i]);
            containerElement.Add(elements[i]);
        }
    }

    public static void InvertChildOrder<T>(ref T containerElement) where T : VisualElement
    {
        List<VisualElement> invertedChildren = new List<VisualElement>();
        for (int i = containerElement.childCount - 1; i > -1; i--)
        {
            invertedChildren.Add(containerElement.contentContainer[i]);
        }

        containerElement.Clear();
        for (int i = 0; i < invertedChildren.Count; i++)
        {
            containerElement.Add(invertedChildren[i]);
        }

    }

    public static void SetPositionInRelationToParent(ref VisualElement element, Alignment alignmentPreset)
    {
        switch (alignmentPreset)
        {
            case Alignment.TOP_LEFT:
                element.transform.position = new Vector3(0, 0);
                break;
            case Alignment.TOP_CENTER:
                element.transform.position = new Vector3(element.parent.contentRect.width / 2 - element.contentRect.width / 2, 0);
                break;
            case Alignment.TOP_RIGHT:
                element.transform.position = new Vector3(element.parent.contentRect.width - element.contentRect.width, 0);
                break;
            case Alignment.MID_LEFT:
                element.transform.position = new Vector3(0, element.parent.contentRect.height / 2 - element.contentRect.height / 2);
                break;
            case Alignment.MID_CENTER:
                element.transform.position = new Vector3(element.parent.contentRect.width / 2 - element.contentRect.width / 2, element.parent.contentRect.height / 2 - element.contentRect.height / 2);
                break;
            case Alignment.MID_RIGHT:
                element.transform.position = new Vector3(element.parent.contentRect.width - element.contentRect.width, element.parent.contentRect.height / 2 - element.contentRect.height / 2);
                break;
            case Alignment.BOTTOM_LEFT:
                element.transform.position = new Vector3(0, element.parent.contentRect.height - element.contentRect.height);
                break;
            case Alignment.BOTTOM_CENTER:
                element.transform.position = new Vector3(element.parent.contentRect.width/2 - element.contentRect.width / 2, element.parent.contentRect.height - element.contentRect.height);
                break;
            case Alignment.BOTTOM_RIGHT:
                element.transform.position = new Vector3(element.parent.contentRect.width - element.contentRect.width, element.parent.contentRect.height - element.contentRect.height);
                break;
            default:
                break;
        }
    }


    #endregion

    #region Style Utilities
    public static void SetStyleSize<T>(ref T element, float width, float height) where T : VisualElement
    {
        element.style.width = width;
        element.style.height = height;
    }

    public static void SetStyleSlice<T>(ref T element, Vector4 border) where T : VisualElement
    {
        element.style.unitySliceLeft = (int)border.x;
        element.style.unitySliceBottom = (int)border.y;
        element.style.unitySliceRight = (int)border.z;
        element.style.unitySliceTop = (int)border.w;

    }

    public static void SetStyleMargin<T>(ref T element, Vector4 margins) where T : VisualElement
    {
        element.style.marginLeft = margins.x;
        element.style.marginBottom = margins.y;
        element.style.marginRight = margins.z;
        element.style.marginTop = margins.w;

    }

    public static Vector4 GetStyleMargin<T>(T element) where T : VisualElement
    {
        float x = element.style.marginLeft.value.value;     //  X = Left
        float y = element.style.marginBottom.value.value;   //  Y = Bottom
        float z = element.style.marginRight.value.value;    //  Z = Right
        float w = element.style.marginTop.value.value;      //  W = Top

        return new Vector4(x, y, z, w);
    }

    public static StyleLength GetFontSize(TextField textElement)
    {
        return textElement.contentContainer[0].style.fontSize;
    }

    #endregion


}

public class MarginsVE
{
    public Vector4 margins = Vector4.zero;
    public string[] marginNames = {"Left: ", "Bottom: ", "Right: ", "Top: "};
    public Foldout foldout;
    public void Initialize(string foldoutName)
    {
        foldout = DialogueElementUtility.CreateVec4Field(foldoutName, marginNames, margins,
            xCallback => { DialogueElementUtility.RemoveCharactersNaN(xCallback, out margins.x); },
            yCallback => { DialogueElementUtility.RemoveCharactersNaN(yCallback, out margins.y); },
            zCallback => { DialogueElementUtility.RemoveCharactersNaN(zCallback, out margins.z); },
            wCallback => { DialogueElementUtility.RemoveCharactersNaN(wCallback, out margins.w); });
    }


    public void Initialize(string foldoutName, EventCallback<ChangeEvent<string>> xCallback, EventCallback<ChangeEvent<string>> yCallback, EventCallback<ChangeEvent<string>> zCallback, EventCallback<ChangeEvent<string>> wCallback)
    {

        foldout = DialogueElementUtility.CreateVec4Field(foldoutName, marginNames, margins,
            xCallback => { DialogueElementUtility.RemoveCharactersNaN(xCallback, out margins.x); } + xCallback,
            yCallback => { DialogueElementUtility.RemoveCharactersNaN(yCallback, out margins.y); } + yCallback,
            zCallback => { DialogueElementUtility.RemoveCharactersNaN(zCallback, out margins.z); } + zCallback,
            wCallback => { DialogueElementUtility.RemoveCharactersNaN(wCallback, out margins.w); } + wCallback);
    }
    public void Initialize(string foldoutName, EventCallback<ChangeEvent<string>> callback)
    {

        foldout = DialogueElementUtility.CreateVec4Field(foldoutName, marginNames, margins,
            xCallback => { DialogueElementUtility.RemoveCharactersNaN(xCallback, out margins.x); } + callback,
            yCallback => { DialogueElementUtility.RemoveCharactersNaN(yCallback, out margins.y); } + callback,
            zCallback => { DialogueElementUtility.RemoveCharactersNaN(zCallback, out margins.z); } + callback,
            wCallback => { DialogueElementUtility.RemoveCharactersNaN(wCallback, out margins.w); } + callback);
    }

    public void SetValues(Vector4 newMargins)
    {
        margins = newMargins;
    }


    
}

public class Vec2VE
{
    public Vector2 vector = Vector2.zero;

    public Foldout foldout;

    public void Initialize(string foldoutName, EventCallback<ChangeEvent<string>> addedCallback = null, string xLabel = "X: ", string yLabel = "Y: ")
    {
        string[] vectorLabels = {xLabel, yLabel};

        foldout = DialogueElementUtility.CreateVec2Field(foldoutName, vectorLabels, vector,
            Callback => { DialogueElementUtility.RemoveCharactersNaN(Callback, out vector.x); } + addedCallback,
            Callback => { DialogueElementUtility.RemoveCharactersNaN(Callback, out vector.y); } + addedCallback);
    }

    public void InitializeNormalized(string foldoutName, bool allowNegatives, EventCallback<ChangeEvent<string>> addedCallback = null, string xLabel = "X: ", string yLabel = "Y: ")
    {
        string[] vectorLabels = { xLabel, yLabel };
        foldout = DialogueElementUtility.CreateVec2Field(foldoutName, vectorLabels, vector, 
            callback=> { DialogueElementUtility.Normalize(callback, out vector.x, allowNegatives); } + addedCallback, 
            callback=> { DialogueElementUtility.Normalize(callback, out vector.y, allowNegatives); } + addedCallback);
    }


    public void Initialize(string foldoutName, EventCallback<ChangeEvent<string>> xCallback, EventCallback<ChangeEvent<string>> yCallback, string xLabel = "X: ", string yLabel = "Y: ")
    {
        string[] vectorLabels = { xLabel, yLabel };
        foldout = DialogueElementUtility.CreateVec2Field(foldoutName, vectorLabels, vector, xCallback, yCallback);
    }
    
    public void SetValues(Vector2 newVector)
    {
        vector = newVector;
    }

    


}