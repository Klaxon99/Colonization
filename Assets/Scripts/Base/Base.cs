using UnityEngine;

[RequireComponent (typeof(UnitSpawner))]
[RequireComponent(typeof(ResourceCounter))]
public class Base : MonoBehaviour
{
    private Transform _transform;

    public Vector3 Position => _transform.position;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
}