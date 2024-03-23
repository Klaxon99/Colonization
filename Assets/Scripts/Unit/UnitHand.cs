using System;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitHand : MonoBehaviour
{
    [SerializeField] private float _height;
    
    public bool IsSelected {  get; private set; }

    public event Action ResourceSelected;
    public event Action ResourceDropped;

    private Unit _unit;

    public float Height => _height;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void TakeResource()
    {
        if (CanTakeResource() == true)
        {
            Transform resource = _unit.TargetResource.transform;
            resource.SetParent(transform);
            resource.localPosition = new Vector3(0, Height, Height);
            ResourceSelected?.Invoke();
            IsSelected = true;
        }
    }

    public bool CanTakeResource()
    {
        if (_unit.TargetResource != null)
        {
            return Vector3.Distance(_unit.transform.position, _unit.TargetResource.Position) <= _height;
        }

        return false;
    }

    public void DropResource()
    {
        _unit.TargetResource.transform.SetParent(null);
        IsSelected = false;
        ResourceDropped?.Invoke();
    }
}