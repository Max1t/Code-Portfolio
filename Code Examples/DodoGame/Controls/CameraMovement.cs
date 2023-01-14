using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform _target = null;
    private Vector3 _targetPos;
    [SerializeField] private float smoothing = 2f;
    private Vector3 nullVelocity = Vector3.zero;
    private Vector3 _offset;
    [SerializeField] Camera attachedCamera;

    [Range(1, 10)]
    public float _cameraZ;
    [Range(1, 10)]
    public float _cameraY;
    [Range(-10, 10)]
    public float _cameraX;
    [Range(0, 90)]
    public float _cameraRot;

    public float _spcXOffset = 0; // Splitscreen offset
    public float _spcYOffset = 0; // Splitscreen offset
    public float _spcZOffset = 0; // Splitscreen offset

    public Vector3 _relativePos;

    public bool _Ready = false;

    private void OnValidate()
    {
        if (_target)
        {
            SetPos();
            calcOffset();
        }
    }

    public void calcOffset()
    {
        _offset = _targetPos - _target.position;
    }

    public void SetPos()
    {
        transform.position = new Vector3(_target.position.x + _cameraX,
                                 _target.position.y + _cameraY,
                                 _target.position.z + _cameraZ);
        transform.rotation = Quaternion.identity;
        transform.Rotate(_cameraRot, 180, 0);
    }

    void Update()
    {
        if (_target)
        {
            _relativePos = _target.position + _offset;
            Vector3 targetPosition = _target.position + _offset;
            if (_spcXOffset != 0) targetPosition += new Vector3(_spcXOffset, _spcYOffset, 0);

            //Not correct use of lerp öh well
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_cameraRot + _spcZOffset, 180, 0), 0.1f);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref nullVelocity, smoothing);
        }
    }

    public void TargetObject(Transform target, bool rotate)
    {
        //_Ready = false;
        _target = target;
        _targetPos = new Vector3(target.position.x + _cameraX,
                                         target.position.y + _cameraY,
                                         target.position.z + _cameraZ);
        if (rotate)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(_cameraRot, 180, 0);
        }
        calcOffset();
        //if (gameObject.activeSelf) StartCoroutine(MoveToTarget(_target.position + _offset));
        //else
        //{
        //    SetPos();
        //    _Ready = true;sa
        //}

    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (!_Ready)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref nullVelocity, smoothing);
            if (transform.position == targetPosition) _Ready = true;
            yield return null;
        }
    }
}
