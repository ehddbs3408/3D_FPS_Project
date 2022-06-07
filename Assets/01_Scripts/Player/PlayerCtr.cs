using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtr : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 moveForce;

    private CharacterController _characyerController;

    private void Awake()
    {
        _characyerController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _characyerController.Move(moveForce * Time.deltaTime);
    }
    public void MoveTo(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }
}
