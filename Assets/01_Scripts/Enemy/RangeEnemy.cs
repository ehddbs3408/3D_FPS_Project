using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{

    protected override void Attack()
    {
        if (_isAttack) return;


    }

    protected override void Move()
    {
        Vector3 dir = _targetTrm.position - transform.position;
        if(dir.sqrMagnitude <= 40)
        {
            Vector3 vec = new Vector3(-dir.z, 0, dir.x);
            transform.Translate(vec * 3 * Time.deltaTime);
        }
        else
        {
            dir.Normalize();
            transform.Translate(dir * _enemyDataSO.speed * Time.deltaTime);
        }
        
    }
}
