using UnityEngine;

[RequireComponent(typeof(UnitHand))]
public class DropResourceState : State
{
    private UnitHand _hand;

    private void Awake()
    {
        _hand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _hand.DropResource();
        SwitchState();
    }
}