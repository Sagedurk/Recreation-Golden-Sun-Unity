// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Town"",
            ""id"": ""dbb717a8-62fd-4c52-9e8c-8007d7d51412"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ebcb693d-1713-4247-b050-5ddd07276457"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quick Psynergy #1"",
                    ""type"": ""Button"",
                    ""id"": ""a6eb0421-4eaf-4972-8f08-ca647226d10a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quick Psynergy #2"",
                    ""type"": ""Button"",
                    ""id"": ""0a9d231f-de45-40f3-b8ae-cb3e956f9791"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Open General Menu"",
                    ""type"": ""Button"",
                    ""id"": ""fc95c0cb-cd55-4d7c-afd3-3c6458365325"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""8ccdb1dd-961d-4911-80d3-13c8db29d30d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start Menu"",
                    ""type"": ""Button"",
                    ""id"": ""f8c8773d-7cf4-451b-b960-92da16e9e271"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""793d36fc-be65-4045-9aa3-cb73c062f833"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Left Joystick"",
                    ""id"": ""dce56ac1-10cd-421c-9ff9-9639e94adf2a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""29da9704-ca48-452b-8727-e2eea8029ab4"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0d00991f-d4f9-4d49-a21d-c2f8193a5b84"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2146160d-e90f-4959-872c-77c01768fbae"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""94adda01-072a-46a5-88aa-4d29750fcc7a"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6b496679-380f-46e7-bde9-fe7e9ba67d43"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""924498ff-59bf-4d3a-9ea5-5536e47bbf60"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c3e7767c-470d-4062-a189-9dd6b174f2c4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""420dea5d-6ae2-4a4a-9808-7b0e5a41664f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5629ce66-434a-41fd-b658-d23da786168a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Dpad"",
                    ""id"": ""67980951-ce4a-4616-b01a-ec85f1c265a3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b044a38e-de8f-42b9-8a22-d998ed259959"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c74b9795-74a8-4874-acbe-21056c6e35ee"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""45e36e8f-5a12-4260-ac45-d9f1a62c1dcd"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""670d6c87-99bb-4d63-87a9-c35038076834"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""00de903f-3888-4da1-bf72-81cf85166dab"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Quick Psynergy #1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87d8264f-7e2f-4863-8915-effa96a74d88"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM;Xbox"",
                    ""action"": ""Quick Psynergy #1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b64b9672-5e9a-4e0a-a087-224dc202b72e"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Quick Psynergy #2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4da50c2-537a-4ee1-b3db-7b245885e0f2"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Quick Psynergy #2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e4c2a13-c5ac-456b-8caa-c1af8904651f"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Open General Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5701a4c0-7c3b-45c3-9377-49c41f07bca8"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Open General Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""385385f2-fded-44bb-a115-f4fb674f2a87"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Open General Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5d60b6f-62bd-4123-a63f-f28f504b2bfa"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea762568-a2b7-43d0-b4a7-51c8e1dfb957"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80ef741d-dacf-413a-8f46-940f228d09ca"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Start Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b59389a2-1887-4c96-9bc1-59bc63a40328"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Start Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""535e929e-6caf-4cde-b1d8-477c983aaa69"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Overworld"",
            ""id"": ""5b6337b4-bebf-4b89-a677-cf3d1c49a5d3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5a8a676f-e53d-4c35-9cad-fcd362a36092"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quick Psynergy #1"",
                    ""type"": ""Button"",
                    ""id"": ""395dedac-480f-4681-8558-1ebaaff725e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quick Psynergy #2"",
                    ""type"": ""Button"",
                    ""id"": ""7236d263-8fe4-4f5a-b3c6-c6f05272ae94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Open General Menu"",
                    ""type"": ""Button"",
                    ""id"": ""99db56c5-f00f-4bb1-930f-63b7fcc2818f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""2481ffb0-ac0a-4ea0-8fd0-161401ca0853"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start Menu"",
                    ""type"": ""Button"",
                    ""id"": ""9005de0f-6a5c-40f1-8ebc-46ca916993a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b3a4197f-1189-4591-a21a-493791eba1df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Left Joystick"",
                    ""id"": ""eeae42d9-d641-4015-a231-24c744b05455"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""753439ae-97ef-463a-b02a-41592260c4dd"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c968f58a-2fe3-458e-8dfb-477f923428db"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e1a94c14-7ead-465b-b7f2-7d458f6db1c0"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""931c6cdc-a570-4044-aa97-ed3513ddb9c4"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a487c328-c176-498b-acbb-85099c53b123"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6017c709-b723-4ab2-b4ed-b6b957336099"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9eb80a2f-1c8b-479a-ae67-2696944546e4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a9ea0aa5-4bd4-4a43-8038-60adba3a13b8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3d1fc573-301f-42b1-8b9a-3852c306a0bb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Dpad"",
                    ""id"": ""46571446-d2b5-4122-9f0b-6bfda8773457"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b86f6f20-f70d-41de-add1-4440093282ea"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ccc03a2c-b415-46f5-aa35-7d94b14a56ed"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a7d14ca6-4bb3-4846-9427-714b9861af56"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""528a0188-30f6-43db-9969-cc6a0ab797c0"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""09ce6571-805e-4ef4-9753-ed858b2bb6bd"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Quick Psynergy #1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14554d52-402b-40e5-ade5-b03f167cab52"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Quick Psynergy #2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65854d83-7cb8-480f-ab36-b2f45f9004b4"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Open General Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67be796a-5b9b-4fba-83f9-00cbb64f1136"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Open General Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88505f05-7574-45db-be9e-202c0fdec489"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""035d8861-4bc5-4d19-a233-0822330cbf32"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Start Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f594e4b-88ca-426e-ad04-c9ff2bccf5c7"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""General Menu"",
            ""id"": ""075390b3-6b2f-4749-a1b7-767a2206da12"",
            ""actions"": [
                {
                    ""name"": ""Cursor Left"",
                    ""type"": ""Button"",
                    ""id"": ""7be17c16-2aeb-497e-bd95-7c042195c10f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cursor Right"",
                    ""type"": ""Button"",
                    ""id"": ""0bd6b5e6-1902-429d-881e-92e11f16feaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""576d3c35-f342-45e2-a76a-5ef92e0d3880"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""bc78032e-194e-4edf-bc85-35f99847d53d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""77777533-d023-4c29-b2bd-927e2b91f9b5"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cursor Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""927dbe5d-4ba4-4239-b481-61f782b608b0"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cursor Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6402c655-151b-43a3-9b34-3d78d71c3617"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0c18df1-ff82-4f0f-83b1-8ead397c19c7"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6684a31a-153c-4c39-bb67-ad44c3d0b7c7"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""913677c8-f046-4482-a777-4e81d6a9e7ef"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cursor Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d481de8f-4709-4b0e-8250-1375d2af2858"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cursor Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Start Menu"",
            ""id"": ""af7d58bf-6d7a-45b5-8da4-d356bfc97568"",
            ""actions"": [
                {
                    ""name"": ""Cursor Up"",
                    ""type"": ""Button"",
                    ""id"": ""1a8fe4c8-4273-45fa-b3e2-b8ce75c43348"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cursor Down"",
                    ""type"": ""Button"",
                    ""id"": ""e4f7b68d-43d9-45ab-ae39-4aae15060c0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""c42f253a-55bc-458d-a974-1f370c958743"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""e4cef3de-b73b-457b-9c1a-3821fe3a44ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91c6a2fb-0d47-4993-b505-35e40955fb76"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cursor Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0da46ccf-0fb0-42e2-a658-9a59f5b94564"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cursor Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b90a07e6-dd4c-4310-9fd4-6b638a0505ab"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a11990d-6063-400e-94a9-fb86c5f5a30b"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbbf2be3-c7a0-4377-b307-78ea3340beec"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Testing"",
            ""id"": ""ba879e20-1e32-463b-8694-f85f6428e3e9"",
            ""actions"": [
                {
                    ""name"": ""Cancel Menu"",
                    ""type"": ""Button"",
                    ""id"": ""3ee68f3d-700b-43de-98ba-1b4a8f4db29f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1927fe46-2860-4b60-8eea-af9385b65011"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06a56803-2a9d-4341-9578-480cf0efbb3e"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Cancel Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KBM"",
            ""bindingGroup"": ""KBM"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Town
        m_Town = asset.FindActionMap("Town", throwIfNotFound: true);
        m_Town_Move = m_Town.FindAction("Move", throwIfNotFound: true);
        m_Town_QuickPsynergy1 = m_Town.FindAction("Quick Psynergy #1", throwIfNotFound: true);
        m_Town_QuickPsynergy2 = m_Town.FindAction("Quick Psynergy #2", throwIfNotFound: true);
        m_Town_OpenGeneralMenu = m_Town.FindAction("Open General Menu", throwIfNotFound: true);
        m_Town_Sprint = m_Town.FindAction("Sprint", throwIfNotFound: true);
        m_Town_StartMenu = m_Town.FindAction("Start Menu", throwIfNotFound: true);
        m_Town_Interact = m_Town.FindAction("Interact", throwIfNotFound: true);
        // Overworld
        m_Overworld = asset.FindActionMap("Overworld", throwIfNotFound: true);
        m_Overworld_Move = m_Overworld.FindAction("Move", throwIfNotFound: true);
        m_Overworld_QuickPsynergy1 = m_Overworld.FindAction("Quick Psynergy #1", throwIfNotFound: true);
        m_Overworld_QuickPsynergy2 = m_Overworld.FindAction("Quick Psynergy #2", throwIfNotFound: true);
        m_Overworld_OpenGeneralMenu = m_Overworld.FindAction("Open General Menu", throwIfNotFound: true);
        m_Overworld_Sprint = m_Overworld.FindAction("Sprint", throwIfNotFound: true);
        m_Overworld_StartMenu = m_Overworld.FindAction("Start Menu", throwIfNotFound: true);
        m_Overworld_Interact = m_Overworld.FindAction("Interact", throwIfNotFound: true);
        // General Menu
        m_GeneralMenu = asset.FindActionMap("General Menu", throwIfNotFound: true);
        m_GeneralMenu_CursorLeft = m_GeneralMenu.FindAction("Cursor Left", throwIfNotFound: true);
        m_GeneralMenu_CursorRight = m_GeneralMenu.FindAction("Cursor Right", throwIfNotFound: true);
        m_GeneralMenu_Cancel = m_GeneralMenu.FindAction("Cancel", throwIfNotFound: true);
        m_GeneralMenu_Accept = m_GeneralMenu.FindAction("Accept", throwIfNotFound: true);
        // Start Menu
        m_StartMenu = asset.FindActionMap("Start Menu", throwIfNotFound: true);
        m_StartMenu_CursorUp = m_StartMenu.FindAction("Cursor Up", throwIfNotFound: true);
        m_StartMenu_CursorDown = m_StartMenu.FindAction("Cursor Down", throwIfNotFound: true);
        m_StartMenu_Cancel = m_StartMenu.FindAction("Cancel", throwIfNotFound: true);
        m_StartMenu_Accept = m_StartMenu.FindAction("Accept", throwIfNotFound: true);
        // Testing
        m_Testing = asset.FindActionMap("Testing", throwIfNotFound: true);
        m_Testing_CancelMenu = m_Testing.FindAction("Cancel Menu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Town
    private readonly InputActionMap m_Town;
    private ITownActions m_TownActionsCallbackInterface;
    private readonly InputAction m_Town_Move;
    private readonly InputAction m_Town_QuickPsynergy1;
    private readonly InputAction m_Town_QuickPsynergy2;
    private readonly InputAction m_Town_OpenGeneralMenu;
    private readonly InputAction m_Town_Sprint;
    private readonly InputAction m_Town_StartMenu;
    private readonly InputAction m_Town_Interact;
    public struct TownActions
    {
        private @PlayerControls m_Wrapper;
        public TownActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Town_Move;
        public InputAction @QuickPsynergy1 => m_Wrapper.m_Town_QuickPsynergy1;
        public InputAction @QuickPsynergy2 => m_Wrapper.m_Town_QuickPsynergy2;
        public InputAction @OpenGeneralMenu => m_Wrapper.m_Town_OpenGeneralMenu;
        public InputAction @Sprint => m_Wrapper.m_Town_Sprint;
        public InputAction @StartMenu => m_Wrapper.m_Town_StartMenu;
        public InputAction @Interact => m_Wrapper.m_Town_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Town; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TownActions set) { return set.Get(); }
        public void SetCallbacks(ITownActions instance)
        {
            if (m_Wrapper.m_TownActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_TownActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnMove;
                @QuickPsynergy1.started -= m_Wrapper.m_TownActionsCallbackInterface.OnQuickPsynergy1;
                @QuickPsynergy1.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnQuickPsynergy1;
                @QuickPsynergy1.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnQuickPsynergy1;
                @QuickPsynergy2.started -= m_Wrapper.m_TownActionsCallbackInterface.OnQuickPsynergy2;
                @QuickPsynergy2.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnQuickPsynergy2;
                @QuickPsynergy2.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnQuickPsynergy2;
                @OpenGeneralMenu.started -= m_Wrapper.m_TownActionsCallbackInterface.OnOpenGeneralMenu;
                @OpenGeneralMenu.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnOpenGeneralMenu;
                @OpenGeneralMenu.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnOpenGeneralMenu;
                @Sprint.started -= m_Wrapper.m_TownActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnSprint;
                @StartMenu.started -= m_Wrapper.m_TownActionsCallbackInterface.OnStartMenu;
                @StartMenu.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnStartMenu;
                @StartMenu.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnStartMenu;
                @Interact.started -= m_Wrapper.m_TownActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_TownActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_TownActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_TownActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @QuickPsynergy1.started += instance.OnQuickPsynergy1;
                @QuickPsynergy1.performed += instance.OnQuickPsynergy1;
                @QuickPsynergy1.canceled += instance.OnQuickPsynergy1;
                @QuickPsynergy2.started += instance.OnQuickPsynergy2;
                @QuickPsynergy2.performed += instance.OnQuickPsynergy2;
                @QuickPsynergy2.canceled += instance.OnQuickPsynergy2;
                @OpenGeneralMenu.started += instance.OnOpenGeneralMenu;
                @OpenGeneralMenu.performed += instance.OnOpenGeneralMenu;
                @OpenGeneralMenu.canceled += instance.OnOpenGeneralMenu;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @StartMenu.started += instance.OnStartMenu;
                @StartMenu.performed += instance.OnStartMenu;
                @StartMenu.canceled += instance.OnStartMenu;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public TownActions @Town => new TownActions(this);

    // Overworld
    private readonly InputActionMap m_Overworld;
    private IOverworldActions m_OverworldActionsCallbackInterface;
    private readonly InputAction m_Overworld_Move;
    private readonly InputAction m_Overworld_QuickPsynergy1;
    private readonly InputAction m_Overworld_QuickPsynergy2;
    private readonly InputAction m_Overworld_OpenGeneralMenu;
    private readonly InputAction m_Overworld_Sprint;
    private readonly InputAction m_Overworld_StartMenu;
    private readonly InputAction m_Overworld_Interact;
    public struct OverworldActions
    {
        private @PlayerControls m_Wrapper;
        public OverworldActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Overworld_Move;
        public InputAction @QuickPsynergy1 => m_Wrapper.m_Overworld_QuickPsynergy1;
        public InputAction @QuickPsynergy2 => m_Wrapper.m_Overworld_QuickPsynergy2;
        public InputAction @OpenGeneralMenu => m_Wrapper.m_Overworld_OpenGeneralMenu;
        public InputAction @Sprint => m_Wrapper.m_Overworld_Sprint;
        public InputAction @StartMenu => m_Wrapper.m_Overworld_StartMenu;
        public InputAction @Interact => m_Wrapper.m_Overworld_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Overworld; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OverworldActions set) { return set.Get(); }
        public void SetCallbacks(IOverworldActions instance)
        {
            if (m_Wrapper.m_OverworldActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @QuickPsynergy1.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnQuickPsynergy1;
                @QuickPsynergy1.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnQuickPsynergy1;
                @QuickPsynergy1.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnQuickPsynergy1;
                @QuickPsynergy2.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnQuickPsynergy2;
                @QuickPsynergy2.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnQuickPsynergy2;
                @QuickPsynergy2.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnQuickPsynergy2;
                @OpenGeneralMenu.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOpenGeneralMenu;
                @OpenGeneralMenu.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOpenGeneralMenu;
                @OpenGeneralMenu.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOpenGeneralMenu;
                @Sprint.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSprint;
                @StartMenu.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnStartMenu;
                @StartMenu.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnStartMenu;
                @StartMenu.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnStartMenu;
                @Interact.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_OverworldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @QuickPsynergy1.started += instance.OnQuickPsynergy1;
                @QuickPsynergy1.performed += instance.OnQuickPsynergy1;
                @QuickPsynergy1.canceled += instance.OnQuickPsynergy1;
                @QuickPsynergy2.started += instance.OnQuickPsynergy2;
                @QuickPsynergy2.performed += instance.OnQuickPsynergy2;
                @QuickPsynergy2.canceled += instance.OnQuickPsynergy2;
                @OpenGeneralMenu.started += instance.OnOpenGeneralMenu;
                @OpenGeneralMenu.performed += instance.OnOpenGeneralMenu;
                @OpenGeneralMenu.canceled += instance.OnOpenGeneralMenu;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @StartMenu.started += instance.OnStartMenu;
                @StartMenu.performed += instance.OnStartMenu;
                @StartMenu.canceled += instance.OnStartMenu;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public OverworldActions @Overworld => new OverworldActions(this);

    // General Menu
    private readonly InputActionMap m_GeneralMenu;
    private IGeneralMenuActions m_GeneralMenuActionsCallbackInterface;
    private readonly InputAction m_GeneralMenu_CursorLeft;
    private readonly InputAction m_GeneralMenu_CursorRight;
    private readonly InputAction m_GeneralMenu_Cancel;
    private readonly InputAction m_GeneralMenu_Accept;
    public struct GeneralMenuActions
    {
        private @PlayerControls m_Wrapper;
        public GeneralMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CursorLeft => m_Wrapper.m_GeneralMenu_CursorLeft;
        public InputAction @CursorRight => m_Wrapper.m_GeneralMenu_CursorRight;
        public InputAction @Cancel => m_Wrapper.m_GeneralMenu_Cancel;
        public InputAction @Accept => m_Wrapper.m_GeneralMenu_Accept;
        public InputActionMap Get() { return m_Wrapper.m_GeneralMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralMenuActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralMenuActions instance)
        {
            if (m_Wrapper.m_GeneralMenuActionsCallbackInterface != null)
            {
                @CursorLeft.started -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCursorLeft;
                @CursorLeft.performed -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCursorLeft;
                @CursorLeft.canceled -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCursorLeft;
                @CursorRight.started -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCursorRight;
                @CursorRight.performed -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCursorRight;
                @CursorRight.canceled -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCursorRight;
                @Cancel.started -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnCancel;
                @Accept.started -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_GeneralMenuActionsCallbackInterface.OnAccept;
            }
            m_Wrapper.m_GeneralMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CursorLeft.started += instance.OnCursorLeft;
                @CursorLeft.performed += instance.OnCursorLeft;
                @CursorLeft.canceled += instance.OnCursorLeft;
                @CursorRight.started += instance.OnCursorRight;
                @CursorRight.performed += instance.OnCursorRight;
                @CursorRight.canceled += instance.OnCursorRight;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
            }
        }
    }
    public GeneralMenuActions @GeneralMenu => new GeneralMenuActions(this);

    // Start Menu
    private readonly InputActionMap m_StartMenu;
    private IStartMenuActions m_StartMenuActionsCallbackInterface;
    private readonly InputAction m_StartMenu_CursorUp;
    private readonly InputAction m_StartMenu_CursorDown;
    private readonly InputAction m_StartMenu_Cancel;
    private readonly InputAction m_StartMenu_Accept;
    public struct StartMenuActions
    {
        private @PlayerControls m_Wrapper;
        public StartMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CursorUp => m_Wrapper.m_StartMenu_CursorUp;
        public InputAction @CursorDown => m_Wrapper.m_StartMenu_CursorDown;
        public InputAction @Cancel => m_Wrapper.m_StartMenu_Cancel;
        public InputAction @Accept => m_Wrapper.m_StartMenu_Accept;
        public InputActionMap Get() { return m_Wrapper.m_StartMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StartMenuActions set) { return set.Get(); }
        public void SetCallbacks(IStartMenuActions instance)
        {
            if (m_Wrapper.m_StartMenuActionsCallbackInterface != null)
            {
                @CursorUp.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCursorUp;
                @CursorUp.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCursorUp;
                @CursorUp.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCursorUp;
                @CursorDown.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCursorDown;
                @CursorDown.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCursorDown;
                @CursorDown.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCursorDown;
                @Cancel.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnCancel;
                @Accept.started -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_StartMenuActionsCallbackInterface.OnAccept;
            }
            m_Wrapper.m_StartMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CursorUp.started += instance.OnCursorUp;
                @CursorUp.performed += instance.OnCursorUp;
                @CursorUp.canceled += instance.OnCursorUp;
                @CursorDown.started += instance.OnCursorDown;
                @CursorDown.performed += instance.OnCursorDown;
                @CursorDown.canceled += instance.OnCursorDown;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
            }
        }
    }
    public StartMenuActions @StartMenu => new StartMenuActions(this);

    // Testing
    private readonly InputActionMap m_Testing;
    private ITestingActions m_TestingActionsCallbackInterface;
    private readonly InputAction m_Testing_CancelMenu;
    public struct TestingActions
    {
        private @PlayerControls m_Wrapper;
        public TestingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CancelMenu => m_Wrapper.m_Testing_CancelMenu;
        public InputActionMap Get() { return m_Wrapper.m_Testing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestingActions set) { return set.Get(); }
        public void SetCallbacks(ITestingActions instance)
        {
            if (m_Wrapper.m_TestingActionsCallbackInterface != null)
            {
                @CancelMenu.started -= m_Wrapper.m_TestingActionsCallbackInterface.OnCancelMenu;
                @CancelMenu.performed -= m_Wrapper.m_TestingActionsCallbackInterface.OnCancelMenu;
                @CancelMenu.canceled -= m_Wrapper.m_TestingActionsCallbackInterface.OnCancelMenu;
            }
            m_Wrapper.m_TestingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CancelMenu.started += instance.OnCancelMenu;
                @CancelMenu.performed += instance.OnCancelMenu;
                @CancelMenu.canceled += instance.OnCancelMenu;
            }
        }
    }
    public TestingActions @Testing => new TestingActions(this);
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    private int m_KBMSchemeIndex = -1;
    public InputControlScheme KBMScheme
    {
        get
        {
            if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.FindControlSchemeIndex("KBM");
            return asset.controlSchemes[m_KBMSchemeIndex];
        }
    }
    public interface ITownActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnQuickPsynergy1(InputAction.CallbackContext context);
        void OnQuickPsynergy2(InputAction.CallbackContext context);
        void OnOpenGeneralMenu(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnStartMenu(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IOverworldActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnQuickPsynergy1(InputAction.CallbackContext context);
        void OnQuickPsynergy2(InputAction.CallbackContext context);
        void OnOpenGeneralMenu(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnStartMenu(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IGeneralMenuActions
    {
        void OnCursorLeft(InputAction.CallbackContext context);
        void OnCursorRight(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
    }
    public interface IStartMenuActions
    {
        void OnCursorUp(InputAction.CallbackContext context);
        void OnCursorDown(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
    }
    public interface ITestingActions
    {
        void OnCancelMenu(InputAction.CallbackContext context);
    }
}
