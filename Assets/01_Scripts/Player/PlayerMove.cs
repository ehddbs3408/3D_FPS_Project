using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private RotateToMoues _rotateToMouse;
    private PlayerCtr _movement;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _rotateToMouse = GetComponent<RotateToMoues>();
        _movement = GetComponent<PlayerCtr>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        _movement.MoveTo(new Vector3(x, 0, z));
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _rotateToMouse.UpdateRotate(mouseY, mouseX);
    }
}
