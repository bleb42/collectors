using UnityEngine;

public class BaseResources : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    
    public ScoreCounter ScoreCounter { get => _scoreCounter; private set { } }

    public int Score { get; private set; }

    private void Awake()
    {
        if (_scoreCounter != null)
            ScoreCounter = _scoreCounter;
        
        Score = 0;
    }

    public void AddScore(int amount)
    {
        Score += amount;
        ScoreCounter.UpdateScore();
    }

    public void Pay(int amount)
    {
        Score -= amount;
        ScoreCounter.UpdateScore();
    }

    public void AddScoreCounter(ScoreCounter scoreCounter)
    {
        ScoreCounter = scoreCounter;
    }
}