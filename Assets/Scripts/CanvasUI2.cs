using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUI2 : MonoBehaviour
{
    public Toggle m_RadianTg;
    public Toggle m_DigitalTg;

    public Toggle[] m_Players;

    public Button m_ResetBtn;
    public Button m_NextBtn;

    void Start()
    {
        m_Players[NumData.playerIndex].isOn = true;
        if (NumData.PanelType == EPanel.Radian)
        {
            m_RadianTg.isOn = true;
            m_DigitalTg.isOn = false;
        }
        else
        {
            m_RadianTg.isOn = false;
            m_DigitalTg.isOn = true;
        }
                
        m_RadianTg.onValueChanged.AddListener(OnRadianModel);
        m_DigitalTg.onValueChanged.AddListener(OnDigitalModel);

        for (int i = 0; i < 4; i++)
        {
            int index = i;
            m_Players[i].onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    OnChangePlayer(index);
                }
            });
        }

        m_ResetBtn.onClick.AddListener(OnResetClick);
        m_NextBtn.onClick.AddListener(OnNextLevel);
    }

    /// <summary>
    /// 切换模式
    /// </summary>
    /// <param name="value"></param>
    private void OnRadianModel(bool value)
    {
        if (value)
        {
            UIManager.Instance.Save();
            UIManager.Instance.Clear();

            UIManager.Instance.ChangeMode(EPanel.Radian);

            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// 切换模式
    /// </summary>
    /// <param name="value"></param>
    private void OnDigitalModel(bool value)
    {
        if (value)
        {
            UIManager.Instance.Save();
            UIManager.Instance.Clear();
            UIManager.Instance.ChangeMode(EPanel.Digital);
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// 切换玩家
    /// </summary>
    /// <param name="index"></param>
    private void OnChangePlayer(int index)
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        NumData.ChangePlayer(index);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 重置
    /// </summary>
    private void OnResetClick()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        NumData.Rest();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 下一关
    /// </summary>
    private void OnNextLevel()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        NumData.NextData();
        SceneManager.LoadScene(0);
    }
}