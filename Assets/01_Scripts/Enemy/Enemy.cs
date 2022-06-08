using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittalble
{
    [SerializeField]
    protected EnemyDataSO _enemyDataSO;

    #region Interface
    public float Health { get; set; }

    public void GetHit(float damge, GameObject dealer)
    {
        Health -= damge;

        Debug.Log("Hit : " + Health);

        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    protected Transform _targetTrm => GameManager.Instance.PlayerTrm;

    private void Awake()
    {
        Health = 10;
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
        Vector3 dir = _targetTrm.position - transform.position;
        dir.Normalize();
        transform.Translate(dir * _enemyDataSO.speed*Time.deltaTime);
    }

}
