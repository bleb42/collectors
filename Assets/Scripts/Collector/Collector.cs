using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CollectorCollision))]
public class Collector : MonoBehaviour
{
    [SerializeField] private float _speed;

    public bool IsWorking { get; private set; }
    public bool IsResorseTaked { get; private set; }
    public Resource TargetResourseToTake { get; private set; }

    private CollectorPlace _place;
    
    private Coroutine _findResource;
    private Coroutine _bringResource;

    private void Awake()
    {
        IsWorking = false;
        IsResorseTaked= false;
    }

    public void ClaimResourse(Resource resource)
    {
        StopCoroutine(_findResource);
        
        resource.Take(this);
        IsResorseTaked = true;

        _bringResource = StartCoroutine(BringResourceToBase());
    }

    public void TakePlace(CollectorPlace collectorPlace)
    {
        _place = collectorPlace;
    }

    public void TakeResource(Resource resourceToTake)
    {
        _findResource = StartCoroutine(FindResource(resourceToTake));
        TargetResourseToTake= resourceToTake;
    }

    private IEnumerator FindResource(Resource resource)
    {
        IsWorking = true;

        while (transform.position != resource.transform.position - resource.transform.localScale - gameObject.transform.localScale)
        {
            if (resource != null)
            {
                transform.position
                    = Vector3.MoveTowards(transform.position, resource.transform.position, Time.deltaTime * _speed);
            }

            yield return null;
        }
    }

    private IEnumerator BringResourceToBase()
    {
        while (transform.position != _place.transform.position)
        {
            transform.position
                = Vector3.MoveTowards(transform.position, _place.transform.position, Time.deltaTime * _speed);

            yield return null;
        }

        StopCoroutine(_bringResource);
        
        IsWorking = false;
        IsResorseTaked = false;
    }
}