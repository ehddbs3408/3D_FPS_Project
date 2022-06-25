using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Weapon/Data")]
public class WeaponDataSO : ScriptableObject
{
    public float fireRate = 0.2f;
    public float aimRate = 1f;
    public float recoil = 3f;

    public int maxAmmo;

    public AudioClip shootingClip;
    public AudioClip reloadClip;
    public AudioClip noAmmoClip;

    public GameObject muzzleImpact;

}
