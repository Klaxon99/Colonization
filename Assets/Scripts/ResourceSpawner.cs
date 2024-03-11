using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner
{
    [SerializeField] private Collider _resource;
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    public override void Spawn()
    {
        var resource = Instantiate(_resource);
        Vector3 spawnPos = GetFreePlace();
        resource.transform.position = spawnPos;
    }

    private IEnumerator SpawnWithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return wait;

            Spawn();
        }
    }

    private Vector3 GetFreePlace()
    {
        Vector3 position = GenerateRandomPosition();

        while (CanSpawn(position, _resource) == false)
        {
            position = GenerateRandomPosition();
        }

        return position;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position;
        Vector3 minBound = PlaceBounds.min;
        Vector3 maxBound = PlaceBounds.max;

        position.x = Random.Range(minBound.x, maxBound.x);
        position.z = Random.Range(minBound.z, maxBound.z);
        position.y = PlaceBounds.max.y;

        return position;
    }
}