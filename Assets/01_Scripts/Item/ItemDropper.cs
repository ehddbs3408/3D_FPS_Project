using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private ItemDropTableSO _dropTable;
    private float[] _itemWeights;

    [SerializeField]
    private bool _dropEffect = false;

    [SerializeField]
    private float _dropPower;
    [SerializeField]
    [Range(0, 1)]
    private float _dropChance;

    private void Start()
    {
        _itemWeights = _dropTable.dropList.Select(item => item.rate).ToArray();
    }

    public void DropItem()
    {
        float dropVariable = Random.value;
        if (dropVariable < _dropChance)
        {
            int idx = GetRandomWeightedIndex();
            Item resource = PoolManager.Instance.Pop(_dropTable.dropList[idx].itemPrefab.name) as Item;

            resource.transform.position = transform.position;

            //Action destroyAction = null;
            //destroyAction = () =>
            //{
            //    resource.DestroyResource();
            //    GameManager.Instance.OnClearAllDropItems -= destroyAction;
            //};
            //GameManager.Instance.OnClearAllDropItems += destroyAction;

            if (_dropEffect)
            {
                Vector3 offset = Random.insideUnitCircle;
                offset.z = 0;
                resource.transform.DOJump(transform.position + offset, _dropPower, 1, 0.3f);
            }
        }
    }
    private int GetRandomWeightedIndex()
    {
        float sum = 0f;
        for (int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i]; // 여러번 모든 아이템의 드랍확률이 합산된다.
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for (int i = 0; i < _itemWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }
            else
            {
                tempSum += _itemWeights[i];
            }
        }

        return 0;
    }
}
