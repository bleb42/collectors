using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(BaseFlag))]
[RequireComponent(typeof(BaseResources))]
[RequireComponent(typeof(CollectorCreator))]
public class BaseCollider : MonoBehaviour
{
    private BaseResources _baseResources;
    private CollectorCreator _collectorCreator;
    private Base _base;
    private BaseFlag _baseFlag;

    private void Awake()
    {
        _baseResources = GetComponent<BaseResources>();
        _collectorCreator = GetComponent<CollectorCreator>();
        _base = GetComponent<Base>();
        _baseFlag = GetComponent<BaseFlag>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            Destroy(resource.gameObject);
            _baseResources.AddScore(resource.Price);

            if (_baseFlag.Flag != null)
            {
                if (!_base.IsSecondBaseBuilded)
                {
                    if (_baseResources.Score >= _base.Price)
                    {
                        _base.BuildBase();

                        _baseResources.ScoreCounter.UpdateScore();
                    }
                }
            }
            else if (_baseResources.Score >= _collectorCreator.CollectorPrice)
            {
                if (_collectorCreator.TrySpawnCollector())
                {
                    _baseResources.Pay(_collectorCreator.CollectorPrice);
                    _baseResources.ScoreCounter.UpdateScore();
                }
            }
        }
    }
}
