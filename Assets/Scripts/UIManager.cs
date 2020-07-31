using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Text CratesInfo;
    public Text CoinInfo;
    public Text GameOverText;
    public GameObject m_RestartButton;
    public GameObject m_MainMenu;

    private uint CoinsCount = 0u;

    const string CoinsTemplate = "Coins Count: {0}";
    const string CratesTemplate = "Crates Count: {0}";

    private void Start()
    {
        
    }

    public static void UpdateCoinInfo()
    {
        instance.CoinInfo.text = string.Format(CoinsTemplate, ++instance.CoinsCount);
    }

    public static void UpdateLives(uint lives)
    {
        instance.CratesInfo.text = string.Format(CratesTemplate, lives);

        if (lives == 0)
        {
            instance.GameOverText.enabled = true;
            instance.m_RestartButton.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayLevel()
    {
        PlayerController.SetPlayActive();
        m_MainMenu.SetActive(false);
    }

    public void ShutApplication()
    {
        Application.Quit();
    }

}
