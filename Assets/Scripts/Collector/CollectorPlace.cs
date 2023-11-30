using System.Collections.ObjectModel;
using UnityEngine;

public class CollectorPlace : MonoBehaviour
{
    public bool IsFree { get; private set; }

    public Collector Collector { get; private set; }

    private void Awake()
    {
        IsFree = true;
    }

    public void TakePlace(Collector collector)
    {
        IsFree = false;
        Collector = collector;
    }

    public void SetFree()
    {
        IsFree = true;
        Collector = null;
    }
}