using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField]
    private BulletDataSO _bulletDataSO;

    private TrailRenderer _trailRenderer;

    private Vector3 _directionMove = Vector3.forward;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        
        StartCoroutine(BulletLifeTimeCoroutine());
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position += _directionMove * _bulletDataSO.speed * Time.deltaTime;
    }
    public void SetInit()
    {
        _trailRenderer.enabled = true;
        _directionMove = Camera.main.transform.TransformDirection(Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IHittalble hit = other.GetComponent<IHittalble>();
            hit.GetHit(1, gameObject);
            PoolManager.Instance.Push(this);
        }
    }

    private IEnumerator BulletLifeTimeCoroutine()
    {
        yield return new WaitForSeconds(5);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        _trailRenderer.Clear();
        _trailRenderer.enabled = false;
        transform.position = Vector3.zero;
        _directionMove = Vector3.forward;
    }
}
