using System.Collections;
using UnityEngine;

public class ResourceScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _layerMask;

    public Collider[] Scan() => Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);
}
