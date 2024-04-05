using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(UnitSpawner))]
[RequireComponent(typeof(ResourceCollector))]
public class UnitsStorage : MonoBehaviour
{
    [SerializeField] private int _initialUnitCount;

    public event Action<Unit> UnitFreed;

    private Queue<Unit> _freeUnitsQueue;
    private Base _base;
    private ResourceCollector _resourceCollector;
    private UnitSpawner _unitSpawner;

    public bool HasFreeUnit => _freeUnitsQueue.Count > 0;
    public int Count => _freeUnitsQueue.Count;

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

    public void CreateUnit()
    {
        Unit unit = _unitSpawner.Spawn();
        unit.Init(_base, _resourceCollector);
        _freeUnitsQueue.Enqueue(unit);
        Add(unit);
    }

    public void Add(Unit unit)
    {
        LinkUnit(unit);
    }

    public Unit GetFreeUnit()
    {
        return _freeUnitsQueue.Dequeue();
    }

    public Unit GiveUnit()
    {
        if (_freeUnitsQueue.Count > 0)
        {
            Unit unit = _freeUnitsQueue.Dequeue();

            unit.Freed -= OnFreed;

            return unit;
        }

        return null;
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

    private void OnFreed(Unit unit)
    {
        _freeUnitsQueue.Enqueue(unit);
        UnitFreed?.Invoke(unit);
    }
}