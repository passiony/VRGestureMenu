using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControll : MonoBehaviour
{
    public static LineControll Instance;
    private LineRenderer m_Line;

    private void Awake()
    {
        Instance = this;
        m_Line = gameObject.GetComponent<LineRenderer>();
    }

    public void SetPos(int index, Vector3 pos)
    {
        if (NumData.PanelType == EPanel.Digital)
        {
            m_Line.positionCount = index + 1;
            m_Line.SetPosition(index, pos);
        }
    }
}