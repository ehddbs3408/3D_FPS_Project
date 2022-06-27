using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    private Text _timeText;
    private Text _maxTimeText;

    private int _maxLifeTime;
    private int _time = 0;
    private void Awake()
    {
        _time = 0;
        
        _maxLifeTime = PlayerPrefs.GetInt("Time");
        _timeText = transform.Find("TimeText").GetComponent<Text>();
        _maxTimeText = transform.Find("MaxTimeText").GetComponent<Text>();
    }

    private void Start()
    {
        _maxTimeText.text = string.Format("{0}", _maxLifeTime);
        StartCoroutine(Timer());
    }
    private void Update()
    {
        //PlayerPrefs.SetInt("Time", 0);
    }

    private IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            _time += 1;
            _timeText.text = string.Format("{0}", _time);
            if(_time>_maxLifeTime)
            {
                _maxLifeTime = _time;
                _maxTimeText.text = string.Format("{0}", _maxLifeTime);
                PlayerPrefs.SetInt("Time", _time);
            }
        }
    }



}
