using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnObject
{
    [SerializeField] private SpawnPlace _spawnPlace;

    protected Bounds PlaceBounds => _spawnPlace.Bounds;

    public virtual T Spawn()
    {
        T spawnObject = Instantiate(GetSpawnObject());
        Vector3 spawnPosition = GetFreePlace(spawnObject.Collider);
        spawnObject.transform.position = spawnPosition;

        return spawnObject;
    }

    public T Spawn(Vector3 position)
    {
        T spawnObject = Instantiate(GetSpawnObject());
        spawnObject.transform.position = position;
        position.y += 1;
        return spawnObject;
    }

    public virtual bool CanSpawn(Vector3 position, Collider collider)
    {
        Vector3 boxSize = collider.bounds.extents;
        Vector3 center = position;
        center.y = - boxSize.y;

        return !Physics.BoxCast(center, boxSize, Vector3.up);
    }

    protected abstract T GetSpawnObject();

    private Vector3 GetFreePlace(Collider spawnObjectCollider)
    {
        Vector3 position = GenerateRandomPosition();

        while (CanSpawn(position, spawnObjectCollider) == false)
        {
            position = GenerateRandomPosition();
        }

        position.y += spawnObjectCollider.bounds.extents.y;

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