using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodTrough_Container : MonoBehaviour
{
    [SerializeField] float _yMin;
    [SerializeField] float _yMax;
    [SerializeField] float _foodAmount;
    [SerializeField] float _foodMax;
    [SerializeField] GameObject _surface;
    [SerializeField] private GameObject _exclamationMark;
    [SerializeField] private GameObject _dinnerBell;
    
    private Vector3 _startPos;
    private GameManager _manager;

    void OnEnable()
    {
        _foodAmount = 0;
        _exclamationMark.SetActive(true);
        UpdateTrough();
    }
    void Start()
    {
        if (_manager == null)
        {
            _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        _startPos = _surface.transform.localPosition;
        UpdateTrough();
    }

    public GameObject GetDinnerBell()
    {
        return _dinnerBell;
    }

    public bool AddFood(float amount)
    {
        if (_foodAmount == 0)
        {
            _exclamationMark.SetActive(false);
            var foodSpots = GetComponentsInChildren<Transform>().ToList().FindAll(trans => trans.gameObject.tag == "Food");
            foreach (var spot in foodSpots)
            {
                _manager.foodPlaces[spot.gameObject] = false;
            }
        }
        _foodAmount += amount;
        if (_foodAmount > _foodMax)
        {
            _foodAmount = _foodMax;
        }
        UpdateTrough();
        return true;
    }

    public bool DecreaseFood(float amount)
    {
        _foodAmount -= amount;
        if(_foodAmount <= 0)
        {
            _exclamationMark.SetActive(true);
            var foodSpots = GetComponentsInChildren<Transform>().ToList().FindAll(trans => trans.gameObject.tag == "Food");
            foreach (var spot in foodSpots)
            {
                _manager.foodPlaces[spot.gameObject] = true;
            }
            _foodAmount = 0;
            return false;
        }
        UpdateTrough();
        return true;
    }

    private void UpdateTrough()
    {
        if (_foodAmount > 0)
        {
            _surface.transform.localPosition = new Vector3(_startPos.x, Mathf.Lerp(_yMin, _yMax, (_foodAmount / _foodMax)), _startPos.z);
            _surface.transform.localScale = new Vector3(Mathf.Lerp(0.3f,1, (_foodAmount /_foodMax)), 1, 1);
        }
        else
        {
            _surface.transform.localPosition = new Vector3(_startPos.x, _yMin, _startPos.z);
        }
    }
}
