using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class DialogueMasterChoiceRequirements : MonoBehaviour
{
    public List<string> valueFunctionNames = new List<string>();
    public List<string> flagFunctionNames = new List<string>();
    public Flags flags = new Flags();
    public Values values = new Values();
    BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    public void Instantiate()
    {
        System.Type flagType = flags.GetType();
        System.Type valueType = values.GetType();

        MethodInfo[] flagInfo = flagType.GetMethods(bindingFlags);
        MethodInfo[] valueInfo = valueType.GetMethods(bindingFlags);

        foreach (MethodInfo methodInfo in flagInfo)
        {
            flagFunctionNames.Add(methodInfo.Name);
        }
        
        foreach (MethodInfo methodInfo in valueInfo)
        {
            valueFunctionNames.Add(methodInfo.Name);
        }
    }


    public class Flags
    {
        public DialogueMasterNodeChoice choiceInstance;
        
        private bool GenericFlag()
        {
            return !choiceInstance.requirementInvertedFlagCheck;
        }

    }

    public class Values
    {
        public DialogueMasterNodeChoice choiceInstance;
        private bool CheckStrength()
        {
            int strength = 1;

            if (strength < choiceInstance.requirementValueCheck)
                return false;

            return true;
        }

        private bool CheckSpeed()
        {
            int speed = 1;

            if (speed < choiceInstance.requirementValueCheck)
                return false;

            return true;
        }
        
        private bool CheckDefense()
        {
            int speed = 1;

            if (speed < choiceInstance.requirementValueCheck)
                return false;

            return true;
        }
        
        private bool CheckIntelligence()
        {
            int speed = 1;

            if (speed < choiceInstance.requirementValueCheck)
                return false;

            return true;
        }

    }

    



}
