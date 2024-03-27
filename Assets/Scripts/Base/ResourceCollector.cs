using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitsStorage))]
[RequireComponent(typeof(ResourceScaner))]
[RequireComponent(typeof(ResourceCounter))]
public class ResourceCollector : MonoBehaviour
{
    private ResourceScaner _resourceScaner;
    private ResourceCounter _resourceCounter;
    private UnitsStorage _unitsStorage;

    private void Awake()
    {
        _resourceScaner = GetComponent<ResourceScaner>();
        _resourceCounter = GetComponent<ResourceCounter>();
        _unitsStorage = GetComponent<UnitsStorage>();

        StartCoroutine(CollectResources());
    }

    private IEnumerator CollectResources()
    {
        while (enabled)
        {
            if (_unitsStorage.HasFreeUnit)
            {
                Collider resourceCollider = _resourceScaner.Scan().FirstOrDefault();

                if (resourceCollider != null)
                {
                    if (resourceCollider.TryGetComponent(out Resource resource))
                    {
                        Unit unit = _unitsStorage.GetFreeUnit();
                        unit.CollectResource(resource);
                    }
                }
            }

            yield return null;
        }
    }
}