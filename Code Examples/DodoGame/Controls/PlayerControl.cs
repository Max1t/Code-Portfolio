// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""97336343-767a-4342-910c-d01adf876493"",
            ""actions"": [
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Button"",
                    ""id"": ""a35132fd-c0f3-4277-ba71-9ad40c23bb81"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Button"",
                    ""id"": ""0302ec1c-007a-42d0-a269-7c55f570aa29"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""093331e8-b9b9-45c3-a912-c57c4063337e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""ade780a7-3e6e-4023-a658-0a354eb8b20c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Touch"",
                    ""type"": ""Button"",
                    ""id"": ""64351117-a862-43fc-8a2c-a070346fc914"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""07c28cc5-34a0-41be-a8b7-7935eb23c521"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""2c6b7db5-191e-4d4c-8f88-f90b29170aed"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""10127109-5d7c-4f14-8a10-6e2d678f56e8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58df7125-26bc-4471-b93d-353a3d7125c5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bd7adc7-14ab-4c14-b639-5336cfb5a992"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb893d6d-4990-4def-a240-a07afd73d2ea"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press(behavior=2),Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08873e2f-15b2-4300-a6eb-f3be074e4953"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e2b4d24-21c2-4d1b-be2f-4ef43174ef89"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5fd9499-58c4-459c-86a1-aba26335d420"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Global"",
                    ""action"": ""InteractDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d36a8817-187a-4b05-ac42-cf21fb751e30"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Global"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1ff8401-74f6-44d3-a190-7c54232d956f"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Global"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""id"": ""5d908b14-f9bf-4e89-909f-a09cf4a21d5b"",
            ""actions"": [
                {
                    ""name"": ""WASD"",
                    ""type"": ""Button"",
                    ""id"": ""05d4f4d8-3f9f-4d56-903b-4114833b121d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arrows"",
                    ""type"": ""Button"",
                    ""id"": ""7191223d-469a-4364-8861-ad138330edbd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""09020845-84ab-491f-b9f1-60abcff1e40a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""InteractP2"",
                    ""type"": ""Button"",
                    ""id"": ""1aa809c9-f68f-45b9-98a5-18670c6e7cdb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""e5b95a61-15b3-4d15-8d9d-18026997c8de"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Touch"",
                    ""type"": ""Value"",
                    ""id"": ""0348629b-731b-4642-b77e-e170250a4ff2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchP2"",
                    ""type"": ""Button"",
                    ""id"": ""e2c73422-72c3-4f2f-950a-a3ea596b9d52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""3939a2c9-e1a9-4ede-9c8d-f1de2252daaf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3a949815-0bc7-49ce-9cd9-30ecf2fc0622"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f598340-e1fc-45e6-84eb-c09863b47dfb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""97e1a1c0-2731-4a7e-9294-9b1ca231d9d7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f16d0333-d009-4d93-910b-06594769d7be"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4576e7a2-a0e4-42fc-95cd-1ab876db0fc6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ef60636b-d5d4-47d1-84b3-fdfa314c8e13"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""926a19c2-57c5-4892-8f18-6c3a8eccba9d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press(behavior=2),Tap"",
                    ""processors"": """",
                    ""groups"": ""Global"",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ca05ae6-b0b7-4f84-a66a-993bd69ecd51"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f16ea318-9f9e-4ebb-a847-96077dd070d5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrows"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a4533354-9f9e-4ce8-afe9-9fb173df9e41"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cbf2d2df-e7ab-4447-b3e8-be38d8dac337"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""51d39845-c232-4b4c-a155-75e42ddeb126"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d40a442d-bf94-4ef8-b1c4-36fb168c161f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5b75dca8-caad-4351-8a9a-2b5510882e52"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e493482-fbb9-48d9-97b8-3c42c592cbfc"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": ""Press(behavior=2),Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchP2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1ebc013-0589-44f8-b681-b7fd1d3f4e06"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Global"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Global"",
            ""bindingGroup"": ""Global"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_RightStick = m_Controller.FindAction("RightStick", throwIfNotFound: true);
        m_Controller_LeftStick = m_Controller.FindAction("LeftStick", throwIfNotFound: true);
        m_Controller_Interact = m_Controller.FindAction("Interact", throwIfNotFound: true);
        m_Controller_InteractDialogue = m_Controller.FindAction("InteractDialogue", throwIfNotFound: true);
        m_Controller_Touch = m_Controller.FindAction("Touch", throwIfNotFound: true);
        m_Controller_X = m_Controller.FindAction("X", throwIfNotFound: true);
        m_Controller_Start = m_Controller.FindAction("Start", throwIfNotFound: true);
        m_Controller_Select = m_Controller.FindAction("Select", throwIfNotFound: true);
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_WASD = m_Keyboard.FindAction("WASD", throwIfNotFound: true);
        m_Keyboard_Arrows = m_Keyboard.FindAction("Arrows", throwIfNotFound: true);
        m_Keyboard_Interact = m_Keyboard.FindAction("Interact", throwIfNotFound: true);
        m_Keyboard_InteractP2 = m_Keyboard.FindAction("InteractP2", throwIfNotFound: true);
        m_Keyboard_InteractDialogue = m_Keyboard.FindAction("InteractDialogue", throwIfNotFound: true);
        m_Keyboard_Touch = m_Keyboard.FindAction("Touch", throwIfNotFound: true);
        m_Keyboard_TouchP2 = m_Keyboard.FindAction("TouchP2", throwIfNotFound: true);
        m_Keyboard_Escape = m_Keyboard.FindAction("Escape", throwIfNotFound: true);
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

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_RightStick;
    private readonly InputAction m_Controller_LeftStick;
    private readonly InputAction m_Controller_Interact;
    private readonly InputAction m_Controller_InteractDialogue;
    private readonly InputAction m_Controller_Touch;
    private readonly InputAction m_Controller_X;
    private readonly InputAction m_Controller_Start;
    private readonly InputAction m_Controller_Select;
    public struct ControllerActions
    {
        private @PlayerControl m_Wrapper;
        public ControllerActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightStick => m_Wrapper.m_Controller_RightStick;
        public InputAction @LeftStick => m_Wrapper.m_Controller_LeftStick;
        public InputAction @Interact => m_Wrapper.m_Controller_Interact;
        public InputAction @InteractDialogue => m_Wrapper.m_Controller_InteractDialogue;
        public InputAction @Touch => m_Wrapper.m_Controller_Touch;
        public InputAction @X => m_Wrapper.m_Controller_X;
        public InputAction @Start => m_Wrapper.m_Controller_Start;
        public InputAction @Select => m_Wrapper.m_Controller_Select;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @RightStick.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStick;
                @RightStick.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStick;
                @RightStick.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnRightStick;
                @LeftStick.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnLeftStick;
                @Interact.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @InteractDialogue.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteractDialogue;
                @InteractDialogue.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteractDialogue;
                @InteractDialogue.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteractDialogue;
                @Touch.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnTouch;
                @X.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnX;
                @Start.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStart;
                @Select.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightStick.started += instance.OnRightStick;
                @RightStick.performed += instance.OnRightStick;
                @RightStick.canceled += instance.OnRightStick;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @InteractDialogue.started += instance.OnInteractDialogue;
                @InteractDialogue.performed += instance.OnInteractDialogue;
                @InteractDialogue.canceled += instance.OnInteractDialogue;
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_WASD;
    private readonly InputAction m_Keyboard_Arrows;
    private readonly InputAction m_Keyboard_Interact;
    private readonly InputAction m_Keyboard_InteractP2;
    private readonly InputAction m_Keyboard_InteractDialogue;
    private readonly InputAction m_Keyboard_Touch;
    private readonly InputAction m_Keyboard_TouchP2;
    private readonly InputAction m_Keyboard_Escape;
    public struct KeyboardActions
    {
        private @PlayerControl m_Wrapper;
        public KeyboardActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @WASD => m_Wrapper.m_Keyboard_WASD;
        public InputAction @Arrows => m_Wrapper.m_Keyboard_Arrows;
        public InputAction @Interact => m_Wrapper.m_Keyboard_Interact;
        public InputAction @InteractP2 => m_Wrapper.m_Keyboard_InteractP2;
        public InputAction @InteractDialogue => m_Wrapper.m_Keyboard_InteractDialogue;
        public InputAction @Touch => m_Wrapper.m_Keyboard_Touch;
        public InputAction @TouchP2 => m_Wrapper.m_Keyboard_TouchP2;
        public InputAction @Escape => m_Wrapper.m_Keyboard_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @WASD.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnWASD;
                @WASD.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnWASD;
                @WASD.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnWASD;
                @Arrows.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnArrows;
                @Arrows.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnArrows;
                @Arrows.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnArrows;
                @Interact.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @InteractP2.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteractP2;
                @InteractP2.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteractP2;
                @InteractP2.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteractP2;
                @InteractDialogue.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteractDialogue;
                @InteractDialogue.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteractDialogue;
                @InteractDialogue.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteractDialogue;
                @Touch.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTouch;
                @TouchP2.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTouchP2;
                @TouchP2.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTouchP2;
                @TouchP2.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTouchP2;
                @Escape.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEscape;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @Arrows.started += instance.OnArrows;
                @Arrows.performed += instance.OnArrows;
                @Arrows.canceled += instance.OnArrows;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @InteractP2.started += instance.OnInteractP2;
                @InteractP2.performed += instance.OnInteractP2;
                @InteractP2.canceled += instance.OnInteractP2;
                @InteractDialogue.started += instance.OnInteractDialogue;
                @InteractDialogue.performed += instance.OnInteractDialogue;
                @InteractDialogue.canceled += instance.OnInteractDialogue;
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
                @TouchP2.started += instance.OnTouchP2;
                @TouchP2.performed += instance.OnTouchP2;
                @TouchP2.canceled += instance.OnTouchP2;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    private int m_GlobalSchemeIndex = -1;
    public InputControlScheme GlobalScheme
    {
        get
        {
            if (m_GlobalSchemeIndex == -1) m_GlobalSchemeIndex = asset.FindControlSchemeIndex("Global");
            return asset.controlSchemes[m_GlobalSchemeIndex];
        }
    }
    public interface IControllerActions
    {
        void OnRightStick(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInteractDialogue(InputAction.CallbackContext context);
        void OnTouch(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnWASD(InputAction.CallbackContext context);
        void OnArrows(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInteractP2(InputAction.CallbackContext context);
        void OnInteractDialogue(InputAction.CallbackContext context);
        void OnTouch(InputAction.CallbackContext context);
        void OnTouchP2(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
    }
}
