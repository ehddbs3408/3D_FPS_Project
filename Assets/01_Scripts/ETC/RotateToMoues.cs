using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateToMoues : MonoBehaviour
{
    [SerializeField]
    private float rotCamXAxisSpeed = 5;
    [SerializeField]
    private float rotCamXAimAxisSpeed = 3;
    [SerializeField]
    private float rotCamYAxisSpeed = 3;
    [SerializeField]
    private float rotCamYAimAxisSpeed = 2;

    private Weapon _weapon;

    private float limitMinX = -80;
    private float limitMaxX = 50;
    private float eulerAngleX;
    private float eulerAngleY;

    private float recoilAngleX;
    private float recoilAngleY;

    private bool _isFire;

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }
    public void UpdateRotate(float mouseX,float mouseY)
    {
        if (_isFire) return;

        if(_weapon.IsAimWepaon)
        {
            eulerAngleY += mouseY * rotCamYAimAxisSpeed;
            eulerAngleX -= mouseX * rotCamXAimAxisSpeed;
        }
        else
        {
            eulerAngleY += mouseY * rotCamYAxisSpeed;
            eulerAngleX -= mouseX * rotCamXAxisSpeed;
        }
        

        eulerAngleX -= recoilAngleX;
        eulerAngleY += recoilAngleY;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    public void CameraRecoil(bool value)
    {
        if(value&&_weapon.IsRecoil==false)
        {
            if(_weapon.IsAimWepaon)
            {
                recoilAngleX = Random.Range(30, 35) * Time.deltaTime;
                recoilAngleY = Random.Range(-0.15f, 0.15f);
            }
            else
            {
                recoilAngleX = Random.Range(30, 40) * Time.deltaTime;
                recoilAngleY = Random.Range(-0.2f, 0.2f);
            }
        }
        else
        {
            recoilAngleX = 0;
            recoilAngleY = 0;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
