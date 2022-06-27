using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text _time;

    private void Awake()
    {
        _time = transform.Find("Time").GetComponent<Text>();
        _time.text = string.Format("{0}", PlayerPrefs.GetInt("Time"));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
