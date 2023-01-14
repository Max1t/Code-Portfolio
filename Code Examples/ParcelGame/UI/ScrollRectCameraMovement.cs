using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRectCameraMovement : MonoBehaviour
{

    [SerializeField]
    private ParcelStack3D _stackController;

    [SerializeField]
    private Transform _startPos;

    [SerializeField]
    private Transform _endPos;

    public bool _playerOneStackSpawned = false;
    public bool _playerTwoStackSpawned = false;
    public bool _playerThreeStackSpawned = false;


    public void OnScrollRectValueChanged(Vector2 values)
    {

        float xMovement = Mathf.Lerp(_startPos.position.x, _endPos.position.x, values.x);
        transform.position = new Vector3(xMovement, transform.position.y, transform.position.z);

        if (xMovement > -5 && _playerOneStackSpawned == false)
        {
            _stackController.SpawnStack(1);
            _playerOneStackSpawned = true;
        }


        if (xMovement > 1 && _playerTwoStackSpawned == false)
        {
            _stackController.SpawnStack(2);
            _playerTwoStackSpawned = true;
        }


        if (xMovement > 7 && _playerThreeStackSpawned == false)
        {
            _stackController.SpawnStack(3);
            _playerThreeStackSpawned = true;
        }

    }


}
