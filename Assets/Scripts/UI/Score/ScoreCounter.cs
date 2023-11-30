using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TotalScore _totalScore;

    public void UpdateScore()
    {
        _score.text = $"{_totalScore.Score}";
    }
}