using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    public bool IsATarget { get; private set; }

    private ResourceSpawnpoint _spawnpoint;

    private void Awake()
    {
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