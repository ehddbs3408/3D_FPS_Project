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
    public UnityEvent OnFireKeyPress { get; set; }
    [field:SerializeField]
    public UnityEvent OnReloadKeyPress { get; set; }
    [field: SerializeField]
    public UnityEvent<bool> OnRecoilKeyPress { get; set; }

    private void Update()
    {
        OnClickAimBtn();
        OnFireInput();
        OnReloadInput();
        OnRecoilInput();
    }

    

    private void OnFireInput()
    {
        if(Input.GetMouseButton(0))
        {
            OnFireKeyPress?.Invoke();
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
    private void OnReloadInput()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnReloadKeyPress?.Invoke(); 
        }
    }
    private void OnRecoilInput()
    {
        if (Input.GetMouseButton(0))
        {
            OnRecoilKeyPress?.Invoke(true);
        }
        else
        {
            OnRecoilKeyPress?.Invoke(false);
        }
    }
}
