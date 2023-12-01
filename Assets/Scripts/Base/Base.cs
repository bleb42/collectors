using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BaseFlag))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _price = 5;
    [SerializeField] private int _startingNumberOfCollectors = 3;
    [SerializeField] private int _collectorPrice = 3;
    [SerializeField] private Collector _collector;
    [SerializeField] private GameObject _secondBasePrefab;

    public int Price { get; private set; }
    public int CollectorPrice { get; private set; }
    public bool IsSecondBaseBuilded { get; private set; }
    
    private List<Collector> _collectors = new List<Collector>();
    private CollectorPlace[] _collectorPlaces;

    private BaseFlag _baseFlag;

    private void Awake()
    {
        _baseFlag= GetComponent<BaseFlag>();
        _collectorPlaces = GetComponentsInChildren<CollectorPlace>();

        CollectorPrice = _collectorPrice;
        Price= _price;
        IsSecondBaseBuilded = false;
    }

    private void Start()
    {
        for (int i = 0; i < _startingNumberOfCollectors; i++)
        {
            TrySpawnCollector();
        }
    }

    public bool TryBuildBase()
    {
        if (_baseFlag.Flag != null)
        {
            IsSecondBaseBuilded = true;

            _collectors[Random.Range(0, _collectors.Count)].BuildBase(_baseFlag.Flag.transform.position, _secondBasePrefab, _baseFlag);

            return IsSecondBaseBuilded;
        }

        return IsSecondBaseBuilded;
    }

    public void AppointCollector(Resource resourceToTake)
    {
        foreach (var collector in _collectors)
        {
            if (!collector.IsWorking)
            {
                collector.TakeResource(resourceToTake);
                resourceToTake.SetAsTarget();

                return;
            }
        }
    }

    public bool TrySpawnCollector()
    {
        if (_collectorPlaces.Length == 0)
        {
            return false;
        }

        foreach (var collectorPlace in _collectorPlaces)
        {
            if (collectorPlace.IsFree)
            {
                Collector collector = Instantiate(_collector, collectorPlace.transform.position, Quaternion.identity);
                
                collector.TakePlace(collectorPlace);
                collectorPlace.TakePlace(collector);
                _collectors.Add(collector);

                return true;
            }
        }

        return false;
    }

    public CollectorPlace TryAddCollector(Collector collector)
    {
        if (_collectorPlaces.Length == 0)
        {
            return null;
        }

        foreach (var collectorPlace in _collectorPlaces)
        {
            if (collectorPlace.IsFree)
            {
                collector.TakePlace(collectorPlace);
                collectorPlace.TakePlace(collector);
                _collectors.Add(collector);

                return collectorPlace;
            }
        }

        return null;
    }

    public void AddCollectorSpawnPoints(CollectorPlace[] collectorPlaces)
    {
        _collectorPlaces.AddRange(collectorPlaces);
    }

}
