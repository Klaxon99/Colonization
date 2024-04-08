using UnityEngine;

[RequireComponent(typeof(UnitHand))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Unit))]
public class TakeResourceState : State
{
    private Unit _unit;
    private UnitHand _unitHand;
    private Mover _mover;

    private void Awake()
    {
        _unitHand = GetComponent<UnitHand>();
        _mover = GetComponent<Mover>();
        _unit = GetComponent<Unit>();
    }

    private void OnEnable()
    {
        _mover.Finished += OnFinished;
        _mover.Move(_unit.TargetResource.Position);
    }

    private void OnDisable()
    {
        _mover.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        _unitHand.TakeResource();
        SwitchState();
    }
}