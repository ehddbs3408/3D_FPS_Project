using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanel : MonoBehaviour
{
    private Text _currentAmmo = null;
    private Text _totalAmmo = null;

    private void Awake()
    {
        _currentAmmo = transform.Find("AmmoText").GetComponent<Text>();
        _totalAmmo = transform.Find("TotalAmmoText").GetComponent<Text>();
    }

    public void ASD(bool asd)
    {

    }
    public void GetCurrentAmmoValue(int ammo)
    {
        _currentAmmo.text = string.Format("{0}", ammo);
    }

    public void GetTotalAmmoValue(int totalAmmo,int maxAmmo)
    {
        _totalAmmo.text = string.Format("{0}", totalAmmo);
    }
}
