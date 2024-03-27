using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _P1layerText;
    [SerializeField] private TextMeshProUGUI _P2layerText;

    private void Start()
    {
        ScoreUpdated(null, null);
        GameManager.ScoreUpdated += ScoreUpdated;
    }

    private void ScoreUpdated(object sender, EventArgs e)
    {
        _P1layerText.text = GameManager.P1Score.ToString();
        _P2layerText.text = GameManager.P2Score.ToString();
    }
}
