using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Settings")]
    private GameState gameState;


    [Header("Action")]
    public static Action<GameState> OnGameStateChanged;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetMenu();
    }
    private void SetMenu()
    {
        SetGameState(GameState.Menu);
    }

    private void SetGame()
    {
        SetGameState(GameState.Game);
    }

    private void SetGameover()
    {
        SetGameState(GameState.Gameover);
    }

    private void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }

    private void SetMenuState()
    {
        SetMenu();
    }

    public void SetGameState()
    {
        SetGame();
    }

    public void SetGameoverState()
    {
        SetGameover();
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }


}
