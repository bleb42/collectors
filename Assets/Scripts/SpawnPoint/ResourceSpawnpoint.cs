using UnityEngine;

public class ResourceSpawnpoint : MonoBehaviour
{
    public bool IsResourceSpawned { get; private set; }
   
    private Resource _resource;

    private void Awake()
    {
        IsResourceSpawned = false;
    }

    public void SpawnResource(Resource resource)
    {
        IsResourceSpawned = true;

        _resource = Instantiate(resource, transform.position, Quaternion.identity);
        _resource.SetSpawnpoint(this); 
    }

    public void SetAsFree()
    {
        IsResourceSpawned= false;
    }
}
