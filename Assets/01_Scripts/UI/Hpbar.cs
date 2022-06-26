using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void GetHealthValue(float value)
    {
        _slider.value = value;
    }
}
