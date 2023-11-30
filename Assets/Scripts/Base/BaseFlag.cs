using UnityEngine;

[RequireComponent(typeof(Base))]
public class BaseFlag : MonoBehaviour
{
    [SerializeField] private GameObject _flagPrefab;

    public GameObject Flag { get; private set; }

    private Base _base;
    
    private bool _isSelected = false;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_base.IsSecondBaseBuilded)
            {
                if (_isSelected)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.gameObject.TryGetComponent(out Map map))
                        {
                            Vector3 clickPosition = hitInfo.point;

                            if (Flag == null)
                            {
                                Flag = TrySpawnFlag(clickPosition);
                            }
                            else
                            {
                                Destroy(Flag);
                                Flag = TrySpawnFlag(clickPosition);
                            }

                            _isSelected = false;
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (_isSelected)
        {
            _isSelected = false;
        }
        else
        {
            _isSelected = true;
        }
    }

    public GameObject TrySpawnFlag(Vector3 position)
    {
        position.y += _flagPrefab.transform.localScale.y;

        if (Flag == null)
        {
            Flag = Instantiate(_flagPrefab, position, Quaternion.identity);

            return Flag ;
        }

        return null;    
    }

    public void DestroyFlag()
    {
        Destroy(Flag);
        Flag= null;
    }
}
