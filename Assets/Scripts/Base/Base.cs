using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(UnitSpawner))]
[RequireComponent(typeof(ResourceScaner))]
[RequireComponent(typeof(ResourceCounter))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _initialUnitCount;

    private UnitSpawner _unitSpawner;
    private ResourceScaner _resourceScaner;
    private List<Unit> _units;
    private Queue<Unit> _freeUnitsQueue;
    private ResourceCounter _resourceCounter;

    private void Start()
    {
        _unitSpawner = GetComponent<UnitSpawner>();
        _resourceScaner = GetComponent<ResourceScaner>();
        _resourceCounter = GetComponent<ResourceCounter>();
        _units = new List<Unit>();
        _freeUnitsQueue = new Queue<Unit>();

        Init();
    }

    private void Update()
    {
        CollectResources();
    }

    public void LinkUnit(Unit unit)
    {
        unit.SetBase(this);
        _units.Add(unit);
        unit.Freed += OnFreed;
    }

    public void AcceptResource(Resource resource)
    {
        _resourceCounter.Add();
        Destroy(resource.gameObject);
    }

    private void CollectResources()
    {
        if (_freeUnitsQueue.Count > 0 )
        {
            Collider resourceCollider = _resourceScaner.Scan().FirstOrDefault();

            if (resourceCollider != null)
            {
                if (resourceCollider.TryGetComponent(out Resource resource))
                {
                    _freeUnitsQueue.Dequeue().CollectResource(resource);
                }
            }
        }
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
        LinkUnit(unit);
        _freeUnitsQueue.Enqueue(unit);
    }

    private void OnFreed(Unit unit)
    {
        _freeUnitsQueue.Enqueue(unit);
    }
}