using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : PoolableMono, IHittalble
{
    [SerializeField]
    protected EnemyDataSO _enemyDataSO;

    [SerializeField]
    protected float spread = 5f;
    [SerializeField]
    protected float flockUpdatesPerSecond = 5f;

    [SerializeField]
    protected float sightRadius = 10f;

    protected Collider[] neighbourhood = null;
    protected MeshRenderer _meshRenderer;


    protected Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;
    #region
    protected Enemy _flockingEnemy;
    public Enemy FlockingEnemy
    {
        get => _flockingEnemy;
        set
        {
            if (_isFlockingEnemy == false)
            {
                _isFlockingEnemy = true;
                _flockingEnemy = value;
                _flockingEnemy.IsHead = true;
            }
        }
    }

    protected int id = 0;
    protected bool _isFlockingEnemy = false;
    protected bool _isHead = false;
    public bool IsHead
    {
        get => _isHead;
        set
        {
            if(value)
            {
                _isHead = true;
                _isFlockingEnemy = false;
            }
            else
            {
                _isHead = false;
            }
            
        }
    }
    #endregion



    protected bool _isAttack = false;
    private bool _isSelfDestruct = false;

    #region Interface
    public float Health { get; set; }

    public void GetHit(float damge, GameObject dealer)
    {
        Health -= damge;


        if(Health <= 0)
        {
            PoolManager.Instance.Push(this);
        }
    }
    #endregion

    protected Transform _targetTrm => GameManager.Instance.PlayerTrm;

    private void Awake()
    {
        Health = _enemyDataSO.health;
        _meshRenderer = GetComponent<MeshRenderer>();
        ChildAwake();
        id = Random.Range(0, 1000);
        StartCoroutine(DoFlocking());
    }
    protected virtual void ChildAwake()
    {
        //nothing
    }

    private void Update()
    {
        Move();
        Attack();
    }

    protected virtual void Attack()
    {
        //
    }

    protected virtual IEnumerator DoFlocking()
    {
        LayerMask layer = LayerMask.GetMask("Enemy");
        while (true)
        {
            if (_isFlockingEnemy||_isHead) 
            {
                yield return null;
                continue;
            }
            neighbourhood = Physics.OverlapSphere(transform.position, sightRadius, layer);
            yield return new WaitForSeconds(SpreadUpdates());
            DoSeparationAndCohesion();
            yield return new WaitForSeconds(SpreadUpdates());
        }
    }
    protected void DoSeparationAndCohesion()
    {
        foreach(Collider other in neighbourhood)
        {
            Enemy en = other.GetComponent<Enemy>();
            if(en.FlockingEnemy==null)
            {
                FlockingEnemy = en;
                break;
            }
            if(en.FlockingEnemy!=this)
            {
                FlockingEnemy = en;
                break;
            }
        }
        EachOtherFlockingCheck();
    }
    protected void EachOtherFlockingCheck()
    {
        if(_flockingEnemy.FlockingEnemy == this)
        {
            IsHead = true;
        }
    }
    protected float SpreadUpdates()
    {
        float offset = (Random.value - Random.value) / spread;
        return (1 / flockUpdatesPerSecond) + offset;
    }

    protected virtual void Move()
    {
        if (_isSelfDestruct) return;
        if (_isFlockingEnemy)
        {
            _direction = _flockingEnemy.Direction;
            Debug.Log("asd");
        }
        else
        {
            _direction = _targetTrm.position - transform.position;
        }

        _direction.Normalize();
        transform.Translate(_direction * _enemyDataSO.speed*Time.deltaTime);
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
        ImpactParticle impact = PoolManager.Instance.Pop(_enemyDataSO.explosionImpact.name) as ImpactParticle;
        impact.transform.position = transform.position;
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push(this);

    }

    protected IEnumerator WaitForTimeAttack()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_enemyDataSO.attackDelay);
        _isAttack = false;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawSphere(transform.position, sightRadius);
            Gizmos.DrawWireSphere(transform.position, sightRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
