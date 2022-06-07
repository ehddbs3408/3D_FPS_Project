using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletDataSO _bulletDataSO;

    private Vector3 _directionMove = Vector3.forward;

    private void Update()
    {
        
        Move();
    }
    private void Move()
    {
        _directionMove = Camera.main.transform.TransformDirection(Vector3.forward);
        transform.position += _directionMove * _bulletDataSO.speed * Time.deltaTime;
    }
}
