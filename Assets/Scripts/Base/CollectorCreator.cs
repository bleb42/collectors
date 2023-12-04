using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(BaseResources))]
public class CollectorCreator : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private int _collectorPrice = 3;

    public List<Collector> Collectors { get; private set; }
    public CollectorPlace[] CollectorPlaces { get; private set; }
    public int CollectorPrice { get; private set; }

    private void Awake()
    {
        Collectors= new List<Collector>();

        CollectorPlaces = GetComponentsInChildren<CollectorPlace>();

        CollectorPrice = _collectorPrice;
    }

    public bool TrySpawnCollector()
    {
        if (CollectorPlaces.Length == 0)
        {
            return false;
        }

        foreach (var collectorPlace in CollectorPlaces)
        {
            if (collectorPlace.IsFree)
            {
                Collector collector = Instantiate(_collector, collectorPlace.transform.position, Quaternion.identity);

                collector.TakePlace(collectorPlace);
                collectorPlace.TakePlace(collector);
                Collectors.Add(collector);

                return true;
            }
        }

        return false;
    }

    public CollectorPlace TryAddCollector(Collector collector)
    {
        if (CollectorPlaces.Length == 0)
        {
            return null;
        }

        foreach (var collectorPlace in CollectorPlaces)
        {
            if (collectorPlace.IsFree)
            {
                collector.TakePlace(collectorPlace);
                collectorPlace.TakePlace(collector);
                Collectors.Add(collector);

                return collectorPlace;
            }
        }

        return null;
    }
}
