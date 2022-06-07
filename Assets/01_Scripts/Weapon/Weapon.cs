
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponDataSO _weaponDataSO;
    [SerializeField]
    private Transform _fireTrm;
    [SerializeField]
    private GameObject _bulletPrefab;

    private Vector3 _currentVec;

    private bool _isAimWeapon;
    private bool _isFire;
    private void Awake()
    {
        _currentVec = transform.localPosition;
    }
    private void Update()
    {
        
    }
    public void Fire()
    {
        Sequence seq = DOTween.Sequence();
        if (!_isFire)
        {
            _isFire = true;
            StartCoroutine(WaitForFireBullet(_weaponDataSO.fireRate));
            Bullet bullet = Instantiate(_bulletPrefab, _fireTrm.position, Quaternion.identity).GetComponent<Bullet>() as Bullet;          

            bullet.gameObject.SetActive(true);
        }
        
    }

    private IEnumerator WaitForFireBullet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isFire = false;
    }

    public void AimWaepon(bool value)
    {
        _isAimWeapon = value;
        DOTween.Kill(this);
        Sequence seq = DOTween.Sequence();
        if (_isAimWeapon)
        {
            seq.Append(transform.DOLocalMoveX(_currentVec.x + -0.3f, _weaponDataSO.aimRate));
        }
        else
        {
            seq.Append(transform.DOLocalMoveX(_currentVec.x, _weaponDataSO.aimRate));

        }
    }

    private void WeaponRecoil()
    {

    }
}
