using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtr : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 moveForce;

    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float gravity;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0, value);
    }

    private CharacterController _characyerController;
     
    private void Awake()
    {
        _characyerController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if(!_characyerController.isGrounded)
        {
            moveForce.y += gravity * Time.deltaTime;
        }


        _characyerController.Move(moveForce * Time.deltaTime);

    }
    public void MoveTo(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Jump()
    {
        if(_characyerController.isGrounded)
        {
            moveForce.y = _jumpForce;
        }
    }
}
