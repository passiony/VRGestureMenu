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

    private void OnEnable()
    {
        m_SelectAction.action.performed += HandleSelectAction;
    }

    void OnDisable()
    {
        m_SelectAction.action.performed -= HandleSelectAction;
    }

    private void HandleSelectAction(InputAction.CallbackContext context)
    {
        OnPinched?.Invoke(PokePoint.position);
    }
}