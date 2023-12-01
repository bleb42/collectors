using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(BaseFlag))]
public class BaseResources : MonoBehaviour
{
    [SerializeField] private TotalScore _totalScore;

    public int Score { get; private set; }

    private Base _base;
    private BaseFlag _baseFlag; 

    private void Awake()
    {
        Score = 0;

        _base = GetComponent<Base>();
        _baseFlag = GetComponent<BaseFlag>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            Destroy(resource.gameObject);
            _totalScore.UpdateScore(resource.Price);

            if (_baseFlag.Flag != null)
            {
                if (!_base.IsSecondBaseBuilded)
                {
                    if (_totalScore.Score >= _base.Price)
                    {
                        if (_base.TryBuildBase())
                        {
                            _totalScore.UpdateScore(-_base.Price);
                        }
                    }
                }
            }
            else if (_totalScore.Score >= _base.CollectorPrice)
            {
                if (_base.TrySpawnCollector())
                {
                    _totalScore.UpdateScore(-_base.CollectorPrice);
                }
            }
        }
    }
}