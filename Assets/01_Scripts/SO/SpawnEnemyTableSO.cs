using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Enemy/SpawnTable")]
public class SpawnEnemyTableSO : ScriptableObject
{ 
    public List<GameObject> enemyList;
}
