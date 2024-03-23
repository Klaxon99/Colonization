using UnityEngine;

[RequireComponent(typeof(UnitHand))]
public class DropResourceTransition : Transition
{
    private UnitHand _hand;

    private void Awake()
    {
        _hand = GetComponent<UnitHand>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BaseZone baseZone))
        {
            IsReady = _hand.IsSelected;
        }
    }
}