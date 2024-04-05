using System;
using UnityEngine;

[RequireComponent (typeof(UnitHand))]
public class BuildUnitState : State
{
    private UnitHand _unitHand;

    private void Awake()
    {
        _unitHand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _unitHand.BuildBase();
        SwitchState();
    }
}