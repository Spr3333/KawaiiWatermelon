using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    [Header("Refrences")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject settingsPanel;


    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;
            case GameState.Game:
                SetGame();
                break;
            case GameState.Gameover:
                SetGameover();
                break;
        }
    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        GamePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void SetGame()
    {
        GamePanel.SetActive(true);
        menuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    private void SetGameover()
    {
        GameOverPanel.SetActive(true);
        menuPanel.SetActive(false);
        GamePanel.SetActive(false);
    }

    public void PlayButtonCallback()
    {
        GameManager.Instance.SetGameState();
    }

    public void NextButtonCallback()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsButtonCallback()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettingCallback()
    {
        settingsPanel.SetActive(false);
    }
}
