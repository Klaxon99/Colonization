using UnityEngine;

[RequireComponent(typeof(ResourceCounter))]
[RequireComponent(typeof(ResourceCollector))]
[RequireComponent(typeof(UnitsStorage))]
public class CreateUnitBaseState : State
{
    private ResourceCounter _resources;
    private ResourceCollector _resourceCollector;
    private UnitsStorage _unitsStorage;

    private void Awake()
    {
        _resources = GetComponent<ResourceCounter>();
        _resourceCollector = GetComponent<ResourceCollector>();
        _unitsStorage = GetComponent<UnitsStorage>();
    }

    private void Start()
    {
        _resourceCollector.Run();
    }

    private void OnEnable()
    {
        _resources.AccumulatedUnitCost += OnAccumulatedUnitCost;
    }

    private void OnDisable()
    {
        _resources.AccumulatedUnitCost -= OnAccumulatedUnitCost;
    }

    private void OnAccumulatedUnitCost()
    {
        _unitsStorage.CreateUnit();
        _resources.BuyUnit();
    }
}