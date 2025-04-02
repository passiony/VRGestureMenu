using System;
using System.IO;
using System.Text;
using UnityEngine;

public enum EPanel
{
    Radian,
    Digital
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public EPanel m_PanelType;
    public GameObject RadianPanel;
    public GameObject DigitalPanel;
    public float distance = 4;

    BaseView m_MainPanel;
    BaseView m_SubPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void HandleSelectAction(Vector3 fingerPosition)
    {
        if (m_MainPanel == null)
        {
            var camera = Camera.main.transform;
            var dirction = Vector3.Normalize(fingerPosition - camera.position);
            var position = camera.position + dirction.normalized * distance;
            transform.position = position;
            transform.forward = Vector3.Normalize(position - camera.position);
            m_MainPanel = CreatePanel(false);
        }
        else if (m_SubPanel == null)
        {
            m_MainPanel.Finish(true);
            m_SubPanel = CreatePanel(true);
        }
        else
        {
            m_SubPanel.Finish(false);
        }
    }

    public BaseView CreatePanel(bool subview)
    {
        switch (m_PanelType)
        {
            case EPanel.Radian:
                return CreateRadianView(subview);
            case EPanel.Digital:
                return CreateDigitalView(subview);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    BaseView CreateRadianView(bool subview)
    {
        // 创建RadianPanel页面
        var go = Instantiate(RadianPanel, transform);
        go.SetActive(true);

        if (subview)
        {
            var pos = m_MainPanel.SelectImg.GetChild(0).position;
            go.transform.position = pos;
        }

        return go.GetComponent<BaseView>();
    }

    BaseView CreateDigitalView(bool subview)
    {
        // 创建RadianPanel页面
        var go = Instantiate(DigitalPanel, transform);
        go.SetActive(true);
        if (subview)
        {
            var pos = m_MainPanel.SelectImg.Find("Text").position;
            go.transform.position = pos;
        }

        return go.GetComponent<BaseView>();
    }

    public void Save()
    {
        if (m_MainPanel == null)
        {
            Debug.Log("没有数据可保存");
            return;
        }

        var firstId = m_MainPanel.SelectIndex;
        var secondId = m_SubPanel.SelectIndex;
        var time = DateTime.Now.ToString("G");
        var filename = DateTime.Now.ToString("yyyyMMddHH") + ".txt";

        string path = Path.Combine(Application.persistentDataPath, filename);
        Debug.Log("path:" + path);
        StringBuilder sb = new StringBuilder();
        if (FileUtility.Exists(path))
        {
            sb.AppendLine(FileUtility.SafeReadAllText(path));
        }

        sb.AppendLine(time);
        sb.AppendLine(firstId + " - " + secondId);
        if (FileUtility.SafeWriteAllText(path, sb.ToString()))
        {
            Debug.Log("保存成功");
        }
        else
        {
            Debug.Log("保存失败");
        }
    }

    public void Clear()
    {
        Destroy(m_MainPanel?.gameObject);
        Destroy(m_SubPanel?.gameObject);

        m_MainPanel = null;
        m_SubPanel = null;
    }

    public void ChangeMode()
    {
        Clear();

        if (m_PanelType == EPanel.Radian)
            m_PanelType = EPanel.Digital;
        else
            m_PanelType = EPanel.Radian;
    }
}