using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public GameObject _fireExplosionAnimation;
    public GameObject _ammoExplosionAnimation;
    public ResourcesScriptUI _resources;
    public Matches _matches;
    public RoomHpController _roomHpController;
    private Vector3 _targetPosition;
    private bool _move = false;
    private bool _hit = true;
    private float _speed = 2f;
    private float _damage = 15;
    private string _room = "";

    // Start is called before the first frame update
    void Start()
    {
        _resources = FindObjectOfType<ResourcesScriptUI>();
        _matches = FindObjectOfType<Matches>();
        _roomHpController = FindObjectOfType<RoomHpController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_move)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, _targetPosition) < 0.001f)
        {
            int rand = Random.Range(0, 10);
            Destroy(Instantiate(_ammoExplosionAnimation, gameObject.transform.position, Quaternion.identity), 0.5f);
            if(_hit)
            {
                if (AirshipStats.battlePause == false)
                {

                    if (AirshipStats.specialBattle == false)
                    {
                        if (AirshipStats.badBttleStart == false)
                        {
                            if (AirshipStats.enemyGunBroken == 0)
                            {
                                if (rand == 4) //10% chance for fire
                                    SpawnFire();
                                int random = Random.Range(0, 11);
                                Debug.Log("random damage: " + random);
                                _roomHpController.RoomTookDamage(_room);
                                _resources.TakeDamage(random);

                            }

                            if (AirshipStats.enemyGunBroken == 1)
                            {
                                if (rand == 4) //10% chance for fire
                                    SpawnFire();
                                int random = Random.Range(0, 8);
                                Debug.Log("random damage: " + random);
                                _roomHpController.RoomTookDamage(_room);
                                _resources.TakeDamage(random);
                            }

                            if (AirshipStats.enemyGunBroken == 2)
                            {
                                if (rand == 4) //10% chance for fire
                                    SpawnFire();
                                int random = Random.Range(0, 5);
                                Debug.Log("random damage: " + random);
                                _roomHpController.RoomTookDamage(_room);
                                _resources.TakeDamage(random);

                            }
                        }
                        else
                        {
                            if (AirshipStats.enemyGunBroken == 0)
                            {
                                if (rand == 4) //10% chance for fire
                                    SpawnFire();
                                int random = Random.Range(15, 25);
                                Debug.Log("random damage: " + random);
                                _roomHpController.RoomTookDamage(_room);
                                _resources.TakeDamage(random);

                            }
                            if (AirshipStats.enemyGunBroken == 1)
                            {


                                if (rand == 4) //10% chance for fire
                                    SpawnFire();
                                int random = Random.Range(10, 20);
                                Debug.Log("random damage: " + random);
                                _roomHpController.RoomTookDamage(_room);
                                _resources.TakeDamage(random);

                            }
                            if (AirshipStats.enemyGunBroken == 2)
                            {
                                if (rand == 4) //10% chance for fire
                                    SpawnFire();
                                int random = Random.Range(5, 15);
                                Debug.Log("random damage: " + random);
                                _roomHpController.RoomTookDamage(_room);
                                _resources.TakeDamage(random);

                            }

                        }
                    }
                    else
                    {
                        if (AirshipStats.enemyGunBroken == 0)
                        {
                            if (rand == 4) //10% chance for fire
                                SpawnFire();
                            int random = Random.Range(20, 30);
                            Debug.Log("random damage: " + random);
                            _roomHpController.RoomTookDamage(_room);
                            _resources.TakeDamage(random);

                        }
                        if (AirshipStats.enemyGunBroken == 1)
                        {


                            if (rand == 4) //10% chance for fire
                                SpawnFire();
                            int random = Random.Range(15, 25);
                            Debug.Log("random damage: " + random);
                            _roomHpController.RoomTookDamage(_room);
                            _resources.TakeDamage(random);

                        }
                        if (AirshipStats.enemyGunBroken == 2)
                        {
                            if (rand == 4) //10% chance for fire
                                SpawnFire();
                            int random = Random.Range(10, 20);
                            Debug.Log("random damage: " + random);
                            _roomHpController.RoomTookDamage(_room);
                            _resources.TakeDamage(random);

                        }
                    }
                }
            }
            else
            {
                //enemy missed
            }
            Destroy(gameObject);
        }
    }

    public void MoveTowardsPosition(Vector3 targetPosition, float damage, float speed, string room, bool hit)
    {
        _targetPosition = targetPosition;
        _damage = damage;
        _speed = speed;
        _room = room;
        _move = true;
        _hit = hit;
    }

    private void SpawnFire()
    {
        _matches.StartBurningFire(_room);
    }
}
