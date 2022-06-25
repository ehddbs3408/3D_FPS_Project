using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[System.Serializable]
[CreateAssetMenu(menuName ="SO/Item/ItemData")]
public class ItemDataSO : ScriptableObject
{
    [Range(0,1)]
    public float rate = 1;
    public GameObject itemPrefab;

    [field: SerializeField]
    public ResourceItemType itemType;

    [SerializeField]
    private int minAmount = 1, maxAmount = 30;
    public AudioClip useAudio;

    public int GetAmount()
    {
        return Random.Range(minAmount, maxAmount + 1);
    }
}
