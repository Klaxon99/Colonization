using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(UnitSpawner))]
[RequireComponent(typeof(ResourceCollector))]
public class UnitsStorage : MonoBehaviour
{
    [SerializeField] private int _initialUnitCount;

    private Queue<Unit> _freeUnitsQueue;
    private Base _base;
    private ResourceCollector _resourceCollector;
    private UnitSpawner _unitSpawner;

    public bool HasFreeUnit => _freeUnitsQueue.Count > 0;

    private void Start()
    {
        Init();
    }

    private void Awake()
    {
        _base = GetComponent<Base>();
        _unitSpawner = GetComponent<UnitSpawner>();
        _freeUnitsQueue = new Queue<Unit>();
        _resourceCollector = GetComponent<ResourceCollector>();
    }

    public Unit GetFreeUnit()
    {
        return _freeUnitsQueue.Dequeue();
    }

    public void LinkUnit(Unit unit)
    {
        unit.SetBase(_base);
        unit.Freed += OnFreed;
    }

    private void Init()
    {
        for (int i = 0; i < _initialUnitCount; i++)
        {
            CreateUnit();
        }
    }

    private void CreateUnit()
    {
        Unit unit = _unitSpawner.Spawn();
        unit.Init(_base, _resourceCollector);
        LinkUnit(unit);
        _freeUnitsQueue.Enqueue(unit);
    }

    private void OnFreed(Unit unit)
    {
        _freeUnitsQueue.Enqueue(unit);
    }
}