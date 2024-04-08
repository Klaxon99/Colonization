using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(BuildBaseState))]
[RequireComponent(typeof(CreateUnitBaseState))]
[RequireComponent(typeof(UnitsStorage))]
[RequireComponent(typeof(ResourceCollector))]
public class Base : MonoBehaviour, ISpawnObject
{
    private Collider _collider;
    private BaseFlag _baseFlag;
    private Transform _transform;
    private StateMachine _stateMachine;
    private UnitsStorage _unitsStorage;
    private BuildBaseState _buildBaseState;
    private CreateUnitBaseState _createUnitBaseState;

    public Vector3 Position => _transform.position;
    public BaseFlag BaseFlag => _baseFlag;
    public Collider Collider => _collider;
    public bool CanBuild => BaseFlag == null;
    public BaseSpawner BaseSpawner {  get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _transform = GetComponent<Transform>();
        _stateMachine = GetComponent<StateMachine>();
        _unitsStorage = GetComponent<UnitsStorage>();
        _createUnitBaseState = GetComponent<CreateUnitBaseState>();
        _buildBaseState = GetComponent<BuildBaseState>();
    }

    public void TakeUnit(Unit unit)
    {
        _unitsStorage.Add(unit);
    }

    public void Build(BaseFlag baseFlag, BaseSpawner baseSpawner)
    {
        BaseSpawner = baseSpawner;
        _baseFlag = baseFlag;
        _stateMachine.SwitchState(_buildBaseState);
    }

    public void CreateUnits()
    {
        _stateMachine.SwitchState(_createUnitBaseState);
    }
}