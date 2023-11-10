using UnityEngine;
using UnityEngine.Events;

public class BaseResources : MonoBehaviour
{
    [SerializeField] private UnityEvent _updateScore; 
    
    public int Score { get; private set; }

    private void Awake()
    {
        Score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            Destroy(resource.gameObject);
            Score++;
            _updateScore.Invoke();
        }
    }
}