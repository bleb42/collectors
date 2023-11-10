using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private BaseResources _base;

    public void UpdateScore()
    {
        _score.text = $"{_base.Score}";
    }
}
