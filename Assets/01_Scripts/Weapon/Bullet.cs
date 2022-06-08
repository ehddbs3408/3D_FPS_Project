using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletDataSO _bulletDataSO;
    [SerializeField]
    private LayerMask _enemyLayer;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IHittalble hit = other.GetComponent<IHittalble>();
            hit.GetHit(1, gameObject);
        }
    }

}
