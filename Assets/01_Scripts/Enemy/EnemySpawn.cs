using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private float _spawnLength;
    private void Start()
    {
        StartCoroutine(Spanw());
    }

    private IEnumerator Spanw()
    {
        while(true)
        {
            Vector3 offset = Random.insideUnitSphere;
            offset.y = Random.Range(1, 10f);
            Enemy enemy = PoolManager.Instance.Pop("Enemy") as Enemy;
            enemy.transform.position = new Vector3(offset.x * _spawnLength, offset.y, offset.z * _spawnLength) + GameManager.Instance.PlayerTrm.position;
            yield return new WaitForSeconds(_spawnDelay);
        }
       
    }
}
