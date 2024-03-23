using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    [SerializeField] private Unit _unitPrefab;

    protected override Unit GetSpawnObject() => _unitPrefab;
}