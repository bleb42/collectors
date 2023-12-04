using UnityEngine;

[RequireComponent(typeof(BaseResources))]
[RequireComponent(typeof(CollectorCreator))]
[RequireComponent(typeof(BaseFlag))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _price = 5;
    [SerializeField] private int _startingNumberOfCollectors = 3;
    [SerializeField] private GameObject _secondBasePrefab;

    public int Price { get; private set; }
    public bool IsSecondBaseBuilded { get; private set; }

    private CollectorCreator _collectorCreator;
    private BaseFlag _baseFlag;
    private BaseResources _baseResources;

    private void Awake()
    {
        _baseFlag= GetComponent<BaseFlag>();
        _collectorCreator = GetComponent<CollectorCreator>();
        _baseResources = GetComponent<BaseResources>();

        Price= _price;
        IsSecondBaseBuilded = false;
    }

    private void Start()
    {
        for (int i = 0; i < _startingNumberOfCollectors; i++)
        {
            _collectorCreator.TrySpawnCollector();
        }
    }

    public void BuildBase()
    {
        IsSecondBaseBuilded = true;

        _collectorCreator.Collectors[Random.Range(0, _collectorCreator.Collectors.Count)]
            .BuildBase(_baseFlag.Flag.transform.position, _secondBasePrefab, _baseFlag, _baseResources.ScoreCounter);
    }

    public void AppointCollector(Resource resourceToTake)
    {
        foreach (var collector in _collectorCreator.Collectors)
        {
            if (!collector.IsWorking)
            {
                collector.TakeResource(resourceToTake);
                resourceToTake.SetAsTarget();

                return;
            }
        }
    }
}