using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Resource : MonoBehaviour, ISpawnObject
{
    [SerializeField] private Collider _collider;

    private string _occupiedMaskName = "Default";
    private LayerMask _occupiedMask;

    public bool IsOccupied {  get; private set; }
    public Vector3 Position => transform.position;

    public Collider Collider => _collider;

    private void Awake()
    {
        _occupiedMask = LayerMask.NameToLayer(_occupiedMaskName);
    }

    public void Occupy()
    {
        gameObject.layer = _occupiedMask;
        IsOccupied = true;
    }
}