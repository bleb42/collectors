using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CollectorCollision))]
public class Collector : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _actionRadious = 1f;
    [SerializeField] private float _buildingSpeed = 2f;

    public bool IsWorking { get; private set; }
    public bool IsResorseTaked { get; private set; }
    public Resource TargetResourseToTake { get; private set; }
    public bool NeedToBuildBase { get; private set; }

    private CollectorPlace _place;
    
    private Coroutine _findResource;
    private Coroutine _goToBase;
    private Coroutine _buildBase;

    private Vector3 _flagPosition;
    private GameObject _basePrefab;
    private BaseFlag _baseFlag;

    private void Awake()
    {
        IsWorking = false;
        IsResorseTaked = false;
        NeedToBuildBase = false;
    }

    public void ClaimResourse(Resource resource)
    {
        StopCoroutine(_findResource);
        
        resource.Take(this);
        IsResorseTaked = true;

        _goToBase = StartCoroutine(GoToBase());
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

    public void BuildBase(Vector3 position, GameObject basePrefab, BaseFlag baseFlag)
    {
        NeedToBuildBase = true;
        IsWorking = true;

        _basePrefab = basePrefab;
        _flagPosition = position;
        _baseFlag = baseFlag;
    }

    private IEnumerator BuildBase()
    {
        _place.SetFree();
        _place = null;

        WaitForSeconds timeToBuildBase = new WaitForSeconds(Time.deltaTime / _buildingSpeed);

        while (Vector3.Distance(transform.position, _flagPosition) > _actionRadious)
        {
            transform.position
                = Vector3.MoveTowards(transform.position, _flagPosition, Time.deltaTime * _speed);

            yield return null;
        }

        yield return timeToBuildBase;

        _baseFlag.DestroyFlag();

        GameObject newBase = Instantiate(_basePrefab, new Vector3(_flagPosition.x, 0, _flagPosition.z), Quaternion.identity);

        if (newBase.TryGetComponent(out Base Base))
        {
            Base.TryAddCollector(this);
        }

        IsWorking = false;
        NeedToBuildBase= false;

        StopCoroutine(_buildBase);
    }

    private IEnumerator FindResource(Resource resource)
    {
        IsWorking = true;

        while (transform.position != resource.transform.position)
        {
            if (resource != null)
            {
                transform.position
                    = Vector3.MoveTowards(transform.position, resource.transform.position, Time.deltaTime * _speed);
            }

            yield return null;
        }
    }

    private IEnumerator GoToBase()
    {
        while (transform.position != _place.transform.position)
        {
            transform.position
                = Vector3.MoveTowards(transform.position, _place.transform.position, Time.deltaTime * _speed);

            yield return null;
        }

        StopCoroutine(_goToBase);
        IsResorseTaked = false;

        if (NeedToBuildBase)
        {
            _buildBase = StartCoroutine(BuildBase());
        }
        else
        {
            IsWorking = false;
        }
    }
}