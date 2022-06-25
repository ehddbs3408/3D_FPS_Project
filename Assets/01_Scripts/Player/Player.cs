using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Weapon _weapon;
    public Weapon Weapon => _weapon;

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }



}
