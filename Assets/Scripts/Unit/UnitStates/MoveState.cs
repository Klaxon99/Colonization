using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(UnitHand))]
public class MoveState : State
{
    private float _contactDistance;
    private Unit _unit;
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _unit = GetComponent<Unit>();
        _contactDistance = GetComponent<UnitHand>().Length;
    }

    private void Update()
    {
        Vector3 direction = _unit.MoveTargetPosition - transform.position;

        if (Vector3.Magnitude(direction) > _contactDistance)
        {
            _mover.Move(direction.normalized);
        }
        else
        {
            SwitchState();
        }
    }
}