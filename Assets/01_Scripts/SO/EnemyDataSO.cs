using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public float speed;
    public int health = 2;

    public GameObject explosionImpact;

}
