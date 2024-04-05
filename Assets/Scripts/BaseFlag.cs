using UnityEngine;

[RequireComponent(typeof(Transform))]
public class BaseFlag : MonoBehaviour
{
    private Transform _transform;

    public Vector3 Position => _transform.position;

    private void Awake()
    {
        _transform = transform;
    }
}
