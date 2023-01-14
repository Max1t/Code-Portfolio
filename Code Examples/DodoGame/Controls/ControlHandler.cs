using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ControlHandler : MonoBehaviour
{


    public delegate void OnControlChanged();
    public OnControlChanged OnControlsChangedCallback;

    public KeyboardControl _keyboardControl;
    public GamepadControl _gamepadControl;

    [SerializeField] private bool _keyboardOn;
    [SerializeField] private bool _gamepadOn;
    private bool _touchHeld = false;
    public bool TouchHeld { get => _touchHeld; set => _touchHeld = value; }

    public Camera _camera;



    public bool CurrentControlTouchHeld()
    {
        if (_keyboardOn) return _keyboardControl.HoldingTouch;
        if (_gamepadOn) return _gamepadControl.HoldingTouch;
        return false;
    }

    public bool CurrentControlState()
    {
        if (_keyboardOn) return _keyboardControl.On;
        if (_gamepadOn) return _gamepadControl.On;
        return false;
    }

    public bool KeyboardOn
    {
        get => _keyboardOn;
        set
        {
            _keyboardOn = value;
            if (OnControlsChangedCallback != null) OnControlsChangedCallback.Invoke();
        }
    }

    public bool GamepadOn
    {
        get => _gamepadOn;
        set
        {
            _gamepadOn = value;
            if (OnControlsChangedCallback != null) OnControlsChangedCallback.Invoke();
        }
    }


    public void updateControls()
    {
        if (KeyboardOn && !_keyboardControl.enabled) _keyboardControl.enabled = true;
        else if (!KeyboardOn && _keyboardControl.enabled) _keyboardControl.enabled = false;

        if (GamepadOn && !_gamepadControl.enabled) _gamepadControl.enabled = true;
        else if (!GamepadOn && _gamepadControl.enabled) _gamepadControl.enabled = false;
    }


    public void EnableCurrentControls(InputUser user)
    {
        if (_keyboardOn) _keyboardControl.EnableControlsUser(user);
        if (_gamepadOn) _gamepadControl.EnableControlsUser(user);
    }


    public void DisableCurrentControls(InputUser user)
    {
        if (_keyboardOn) _keyboardControl.DisableControlsUser(user);
        if (_gamepadOn) _gamepadControl.DisableControlsUser(user);
    }

    void OnEnable()
    {
        OnControlsChangedCallback += updateControls;
        if (OnControlsChangedCallback != null) OnControlsChangedCallback.Invoke();
    }

    void OnDisable()
    {
        OnControlsChangedCallback -= updateControls;
    }

    public void toggleKeyboard()
    {
        if (KeyboardOn) KeyboardOn = false;
        else KeyboardOn = true;
    }

    public void toggleGamepad()
    {
        if (GamepadOn) GamepadOn = false;
        else GamepadOn = true;
    }

    public void CreateControls(InputDevice device, InputUser user, bool tkp)
    {
        switch (device.description.deviceClass)
        {
            case (""):
                KeyboardOn = false;
                GamepadOn = true;
                _gamepadControl.InitializeControlsForPlayer(user);
                break;
            case ("Keyboard"):
                GamepadOn = false;
                KeyboardOn = true;
                if (tkp)
                {
                    _keyboardControl.InitializeControlsForPlayer2(user);
                    break;
                }
                _keyboardControl.InitializeControlsForPlayer(user);
                break;
        }
    }
//
//
    ///// <param name="Device">: 
    /////     0 == keyboard 
    /////     0 == gamepad </param>
    //public void InitSinglePlayerControls(DeviceType Device)
    //{
    //    if (Device == DeviceType.Keyboard)
    //    {
    //        GamepadOn = false;
    //        KeyboardOn = true;
    //        _keyboardControl.EnableControls();
    //    }
    //    if (Device == DeviceType.Gamepad)
    //    {
    //        KeyboardOn = false;
    //        GamepadOn = true;
    //        _gamepadControl.EnableControls();
    //    }
    //}
}
