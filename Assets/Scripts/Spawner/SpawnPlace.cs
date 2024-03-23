using UnityEngine;

[RequireComponent (typeof(Collider))]
public class SpawnPlace : MonoBehaviour
{
    private Collider _colider;

    public Bounds Bounds => _colider.bounds;

    private void Awake()
    {
        _colider = GetComponent<Collider>();
    }
}