using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private SpawnEnemyTableSO _spawnEenemyTable;

    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private float _maxSpawnDelay = 0.1f;
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

            int ran = Random.Range(0, _spawnEenemyTable.enemyList.Count);

            Enemy enemy = PoolManager.Instance.Pop(_spawnEenemyTable.enemyList[ran].name) as Enemy;
            enemy.transform.position = new Vector3(offset.x * _spawnLength, offset.y, offset.z * _spawnLength) + GameManager.Instance.PlayerTrm.position;
            yield return new WaitForSeconds(_spawnDelay);
            if(_maxSpawnDelay<_spawnDelay)
            {
                _spawnDelay -= 0.1f;
                Debug.Log(_spawnDelay);
            }
            else
            {
                _spawnDelay = _maxSpawnDelay - 0.1f;
            }
        }
       
    }
}
