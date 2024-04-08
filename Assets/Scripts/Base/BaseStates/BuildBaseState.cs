using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(UnitsStorage))]
[RequireComponent(typeof(ResourceCounter))]
[RequireComponent(typeof(ResourceCollector))]
public class BuildBaseState : State
{
    private Base _base;
    private ResourceCollector _resourceCollector;
    private UnitsStorage _unitsStorage;
    private ResourceCounter _resourceCounter;
    private Unit _builder;

    private void Awake()
    {
        _base = GetComponent<Base>();
        _unitsStorage = GetComponent<UnitsStorage>();
        _resourceCounter = GetComponent<ResourceCounter>();
        _resourceCollector = GetComponent<ResourceCollector>();
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
        _resourceCounter.BuyBase();
        _builder.BaseBuilded += OnBaseBuilded;
    }

    private void OnBaseBuilded()
    {
        SwitchState();
    }

    private void OnUnitFreed()
    {
        SendUnit(_unitsStorage.GiveUnit());
        _unitsStorage.UnitFreed -= OnUnitFreed;
        _resourceCollector.Run();
    }
}