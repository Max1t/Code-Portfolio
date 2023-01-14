using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Interactions;
using System;

public class KeyboardControl : MonoBehaviour
{


    [SerializeField] private PlayerInteraction _player;
    [SerializeField] private PlayerMovement _playerMovement;
    private PlayerControl _control; // New input system class
    private Vector2 _movement;

    private bool on;

    private bool _holdingTouch = false;

    public bool On { get => on; set => on = value; }

    public bool HoldingTouch { get => _holdingTouch; set => _holdingTouch = value; }

    void Awake()
    {
        _control = new PlayerControl();
    }


    public void InitializeControlsForPlayer2(InputUser user)
    {
        _control = new PlayerControl();
        user.AssociateActionsWithUser(_control);
        var scheme = InputControlScheme.FindControlSchemeForDevice
                        (user.pairedDevices[0],
                         _control.controlSchemes);
        user.ActivateControlScheme(scheme.Value);
        PlayerControl actions = (PlayerControl)user.actions;
        EnableControlsUser2(user);
    }


    public void EnableControlsUser2(InputUser user)
    {
        PlayerControl actions = (PlayerControl)user.actions;
        actions.Keyboard.Arrows.performed += OnMove;
        actions.Keyboard.Arrows.canceled += OnStop;
        actions.Keyboard.InteractP2.performed += OnInteract;
        actions.Keyboard.TouchP2.performed += OnTouch;

        if (!actions.Keyboard.enabled) actions.Keyboard.Enable();
        On = true;
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
        actions.Keyboard.Escape.performed += OnEscape;

        EnableControlsUser(user);
    }

    // public void EnableControls()
    // {
    //     _control.Keyboard.WASD.performed += OnMove;
    //     _control.Keyboard.WASD.canceled += OnStop;
    //     _control.Keyboard.Interact.performed += OnInteract;
    //     _control.Keyboard.Touch.performed += OnTouch;
    //     _control.Keyboard.Escape.performed += OnEscape;
    //
    //     _control.Keyboard.Enable();
    //     On = true;
    // }
    //
    //
    // public void DisableControls()
    // {
    //     _control.Keyboard.WASD.performed -= OnMove;
    //     _control.Keyboard.WASD.canceled -= OnStop;
    //     _control.Keyboard.Interact.performed -= OnInteract;
    //     _control.Keyboard.Touch.performed -= OnTouch;
    //
    //     _control.Keyboard.Disable();
    //     _control.Keyboard.Escape.Enable();
    //
    //     _movement = Vector2.zero;
    //     On = false;
    // }

    public void EnableControlsUser(InputUser user)
    {

        PlayerControl actions = (PlayerControl)user.actions;
        actions.Keyboard.WASD.performed += OnMove;
        actions.Keyboard.WASD.canceled += OnStop;
        actions.Keyboard.Interact.performed += OnInteract;
        actions.Keyboard.Touch.performed += OnTouch;

        actions.Keyboard.Escape.Disable();
        actions.Keyboard.Enable();
        On = true;
    }

    public void DisableControlsUser(InputUser user)
    {
        PlayerControl actions = (PlayerControl)user.actions;
        actions.Keyboard.WASD.performed -= OnMove;
        actions.Keyboard.WASD.canceled -= OnStop;
        actions.Keyboard.Interact.performed -= OnInteract;
        actions.Keyboard.Touch.performed -= OnTouch;

        actions.Keyboard.Disable();
        actions.Keyboard.Escape.Enable();

        _movement = Vector2.zero;
        On = false;
    }



    // -------------------------------------------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------- //

    void Update()
    {
        _playerMovement.Move(_movement);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>(); // Read x and y axis of the keys pressed 
    }

    void OnStop(InputAction.CallbackContext context)
    {
        _movement = Vector2.zero;
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


    private void OnEscape(InputAction.CallbackContext obj)
    {
        if (GameManager._gameManager.TutorialActive)
        {
            GameManager._gameManager.tutorialManager.EndTutorial();
        }
        else if(GameManager._gameManager.GameRunning) GameManager._gameManager.TogglePause();
    }
}
