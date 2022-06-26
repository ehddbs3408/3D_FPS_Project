using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private LayerMask _resourceLayer;
    private Player _player;

    private void Awake()
    {
        _resourceLayer = LayerMask.NameToLayer("Resource");
  
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == _resourceLayer)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                switch (item.ItemDataSO.itemType)
                {
                    case ResourceItemType.AmmoPack:
                        _player.Weapon.TotalAmmo += item.ItemDataSO.GetAmount();
                        item.PickUpResource();
                        break;

                    case ResourceItemType.HealthPack:
                        break;

                    case ResourceItemType.None:
                        break;
                    default:
                        break;
                }
            }
        }
    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //    if (hit.gameObject.layer == _resourceLayer)
    //    {
    //        Debug.Log("GEt");
    //        Item item = hit.gameObject.GetComponent<Item>();
    //        if (item != null)
    //        {
    //            switch (item.ItemDataSO.itemType)
    //            {
    //                case ResourceItemType.AmmoPack:
    //                    _player.Weapon.TotalAmmo += item.ItemDataSO.GetAmount();
    //                    item.PickUpResource();
    //                    break;

    //                case ResourceItemType.HealthPack:
    //                    break;

    //                case ResourceItemType.None:
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }
    //}
}
