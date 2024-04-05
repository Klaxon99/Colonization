using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(BuildBaseState))]
[RequireComponent(typeof(CreateUnitBaseState))]
[RequireComponent(typeof(UnitsStorage))]
public class Base : MonoBehaviour, ISpawnObject
{
    private Transform _transform;
    private StateMachine _stateMachine;
    private CreateUnitBaseState _createUnitBaseState;
    private BuildBaseState _buildBaseState;
    private Collider _collider;
    private BaseFlag _baseFlag;
    private UnitsStorage _unitsStorage;

    public Vector3 Position => _transform.position;
    public BaseFlag BaseFlag => _baseFlag;
    public Collider Collider => _collider;

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

    public void Build(BaseFlag baseFlag)
    {
        _baseFlag = baseFlag;
        _stateMachine.SwitchState(_buildBaseState);
    }

    public void CreateUnits()
    {
        _stateMachine.SwitchState(_createUnitBaseState);
    }
}