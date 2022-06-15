
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

    private Animator _animator;
    private readonly int _aimHashStr = Animator.StringToHash("IsAim");
    private readonly int _reloadHashStr = Animator.StringToHash("Reload");
    private readonly int _walkHashStr = Animator.StringToHash("Walk");

    private Vector3 _currentVec;

    private bool _isAimWeapon;
    public bool IsAimWepaon
    {
        get => _isAimWeapon;
    }
    private bool _isReload;
    public bool IsReload
    {
        get => _isReload;
    }
    private bool _isFire;
    private bool _isRun;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentVec = transform.localPosition;
    }
    public void Fire()
    {
        if (!_isFire&&_isReload==false)
        {
            _isFire = true;

            StartCoroutine(WaitForFireBullet(_weaponDataSO.fireRate));
            Bullet bullet = PoolManager.Instance.Pop("bullet") as Bullet;
            bullet.transform.position = _fireTrm.position;
            bullet.gameObject.SetActive(true);
            bullet.SetInit();

            ImpactParticle impact = PoolManager.Instance.Pop(_weaponDataSO.muzzleImpact.name) as ImpactParticle;
            impact.transform.position = _fireTrm.position;
            impact.transform.SetParent(_fireTrm);
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
        if (value)
        {
            Debug.Log("Aim!");
            _animator.SetBool(_aimHashStr, true);
        }
        else
        {

            _animator.SetBool(_aimHashStr, false);
        }
    }
    public void RunWeapon(bool value)
    {
        _isRun = value;
        if(_isRun)
        {
            _animator.SetBool(_walkHashStr, true);
        }
        else
        {
            _animator.SetBool(_aimHashStr, false);
        }
    }
    public void ReloadWeapon()
    {
        if (_isReload) return;
        _isReload = true;
        _animator.SetTrigger(_reloadHashStr);

    }
    public void ReloadToEnd()
    {
        _isReload = false;
    }
    private void WeaponRecoil()
    {

    }
}
