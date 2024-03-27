using UnityEngine;

[RequireComponent (typeof(UnitSpawner))]
[RequireComponent(typeof(ResourceCounter))]
public class Base : MonoBehaviour
{
    private ResourceCounter _resourceCounter;

    private void Start()
    {
        _resourceCounter = GetComponent<ResourceCounter>();
    }

    public void AcceptResource(Resource resource)
    {
        _resourceCounter.Add();
        Destroy(resource.gameObject);
    }
}