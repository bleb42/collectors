using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class ResourceScan : MonoBehaviour
{
    [SerializeField] private float _scanSpeed;

    private Resource[] _resources = new Resource[0];

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

    private void OnDisable()
    {
        if (_scan != null)
            StopCoroutine(_scan);
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