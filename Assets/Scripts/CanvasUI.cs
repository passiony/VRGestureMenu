using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    public Button m_ResetBtn;
    public Button m_ChagneModeBtn;

    void Start()
    {
        m_ResetBtn.onClick.AddListener(OnResetClick);
        m_ChagneModeBtn.onClick.AddListener(OnChangeModel);
    }

    private void OnResetClick()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        
        SceneManager.LoadScene(0);
    }

    private void OnChangeModel()
    {
        UIManager.Instance.Save();
        UIManager.Instance.Clear();
        UIManager.Instance.ChangeMode();
    }
}