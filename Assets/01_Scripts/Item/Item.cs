using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PoolableMono
{
    [field: SerializeField]
    public ItemDataSO ItemDataSO { get; set; }

    private AudioSource _audioSource;
    private Collider _collider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = ItemDataSO.useAudio;
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length + 0.3f);
        PoolManager.Instance.Push(this);
    }

    public void DestroyResource()
    {
        gameObject.SetActive(false);
        PoolManager.Instance.Push(this);
    }
    public override void Reset()
    {
        _meshRenderer.enabled = true;
        _collider.enabled = true;
    }
}
