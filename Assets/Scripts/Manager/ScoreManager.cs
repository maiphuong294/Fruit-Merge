using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    public int currentScore;
    public int comboCounter;
    public float comboTimer;
    public void Awake()
    {
        instance = this; 
    }
    public void Start()
    {
        Application.targetFrameRate = 60;
        currentScore = 0;
        comboCounter = -1;
        comboTimer = 0f;
    }

    public void Update()
    {
        if (comboCounter > 0) comboTimer += Time.deltaTime;
        if (comboTimer > 0.8f) {
            comboTimer = 0f;
            comboCounter = -1;
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Messenger.FireEvent(EventKey.OnCurrentScoreChange);
    }
    public void ResetScore()
    {
        currentScore = 0;
        Messenger.FireEvent(EventKey.OnCurrentScoreChange);
    }
    public int GetCurrentScore() => currentScore;
 
    


}
