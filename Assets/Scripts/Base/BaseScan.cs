using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class BaseScan : MonoBehaviour
{
    [SerializeField] private float _scanSpeed;

    private Resource[] _resources;

    private Coroutine _scan;
    private Base _base;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void Start()
    {
        _scan = StartCoroutine(Scan());
    }

    public void AddResourceToTake(Resource newResource)
    {
        Resource[] newArray = new Resource[_resources.Length + 1];

        for (int i = 0; i < _resources.Length; i++)
        {
            newArray[i] = _resources[i];
        }

        newArray[_resources.Length] = newResource;
        _resources = newArray;
    }

    private IEnumerator Scan()
    {
        WaitForSeconds scanReload = new WaitForSeconds(_scanSpeed);

        while (true) 
        {
            _resources = null;  
            _resources = FindObjectsOfType<Resource>();

            if (_resources.Length >= 0)
            {
                foreach (var resource in _resources) 
                {
                    if (!resource.IsATarget)
                    {
                        _base.AppointCollector(resource);

                        yield return scanReload;
                    }
                }
            }

            yield return scanReload;
        }
    }
}
