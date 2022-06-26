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
    protected AudioSource _audioSource;
    private ItemDropper _itemDropper;


    protected Vector3 _direction = Vector3.zero;
    public Vector3 Direction => _direction;

    #region Flocking
    protected Action _endCallback;
    public Action EndCallback { get => _endCallback; set { _endCallback = value; } }
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
            StartCoroutine(DeadEnemy());
        }
    }
    #endregion

    protected Transform _targetTrm => GameManager.Instance.PlayerTrm;

    private void Awake()
    {
        Health = _enemyDataSO.health;
        _meshRenderer = GetComponent<MeshRenderer>();
        _itemDropper = GetComponent<ItemDropper>();
        _audioSource = GetComponent<AudioSource>();
        ChildAwake();


        //StartCoroutine(DoFlocking());
        //_endCallback = DeConnectionFlokingEnemy;
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

    protected void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    //protected virtual IEnumerator DoFlocking()
    //{
    //    LayerMask layer = LayerMask.GetMask("Enemy");
    //    while (true)
    //    {
    //        if (_isFlockingEnemy||_isHead) 
    //        {
    //            yield return null;
    //            continue;
    //        }
    //        neighbourhood = Physics.OverlapSphere(transform.position, sightRadius, layer);
    //        yield return new WaitForSeconds(SpreadUpdates());
    //        DoSeparationAndCohesion();
    //        yield return new WaitForSeconds(SpreadUpdates());
    //    }
    //}
    //protected void DoSeparationAndCohesion()
    //{
    //    foreach(Collider other in neighbourhood)
    //    {
    //        Enemy en = other.GetComponent<Enemy>();
    //        if(en.FlockingEnemy==null|| en.FlockingEnemy != this)
    //        {
    //            FlockingEnemy = en;
    //            FlockingEnemy.EndCallback = DeConnectionFlokingEnemy;
    //            break;
    //        }
    //    }
    //    EachOtherFlockingCheck();
    //}
    //protected void EachOtherFlockingCheck()
    //{
    //    if(_flockingEnemy.FlockingEnemy == this)
    //    {
    //        FlockingEnemy.EndCallback -= DeConnectionFlokingEnemy;
    //        IsHead = true;
    //    }
    //}
    //protected float SpreadUpdates()
    //{
    //    float offset = (Random.value - Random.value) / spread;
    //    return (1 / flockUpdatesPerSecond) + offset;
    //}
    //protected void DeConnectionFlokingEnemy()
    //{
    //    _flockingEnemy = null;
    //    _isFlockingEnemy = false;
    //    _isHead = false;
    //}

    protected virtual void Move()
    {
        if (_isSelfDestruct) return;

        
        _direction = _targetTrm.position - transform.position;

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
            _endCallback?.Invoke();
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
        PlayClip(_enemyDataSO.explosionClip);
        yield return new WaitForSeconds(_enemyDataSO.explosionClip.length + 0.3f);
        PoolManager.Instance.Push(this);

    }
    private IEnumerator DeadEnemy()
    {
        PlayClip(_enemyDataSO.deadClip);
        _isSelfDestruct = true;
        _meshRenderer.material.color = Color.black;
        yield return new WaitForSeconds(_enemyDataSO.deadClip.length + 0.1f);
        _endCallback?.Invoke();
        _itemDropper.DropItem();
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
        _endCallback?.Invoke();
        StopAllCoroutines();
    }
    public override void Reset()
    {
        Health = _enemyDataSO.health;
        _meshRenderer.material.color = Color.white;
        _isSelfDestruct = false;
        //DeConnectionFlokingEnemy();
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
