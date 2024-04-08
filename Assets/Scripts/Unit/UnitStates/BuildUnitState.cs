using UnityEngine;

[RequireComponent (typeof(Unit))]
[RequireComponent (typeof(Mover))]
[RequireComponent (typeof(UnitHand))]
public class BuildUnitState : State
{
    private Unit _unit;
    private UnitHand _unitHand;
    private Mover _mover;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _mover = GetComponent<Mover>();
        _unitHand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _mover.Move(_unit.BaseFlag.Position);
        _mover.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _mover.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        _unitHand.BuildBase();
        SwitchState();
    }
}