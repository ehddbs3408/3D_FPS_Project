using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Transform _playerTrm;

    public Transform PlayerTrm
    {
        get
        {
            if(_playerTrm==null)
            {
                _playerTrm = GameObject.FindObjectOfType<PlayerCtr>().GetComponent<Transform>();
            }
            return _playerTrm;
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager");
        }
        Instance = this;
    }

}
