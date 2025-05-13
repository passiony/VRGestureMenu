using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HandControllerListener : MonoBehaviour
{
    public InputActionProperty m_SelectAction =
        new(new InputAction("Select", type: InputActionType.Button));

    public Transform PokePoint;
    public UnityEvent<Vector3> OnPinched;
    public UnityEvent<Vector3> OnUnPinched;
    private bool selected = false;
    private bool released = true;

    private void Update()
    {
        if (m_SelectAction.action.IsPressed())
        {
            Select();
        }
        else
        {
            Release();
        }
    }

    void Select()
    {
        if (!selected)
        {
            selected = true;
            released = false;
            OnPinched?.Invoke(PokePoint.position);
        }
    }
    
    void Release()
    {
        if (!released)
        {
            released = true;
            selected = false;
            OnUnPinched?.Invoke(PokePoint.position);
        }
    }
}