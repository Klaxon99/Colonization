using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(UnitsStorage))]
[RequireComponent(typeof(ResourceCollector))]
[RequireComponent(typeof(ResourceCounter))]
public class BuildBaseState : State
{
    private Base _base;
    private UnitsStorage _unitsStorage;
    private ResourceCollector _resourceCollector;
    private ResourceCounter _resourceCounter;
    private Unit _builder;

    private void Awake()
    {
        _base = GetComponent<Base>();
        _unitsStorage = GetComponent<UnitsStorage>();
        _resourceCollector = GetComponent<ResourceCollector>();
        _resourceCounter = GetComponent<ResourceCounter>();
    }

    private void OnEnable()
    {
        if (_resourceCounter.Count >= _resourceCounter.BaseCost)
        {
            Build();
        }
        else
        {
            _resourceCounter.AccumulatedBaseCost += Build;
        }
    }

    private void OnDisable()
    {
        _resourceCounter.AccumulatedBaseCost -= Build;
    }

    private void Build()
    {
        _resourceCollector.Stop();

        if (_unitsStorage.HasFreeUnit)
        {
            //Не давать юнита, если у базы небольше одного юнита
            SendUnit(_unitsStorage.GiveUnit());
        }
        else
        {
            _unitsStorage.UnitFreed += OnUnitFreed;
        }
    }

    private void SendUnit(Unit unit)
    {
        _builder = unit;
        _builder.BuildBase(_base.BaseFlag);
        Debug.Log(unit);
        SwitchState();
    }

    private void OnUnitFreed(Unit unit)
    {
        SendUnit(unit);
        _builder.Freed -= OnUnitFreed;
    }
}