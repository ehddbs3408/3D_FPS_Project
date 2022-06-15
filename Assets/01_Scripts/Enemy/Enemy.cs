using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono, IHittalble
{
    [SerializeField]
    protected EnemyDataSO _enemyDataSO;

    protected MeshRenderer _meshRenderer;

    private bool _isSelfDestruct = false;

    #region Interface
    public float Health { get; set; }

    public void GetHit(float damge, GameObject dealer)
    {
        Health -= damge;


        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    protected Transform _targetTrm => GameManager.Instance.PlayerTrm;

    private void Awake()
    {
        Health = _enemyDataSO.health;
        _meshRenderer = GetComponent<MeshRenderer>();
        ChildAwake();
    }
    protected virtual void ChildAwake()
    {
        //nothing
    }

    private void Update()
    {
        Move();
    }
    protected virtual void Move()
    {
        if (_isSelfDestruct) return;
        Vector3 dir = _targetTrm.position - transform.position;
        dir.Normalize();
        transform.Translate(dir * _enemyDataSO.speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&&_isSelfDestruct==false)
        {
            Debug.Log("Player Hit");
            _isSelfDestruct = true;
            StartCoroutine(ExplosionEnemy());
        }
    }

    private IEnumerator ExplosionEnemy()
    {
        _meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _meshRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        _meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _meshRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        Instantiate(_enemyDataSO.explosionImpact, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push(this);

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public override void Reset()
    {
        Health = _enemyDataSO.health;
        _meshRenderer.material.color = Color.white;
        _isSelfDestruct = false;
    }
}
