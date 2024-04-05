using UnityEngine;

public class BaseSpawner : Spawner<Base>
{
    [SerializeField] private Base _basePrefab;

    protected override Base GetSpawnObject()
    {
        return _basePrefab;
    }
}