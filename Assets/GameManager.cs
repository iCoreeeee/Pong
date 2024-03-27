using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int _p1PlayerScore;
    public static int P1Score
    {
        get => _p1PlayerScore;
        set
        {
            _p1PlayerScore = value;
            ScoreUpdated?.Invoke(null, EventArgs.Empty);
        }
    }
    
    private static int _p2PlayerScore;
    public static int P2Score
    {
        get => _p2PlayerScore;
        set
        {
            _p2PlayerScore = value;
            ScoreUpdated?.Invoke(null, EventArgs.Empty);
        }
    }
    
    public static EventHandler ScoreUpdated;

    private void Start()
    {
        P1Score = 0;
        P2Score = 0;
        Time.timeScale = 1f;
    }
    
    
    
}
