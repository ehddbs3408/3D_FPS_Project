using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Weapon/Bullet")]
public class BulletDataSO : ScriptableObject
{
    public float speed;
    public int damage;

    public GameObject impactPrefab;
}
