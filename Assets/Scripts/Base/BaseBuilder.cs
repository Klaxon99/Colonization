using UnityEngine;

[RequireComponent (typeof(InputHandler))]
public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private BaseFlag _baseFlag;

    private InputHandler _inputHandler;
    private Base _creatingBase;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void OnEnable()
    {
        _inputHandler.ClickedOnBase += OnBaseClick;
        _inputHandler.Clicked += OnSpawnPlaceClick;
    }

    private void OnBaseClick(Base unitBase)
    {
        _creatingBase = unitBase;
    }

    private void OnSpawnPlaceClick(Vector3 position)
    {
        if (_creatingBase != null)
        {
            if (_baseSpawner.CanSpawn(position, _creatingBase.Collider))
            {
                BaseFlag flag = Instantiate(_baseFlag, position, Quaternion.identity);
                _creatingBase.Build(flag);
                _creatingBase = null;
            }
        }
    }
}