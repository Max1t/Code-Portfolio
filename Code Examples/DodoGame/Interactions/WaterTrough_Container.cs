using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterTrough_Container : MonoBehaviour
{
    [SerializeField] float _yMin;
    [SerializeField] float _yMax;
    [SerializeField] float _waterAmount;
    [SerializeField] float _waterMax;
    [SerializeField] GameObject _surface;
    [SerializeField] private GameObject _exclamationMark;

    private Vector3 _startPos;
    private GameManager _manager;

    void OnEnable()
    {
        _waterAmount = 0;
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

    public bool AddWater(float amount)
    {
        if (_waterAmount == 0)
        {
            _exclamationMark.SetActive(false);
            var waterSpots = GetComponentsInChildren<Transform>().ToList().FindAll(trans => trans.tag == "Water");
            foreach (var spot in waterSpots)
            {
                _manager.waterPlaces[spot.gameObject] = false;
            }
        }
        _waterAmount += amount;
        if (_waterAmount > _waterMax)
        {
            _waterAmount = _waterMax;
        }
        UpdateTrough();
        return true;
    }

    public bool DecreaseWater(float amount)
    {
        _waterAmount -= amount;
        if (_waterAmount <= 0)
        {
            _exclamationMark.SetActive(true);
            var waterSpots = GetComponentsInChildren<Transform>().ToList().FindAll(trans => trans.tag == "Water");
            foreach (var spot in waterSpots)
            {
                _manager.waterPlaces[spot.gameObject] = true;
            }
            _waterAmount = 0;
            return false;
        }
        UpdateTrough();
        return true;
    }

    private void UpdateTrough()
    {
        if (_waterAmount > 0)
        {
            _surface.transform.localPosition = new Vector3(_startPos.x, Mathf.Lerp(_yMin, _yMax, (_waterAmount / _waterMax)), _startPos.z);
        }
        else
        {
            _surface.transform.localPosition = new Vector3(_startPos.x, _yMin, _startPos.z);
        }
    }
}

