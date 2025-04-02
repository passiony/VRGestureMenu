using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    public GameObject m_RadianPrefab;
    public int Num = 8;
    public float Spacing = 0.01f;
    public Transform m_Hand;
    public Transform SelectImg { get; protected set; }
    public int SelectIndex { get; protected set; }

    protected Vector3 SelectScale = new Vector3(1.1f, 1.1f, 1.1f);
    protected List<Image> radianList = new List<Image>();
    protected bool isFinish;

    public Transform Eye
    {
        get
        {
            if (Camera.main != null)
            {
                return Camera.main?.transform;
            }

            Debug.Log("找不到主相机");
            return null;
        }
    }

    public virtual void Start()
    {
        m_RadianPrefab.SetActive(false);
    }

    public virtual void Update()
    {
    }

    public virtual void Finish(bool fade)
    {
        isFinish = true;
    }

    public virtual Transform GetLine()
    {
        return SelectImg.Find("Line");
    }
}