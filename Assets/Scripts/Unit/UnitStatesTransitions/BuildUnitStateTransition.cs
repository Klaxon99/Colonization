using UnityEngine;

[RequireComponent(typeof(Unit))]
public class BuildUnitStateTransition : Transition
{
    private Unit _unit;

    private void Awake()
    {
        _unit = transform.GetComponent<Unit>();
    }

    private void OnEnable()
    {
        IsReady = _unit.BaseFlag != null;
    }
}