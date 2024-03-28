using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateTransition : Transition
{
    [SerializeField] private Unit _unit;

    protected Unit Unit => _unit;
}
