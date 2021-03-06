using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour,IHittalble
{
    private Weapon _weapon;
    public Weapon Weapon => _weapon;

    [field:SerializeField]
    public UnityEvent<float> OnChangeHealth { get; set; }

    [SerializeField]
    private int maxHeatlh;
    private float health;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            OnChangeHealth?.Invoke(health/maxHeatlh);
        }
    }

    public void GetHit(float damge, GameObject dealer)
    {
        Health -= damge;


        if(Health<=0)
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("Die");
        }
    }

    private void Awake()
    {
        
        _weapon = GetComponentInChildren<Weapon>();
    }
    private void Start()
    {
        Health = maxHeatlh;
    }


}
