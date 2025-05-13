using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUI1 : MonoBehaviour
{
    public Button m_ResetBtn;
    public Button m_ChangeModeBtn;
    public Button m_ChangePlayerBtn;
    public Button m_NextBtn;

    void Start()
    {
        m_ResetBtn.onClick.AddListener(OnResetClick);
        m_ChangeModeBtn.onClick.AddListener(OnChangeModel);
        m_ChangePlayerBtn.onClick.AddListener(OnChangePlayer);
        m_NextBtn.onClick.AddListener(OnNextLevel);
    }

    private void OnResetClick()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        NumData.Rest();
        SceneManager.LoadScene(0);
    }

    private void OnChangeModel()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        
        UIManager.Instance.ChangeMode();
        
        SceneManager.LoadScene(0);
    }
    
    private void OnChangePlayer()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        
        NumData.ChangePlayer();
        
        SceneManager.LoadScene(0);
    }

    private void OnNextLevel()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        NumData.NextData();
        SceneManager.LoadScene(0);
    }

}