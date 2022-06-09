using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFloor : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = GameManager.Instance.PlayerTrm.position;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
}
