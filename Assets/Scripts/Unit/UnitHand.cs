using System;
using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(BaseSpawner))]
public class UnitHand : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private Base _basePrefab;

    public event Action ResourceSelected;
    public event Action ResourceDropped;
    public event Action BaseBuilded;

    private Unit _unit;
    private BaseSpawner _baseSpawner;

    public float Length => _length;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _baseSpawner = GetComponent<BaseSpawner>();
    }

    public void TakeResource()
    {
        if (CanTakeResource() == true)
        {
            Transform resource = _unit.TargetResource.transform;
            resource.SetParent(transform);
            resource.localPosition = new Vector3(0, Length, Length);
            ResourceSelected?.Invoke();
        }
    }

    public bool CanTakeResource()
    {
        if (_unit.TargetResource != null)
        {
            return Vector3.Distance(_unit.transform.position, _unit.TargetResource.Position) <= _length;
        }

        return false;
    }

    public void DropResource()
    {
        _unit.TargetResource.transform.SetParent(null);
        ResourceDropped?.Invoke();
    }

    public void BuildBase()
    {
        Vector3 spawnPosition = _unit.Base.BaseFlag.Position;
        float spawnHeight = 0.07f;
        spawnPosition.y += spawnHeight;

        Base newBase = _unit.Base.BaseSpawner.Spawn(spawnPosition);
        _unit.Base.BaseFlag.gameObject.SetActive(false);
        newBase.TakeUnit(_unit);
        BaseBuilded?.Invoke();
    }
}