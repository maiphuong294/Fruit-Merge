using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    public int currentScore;
    public int bestScore;
    public void Awake()
    {
        instance = this; 
    }
    public void Start()
    {
        currentScore = 0;
        bestScore = 0;// = playerprefs.getHighScore
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }
    public int GetCurrentScore() => currentScore;
    public void UpdateBestScore()
    {
        bestScore = (currentScore > bestScore) ? currentScore : bestScore;
    }
    


}
