using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    public int currentScore;
    public void Awake()
    {
        instance = this; 
    }
    public void Start()
    {
        Application.targetFrameRate = 60;
        currentScore = 0;
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }
    public int GetCurrentScore() => currentScore;
 
    


}
