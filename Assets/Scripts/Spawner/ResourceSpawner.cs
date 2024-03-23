using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] private Resource _resource;
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    protected override Resource GetSpawnObject() => _resource;

    private IEnumerator SpawnWithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;

            Spawn();
        }
    }
}