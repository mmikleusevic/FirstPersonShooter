using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int score;
    private int highscore;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        highscore = PlayerPrefs.GetInt(HighScoreKey, 0);
        
        SetScore();
        SetHighScore();
    }

    public void AddScore(int amount)
    {
        score += amount;
        
        SetScore();
        CheckScore();
    }

    private void CheckScore()
    {
        if (score <= highscore) return;
        
        highscore = score;
        SetHighScore();
    }

    private void SetScore()
    {
        scoreText.text = $"Score: {score}";
    }

    private void SetHighScore()
    {
        highScoreText.text = $"HighScore: {highscore}";
    }
    
    public void SaveHighScore()
    {
        if (score < highscore) return;
        
        PlayerPrefs.SetInt(HighScoreKey, highscore);
    }
    
    private void OnApplicationQuit()
    {
        SaveHighScore();
    }
}