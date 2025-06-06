using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Gestures;

public class HandGuestureListener : MonoBehaviour
{
    private static List<XRHandSubsystem> s_SubsystemsReuse = new List<XRHandSubsystem>();
    private XRFingerShape m_XRFingerShape;
    private bool isPinched;
    public Transform IndexTip;

    public UnityEvent<Vector3> OnPinched;
    public UnityEvent<Vector3> OnUnpinch;

    private void Start()
    {
        m_XRFingerShape = new XRFingerShape();
    }

    void Update()
    {
        var subsystem = TryGetSubsystem();
        if (subsystem == null)
            return;

        var hand = subsystem.rightHand;
        m_XRFingerShape = hand.CalculateFingerShape(XRHandFingerID.Index, XRFingerShapeTypes.All);
        if (m_XRFingerShape.TryGetPinch(out var pinch))
        {
            if (!isPinched && pinch >= 0.99f)
            {
                isPinched = true;
                OnPinched?.Invoke(IndexTip.position);
            }
            else if (isPinched && pinch < 0.5f)
            {
                isPinched = false;
                OnUnpinch?.Invoke(IndexTip.position);
            }
        }
    }

    static XRHandSubsystem TryGetSubsystem()
    {
        SubsystemManager.GetSubsystems(s_SubsystemsReuse);
        return s_SubsystemsReuse.Count > 0 ? s_SubsystemsReuse[0] : null;
    }
}