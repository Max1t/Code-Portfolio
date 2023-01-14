using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DodoInteractable :  Touchable
{
    [SerializeField] private StatePatternDodo _dodo;

    private GameObject _progressBar;
    private bool _touched = false;
    private bool _progressBarRunning = false;
    [SerializeField] private float _touchTimer = 0;
    [SerializeField] private float _timeToTouch = 0;

    private void Awake()
    {
        _dodo = GetComponent<StatePatternDodo>();
    }

    //Could return false if you pick up too many times for example
    public override bool Interact(PlayerInteraction player)
    {
        Debug.Log("trying to pick up.");
        if (_dodo.isEgg)
        {
            return false;
        }
        if (!_dodo.isDying)
        {
            if (_dodo.isCarried && player != _dodo.playerCarrying)
            {
                _dodo.playerCarrying.LoseInteractable();
                player.PickUpInteractable(this.transform);
                _dodo.playerCarrying = player;
            }
            else if (!_dodo.isCarried)
            {
                if (_dodo.currentState == _dodo.idleState && !_dodo.isCarried)
                {
                    _dodo.IdleStateCarry(true);
                    Debug.Log("Idle carry true");
                    
                }
                _dodo.isCarried = true;
                _dodo.playerCarrying = player;
                player.PickUpInteractable(this.transform);
            }
            else
            {
                if (_dodo.currentState == _dodo.idleState && _dodo.isCarried)
                {
                    _dodo.IdleStateCarry(false);
                    Debug.Log("Idle carry false");
                } 
                _dodo.playerCarrying = null;
                Debug.Log("put dodo down." + _dodo.currentState);
                _dodo.isCarried = false;
                player.DropInteractable();
            }

            return true;
        }
        return false;
    }

    public override bool Touch(PlayerInteraction player)
    {
        _dodo.GetComponent<DodoHeadTurn>().SetLookTarget(player.transform);
        if (_dodo.currentState == _dodo.carriedState && player._control.CurrentControlTouchHeld())
        {
            if (_dodo.poisonBerryDebuff || _dodo.mushroomDebuff || (_dodo.isBaby && _dodo.hunger > 0) || _dodo.beeStungDebuff)
            {
                _touched = true;
            }
        }
        else if (_dodo.currentState == _dodo.carriedState && !player._control.CurrentControlTouchHeld())
        {
            _touched = false;
        }
        // if (_dodo.isBaby && _dodo.currentState == _dodo.carriedState)
        // {
        //     _dodo.hunger -= 15;
        // }
        if (_dodo.currentState == _dodo.roamState || _dodo.currentState == _dodo.depressedState)
        {
            _dodo.currentState = _dodo.pettingState;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (_touched) _touchTimer += Time.deltaTime;
        else if (!_touched && _touchTimer > 0) _touchTimer = 0;

        if (_touchTimer > _timeToTouch)
        {
            _touched = false;
            Debug.Log("Dodo healed/Fed");
            if (_dodo.isBaby)
            {
                _dodo.hunger -= DodoSettings.Baby.HungerLose;
            }
            else
            {
                _dodo.HealDebuffs();
            }
            _touchTimer = 0;
            _progressBar = null;
            _progressBarRunning = false;
        }

        if (_touched && !_progressBarRunning)
        {
            _progressBar = GameManager._gameManager.GetProgressBar(2, _dodo.hoverText.transform);
            _progressBarRunning = true;
        }

        if (!_touched && _progressBarRunning)
        {
            _progressBar?.GetComponent<ProgressBar>().StopProgressBar();
            _progressBarRunning = false;
            _progressBar = null;
        }
    }


}
