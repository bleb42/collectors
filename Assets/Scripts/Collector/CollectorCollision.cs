using UnityEngine;

[RequireComponent(typeof(Collector))]
public class CollectorCollision : MonoBehaviour
{
    private Collector _collector;

    private void Start()
    {
        _collector = GetComponent<Collector>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            if (_collector.TargetResourseToTake == resource)
            {
                if (!_collector.IsResorseTaked)
                {
                    _collector.ClaimResourse(resource);
                }
            }
        }
    }
}
