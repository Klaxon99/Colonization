using UnityEngine;

[RequireComponent(typeof(UnitHand))]
public class TakeResourceState : State
{
    private UnitHand _unitHand;

    private void Awake()
    {
        _unitHand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _unitHand.TakeResource();
        SwitchState();
    }
}