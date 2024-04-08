using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(UnitHand))]
public class DropResourceState : State
{
    private Unit _unit;
    private Mover _mover;
    private UnitHand _hand;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _mover = GetComponent<Mover>();
        _hand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _mover.Move(_unit.Base.Position);
        _mover.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _mover.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        SwitchState();
        _hand.DropResource();
    }
}