using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticle : PoolableMono
{
    [SerializeField]
    private float _lifeTime;
    private void OnEnable()
    {
        StartCoroutine(WaitForDestroy());
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }
}
