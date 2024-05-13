using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [Header("Settings")]
    private int score;
    private int bestScore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private float scoreMultiplier;

    private void Awake()
    {
        LoadData();
        MergeManager.OnMergeProcess += MergeProcessCallBack;
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.OnMergeProcess -= MergeProcessCallBack;
        GameManager.OnGameStateChanged -= GameStateChangedCallback;

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
        UpdateBestScoreText();
    }

    private void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {            
            case GameState.Gameover:
                CalculateBestScore();
                break;
        }
    }

    private void MergeProcessCallBack(FruitType fruiType, Vector2 unused)
    {
        int scoreToAdd = (int)fruiType;
        score += (int)(scoreToAdd * scoreMultiplier);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();
    }

    private void CalculateBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            SaveData();
        }
    }

    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
}
