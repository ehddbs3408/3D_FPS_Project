using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PoolableMono
{
    [field: SerializeField]
    public ItemDataSO ItemDataSO { get; set; }

    private AudioSource _audioSource;
    private Collider _collider;
    private Collider _childCollider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = ItemDataSO.useAudio;
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _childCollider = transform.Find("Col").GetComponent<Collider>();
    }
    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        _childCollider.enabled = false;
        Debug.Log(_childCollider.gameObject.name);
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
        _childCollider.enabled = true;
    }
}
