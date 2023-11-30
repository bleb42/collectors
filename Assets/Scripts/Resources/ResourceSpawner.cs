using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnSpeed = 1f;
    [SerializeField] private BaseScan _baseScan;
    [SerializeField] private ResourceSpawnpoint[] _spawnpoints;
    [SerializeField] private Resource[] _resources;

    private Coroutine _spawnResorces;

    private void Start()
    {
        _spawnResorces = StartCoroutine(SpawnResource());
    }

    private IEnumerator SpawnResource()
    {
        WaitForSeconds spawnSpeed = new WaitForSeconds(_spawnSpeed);
        Resource resource;

        while (true)
        {
            foreach (var spawnpoint in _spawnpoints)
            {
                if (!spawnpoint.IsResourceSpawned)
                {
                    resource = _resources[Random.Range(0, _resources.Length)];
                    _baseScan.AddResourceToTake(spawnpoint.SpawnResource(resource));

                    yield return spawnSpeed;
                }
            }

            yield return spawnSpeed;
        }
    }
}