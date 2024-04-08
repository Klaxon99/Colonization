using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(UnitSpawner))]
[RequireComponent(typeof(ResourceCollector))]
public class UnitsStorage : MonoBehaviour
{
    [SerializeField] private int _initialUnitCount;

    public event Action UnitFreed;

    private Base _base;
    private UnitSpawner _unitSpawner;
    private Queue<Unit> _freeUnitsQueue;
    private ResourceCollector _resourceCollector;

    public bool HasFreeUnit => _freeUnitsQueue.Count > 0;
    public int Count => _freeUnitsQueue.Count;

    private void Start()
    {
        Init();
    }

    private void Awake()
    {
        _base = GetComponent<Base>();
        _freeUnitsQueue = new Queue<Unit>();
        _unitSpawner = GetComponent<UnitSpawner>();
        _resourceCollector = GetComponent<ResourceCollector>();
    }

    public void CreateUnit()
    {
        Unit unit = _unitSpawner.Spawn();
        unit.Init(_base, _resourceCollector);
        Add(unit);
    }

    public void Add(Unit unit)
    {
        _freeUnitsQueue.Enqueue(unit);
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
        unit.Init(_base, _resourceCollector);
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
        UnitFreed?.Invoke();
    }
}