using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScore : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    public int Score { get; private set; }

    private void Awake()
    {
        Score = 0;
    }

    public void UpdateScore(int value)
    {
        Score += value;

        _scoreCounter.UpdateScore();
    }
}
