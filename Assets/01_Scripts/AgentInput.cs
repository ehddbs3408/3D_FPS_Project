using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<bool> OnClickMouseRBtn { get; set; }
    [field:SerializeField]
    public UnityEvent OnFireButtonKeyPress { get; set; }

    private void Update()
    {
        OnClickAimBtn();
        OnFireButton();
    }

    private void OnFireButton()
    {
        if(Input.GetMouseButton(0))
        {
            OnFireButtonKeyPress?.Invoke();
        }
    }

    private void OnClickAimBtn()
    {
        if(Input.GetMouseButtonDown(1))
        {
            OnClickMouseRBtn?.Invoke(true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            OnClickMouseRBtn?.Invoke(false);
        }
    }
}
