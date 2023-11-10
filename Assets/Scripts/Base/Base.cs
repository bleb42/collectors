using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseResources))]
[RequireComponent(typeof(BaseScan))]
public class Base : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private int _startingNumberOfCollectors = 3;

    private CollectorPlace[] _collectorPlaces;
    
    private List<Collector> _collectors;

    private void Awake()
    {
        _collectorPlaces = GetComponentsInChildren<CollectorPlace>();
        _collectors = new List<Collector>();
    }

    private void Start()
    {
        for (int i = 0; i < _startingNumberOfCollectors; i++)
        {
            SpawnCollector();
        }
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

    private void SpawnCollector()
    {
        if (_collectorPlaces.Length == 0)
        {
            return;
        }

        foreach (var collectorPlace in _collectorPlaces)
        {
            if (collectorPlace.IsFree)
            {
                Collector collector = Instantiate(_collector, collectorPlace.transform.position, Quaternion.identity);
                
                collector.TakePlace(collectorPlace);
                collectorPlace.TakePlace();
                _collectors.Add(collector);

                return;
            }
        }
    }
}
