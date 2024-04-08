using System;
using UnityEngine;

[RequireComponent(typeof(UnitHand))]
public class DropResourceTransition : Transition
{
    private UnitHand _hand;

    private void Awake()
    {
        _hand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _hand.ResourceSelected += OnSelected;
    }

    private void OnDisable()
    {
        _hand.ResourceSelected -= OnSelected;
        IsReady = false;
    }

    private void OnSelected()
    {
        IsReady = true;
    }
}