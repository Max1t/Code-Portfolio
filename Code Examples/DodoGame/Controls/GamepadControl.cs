using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

public class GamepadControl : MonoBehaviour
{

    [SerializeField] private PlayerInteraction _player;
    [SerializeField] private PlayerMovement _playerMovement;
    private PlayerControl _control;
    private Vector2 _leftStickMovement;
    private Vector2 _rightStickMovement;

    public bool MovingPlayer = true;

    public Vector2 RightStickMovement { get => _rightStickMovement; }
    public Vector2 LeftStickMovement { get => _leftStickMovement; }

    private List<InputDevice> _inputDevicesList;
    private List<PlayerControl.ControllerActions> _players;


    private bool on;

    public bool On { get => on; set => on = value; }


    private bool _holdingTouch = false;

    public bool HoldingTouch { get => _holdingTouch; set => _holdingTouch = value; }

    void Awake()
    {
        _control = new PlayerControl();
    }

    public void InitializeControlsForPlayer(InputUser user)
    {
        _control = new PlayerControl();
        user.AssociateActionsWithUser(_control);
        var scheme = InputControlScheme.FindControlSchemeForDevice
                        (user.pairedDevices[0],
                         _control.controlSchemes);

        user.ActivateControlScheme(scheme.Value);
        PlayerControl actions = (PlayerControl)user.actions;
        actions.Controller.Start.performed += OnEscape;

        EnableControlsUser(user);
    }

    //public void EnableControls()
    //{
    //    _control.Controller.LeftStick.performed += OnMove;
    //    _control.Controller.LeftStick.canceled += OnStop;
    //
    //    _control.Controller.RightStick.performed += OnRightStick;
    //    _control.Controller.RightStick.canceled += OnStop;
    //
    //
    //    _control.Controller.Interact.performed += OnInteract;
    //    _control.Controller.Touch.performed += OnTouch;
    //    _control.Controller.Start.performed += OnEscape;
    //
    //    _control.Controller.Enable();
    //
    //
    //    On = true;
    //}
    //
    //public void DisableControls()
    //{
    //    _control.Controller.LeftStick.performed -= OnMove;
    //    _control.Controller.LeftStick.canceled -= OnStop;
    //
    //    _control.Controller.RightStick.performed -= OnRightStick;
    //    _control.Controller.RightStick.canceled -= OnStop;
    //
    //    _control.Controller.Interact.performed -= OnInteract;
    //
    //
    //    _control.Controller.Disable();
    //    _control.Controller.Start.Enable();
    //
    //    _leftStickMovement = Vector2.zero;
    //    On = false;
    //}

    public void EnableControlsUser(InputUser user)
    {
        PlayerControl actions = (PlayerControl)user.actions;
        actions.Controller.LeftStick.performed += OnMove;
        actions.Controller.LeftStick.canceled += OnStop;

        actions.Controller.RightStick.performed += OnRightStick;
        actions.Controller.RightStick.canceled += OnStop;


        actions.Controller.Interact.performed += OnInteract;
        actions.Controller.Touch.performed += OnTouch;

        actions.Controller.Select.performed += OnSelect;

        actions.Controller.Start.Disable();

        actions.Controller.Enable();
        On = true;
    }

    public void DisableControlsUser(InputUser user)
    {
        PlayerControl actions = (PlayerControl)user.actions;
        actions.Controller.LeftStick.performed -= OnMove;
        actions.Controller.LeftStick.canceled -= OnStop;

        actions.Controller.RightStick.performed -= OnRightStick;
        actions.Controller.RightStick.canceled -= OnStop;

        actions.Controller.Interact.performed -= OnInteract;

        actions.Controller.Select.performed -= OnSelect;

        actions.Controller.Disable();
        actions.Controller.Start.Enable();

        _leftStickMovement = Vector2.zero;
        On = false;
    }

    // -------------------------------------------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------- //

    void Update()
    {
        if (MovingPlayer)
            _playerMovement.Move(LeftStickMovement);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        _leftStickMovement = context.ReadValue<Vector2>(); // Read x and y axis of the stick 

    }

    void OnStop(InputAction.CallbackContext context)
    {
        _leftStickMovement = Vector2.zero;
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        _player.Interact();
    }

    void OnTouch(InputAction.CallbackContext context)
    {
        if (context.interaction is TapInteraction)
        {
            _holdingTouch = false;
            _player.Touch();
        }
        if (context.interaction is PressInteraction)
        {
            if (!_holdingTouch) _holdingTouch = true;
            else _holdingTouch = false;
            _player.Touch();
        }
    }

    void OnRightStick(InputAction.CallbackContext context)
    {
        _rightStickMovement = context.ReadValue<Vector2>();
    }

    void OnRightStickStop(InputAction.CallbackContext context)
    {
        _rightStickMovement = Vector2.zero;
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        if (GameManager._gameManager.TutorialActive)
        {
            GameManager._gameManager.tutorialManager.EndTutorial();
        }
        else if(GameManager._gameManager.GameRunning) GameManager._gameManager.TogglePause();
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        GameManager._gameManager.uiManager.ToggleDevMenu();
    }

}
