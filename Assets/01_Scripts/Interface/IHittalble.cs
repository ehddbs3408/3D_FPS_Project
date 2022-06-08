using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittalble
{
    public float Health { get; set; }
    public void GetHit(float damge,GameObject dealer);
}
