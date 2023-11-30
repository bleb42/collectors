using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _price = 1;

    public bool IsATarget { get; private set; }
    public int Price { get; private set; }

    private ResourceSpawnpoint _spawnpoint;

    private void Awake()
    {
        Price = _price;
        IsATarget = false;
    }

    public void SetAsTarget()
    {
        IsATarget= true;
    }

    public void Take(Collector collector)
    {
        transform.SetParent(collector.transform);
        _spawnpoint.SetAsFree();
    }

    public void SetSpawnpoint(ResourceSpawnpoint spawnpoint)
    {
        _spawnpoint= spawnpoint;
    }
}