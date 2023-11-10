using UnityEngine;

public class CollectorPlace : MonoBehaviour
{
    public bool IsFree { get; private set; }

    private void Awake()
    {
        IsFree = true;
    }

    public void TakePlace()
    {
        IsFree = false;
    }
}