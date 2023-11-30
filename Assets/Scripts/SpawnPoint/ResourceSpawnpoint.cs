using UnityEngine;

public class ResourceSpawnpoint : MonoBehaviour
{
    public bool IsResourceSpawned { get; private set; }
   
    private Resource _resource;

    private void Awake()
    {
        IsResourceSpawned = false;
    }

    public Resource SpawnResource(Resource resource)
    {
        IsResourceSpawned = true;

        _resource = Instantiate(resource, transform.position, Quaternion.identity);
        _resource.SetSpawnpoint(this); 
    
        return _resource;
    }

    public void SetAsFree()
    {
        IsResourceSpawned = false;
    }
}
