using System;
using System.Collections;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EPanel
{
    Radian,
    Digital
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI m_PlayerTxt;
    public TextMeshProUGUI m_LableTxt;
    public GameObject RadianPanel;
    public GameObject DigitalPanel;
    public float distance = 4;

    BaseView m_MainPanel;
    BaseView m_SubPanel;

    private float startPinchTime;
    private float endPinchTime;
    private bool match1;
    private bool match2;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public int state;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        m_PlayerTxt.text = $"Player:{NumData.playerIndex + 1}   Mode:{NumData.PanelType}";
        m_LableTxt.text = $"{NumData.CurrentNum[0]}-{NumData.CurrentNum[1]}";
        StartCoroutine(DelayFade());
        match1 = false;
        match2 = false;
        state = 0;
    }

    IEnumerator DelayFade()
    {
        m_LableTxt.alpha = 0;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            m_LableTxt.alpha += 0.1f;
        }

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            m_LableTxt.alpha -= 0.1f;
        }
    }

    /// <summary>
    /// 手指捏合，创建主面板
    /// </summary>
    /// <param name="fingerPosition"></param>
    public void HandlePinchAction(Vector3 fingerPosition)
    {
        if (m_MainPanel == null)
        {
            var camera = Camera.main.transform;
            var dirction = Vector3.Normalize(fingerPosition - camera.position);
            var position = camera.position + dirction.normalized * distance;
            transform.position = position;
            transform.forward = Vector3.Normalize(position - camera.position);
            m_MainPanel = CreatePanel(false);
            m_MainPanel.SetNames(NumData.MainNames);
            m_MainPanel.OnTrigger += OnMainTriggerEnter;
            startPinchTime = Time.time;
            startPosition = fingerPosition;
            state = 1;
            LineControll.Instance.SetPos(0, m_MainPanel.transform.position);
        }
    }

    public void OnMainTriggerEnter(int index)
    {
        string value = NumData.MainNames[index - 1];
        //选择正确
        if (value.ToLower() == NumData.CurrentNum[0].ToLower())
        {
            if (m_SubPanel == null)
            {
                m_MainPanel.Finish(true);
                m_SubPanel = CreatePanel(true);
                m_SubPanel.OnTrigger += OnSubTriggerEnter;
                m_SubPanel.SetNames(NumData.SubNames[index - 1]);
                state = 2;
                LineControll.Instance.SetPos(1, m_SubPanel.transform.position);
            }

            match1 = false;
        }
        else
        {
            // 选择错误
            match1 = false;
        }
    }

    private void OnSubTriggerEnter(int index)
    {
        string value = m_SubPanel.Names[index - 1];
        //选择正确
        if (value.ToLower() == NumData.CurrentNum[1].ToLower())
        {
            match2 = true;
        }
        else
        {
            match2 = false;
        }
    }

    private void Update()
    {
        if (state == 1)
        {
            LineControll.Instance.SetPos(1, m_MainPanel.IntersectionPos);
        }
        else if (state == 2)
        {
            LineControll.Instance.SetPos(2, m_SubPanel.IntersectionPos);
        }
    }

    public void HandleUnpinchAction(Vector3 fingerPosition)
    {
        if (m_SubPanel)
        {
            m_SubPanel.Finish(true);
            NumData.NextData();
            StartCoroutine(CoDelayRestart());
            state = 3;
            LineControll.Instance.SetPos(2, m_SubPanel.SelectPos());
        }

        endPinchTime = Time.time;
        endPosition = fingerPosition;
    }

    BaseView CreatePanel(bool subview)
    {
        switch (NumData.PanelType)
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
            var pos = m_MainPanel.SelectPos();
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
            var pos = m_MainPanel.SelectPos();
            go.transform.position = pos;
        }

        return go.GetComponent<BaseView>();
    }

    public void Save()
    {
        if (!m_MainPanel ||!m_SubPanel)
        {
            Debug.Log("没有数据可保存");
            return;
        }

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
        sb.AppendLine($"Player:{NumData.playerIndex + 1} Mode:{NumData.PanelType}");
        sb.AppendLine($"{NumData.CurrentNum[0]}-{NumData.CurrentNum[1]}");
        sb.AppendLine($"匹配成功：{match1}-{match2}");
        sb.AppendLine("duration:" + (endPinchTime - startPinchTime));
        sb.AppendLine("startPos:" + startPosition);
        sb.AppendLine("endPos:" + endPosition);

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

        if (NumData.PanelType == EPanel.Radian)
            NumData.PanelType = EPanel.Digital;
        else
            NumData.PanelType = EPanel.Radian;
    }

    public void ChangeMode(EPanel mode)
    {
        Clear();
        NumData.PanelType = mode;
    }

    IEnumerator CoDelayRestart()
    {
        yield return new WaitForSeconds(2);
        NumData.times++;
        if (NumData.times >= 4)
        {
            NumData.times = 0;

            UIManager.Instance.Save();
            UIManager.Instance.Clear();
            NumData.NextData();
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}