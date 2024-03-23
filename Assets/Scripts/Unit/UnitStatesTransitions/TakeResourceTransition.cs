using UnityEngine;

[RequireComponent (typeof(UnitHand))]
public class TakeResourceTransition : Transition
{
    private UnitHand _unitHand;

    private void Awake()
    {
        _unitHand = GetComponent<UnitHand>();
    }

    private void Update()
    {
        IsReady = _unitHand.CanTakeResource();
    }
}