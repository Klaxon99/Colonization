using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Plane : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;

    private Collider _colider;

    public Bounds Bounds => _colider.bounds;

    private void Start()
    {
        _colider = GetComponent<Collider>();
        _resourceSpawner.SetPlace(this);
    }
}