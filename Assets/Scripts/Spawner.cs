using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    private Plane _spawnPlace;

    protected Bounds PlaceBounds => _spawnPlace.Bounds;

    public void SetPlace(Plane plane)
    {
        _spawnPlace = plane;
    }

    public virtual bool CanSpawn(Vector3 position, Collider collider)
    {
        Vector3 boxSize = collider.bounds.extents;
        Vector3 center = position;
        center.y = - boxSize.y;

        return !Physics.BoxCast(center, boxSize, Vector3.up);
    }

    public abstract void Spawn();
}