using System;
using UnityEngine;

[RequireComponent (typeof(UnitHand))]
[RequireComponent (typeof(StateMachine))]
[RequireComponent(typeof(MoveState))]
[RequireComponent (typeof(BuildUnitState))]
public class Unit : MonoBehaviour, ISpawnObject
{
    [SerializeField] private Collider _collider;

    public event Action<Unit> Freed;

    private Base _base;
    private ResourceCollector _resourceCollector;
    private MoveState _moveState;
    private StateMachine _stateMachine;
    private UnitHand _hand;

    public Collider Collider => _collider;
    public Base Base => _base;
    public Resource TargetResource { get; private set; }
    public Vector3 MoveTargetPosition {  get; private set; }
    public BaseFlag BaseFlag { get; private set; }

    private void Awake()
    {
        _moveState = GetComponent<MoveState>();
        _stateMachine = GetComponent<StateMachine>();
        _hand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _hand.ResourceSelected += OnSelectedResource;
        _hand.ResourceDropped += OnDroppedResource;
    }

    private void OnDisable()
    {
        _hand.ResourceSelected -= OnSelectedResource;
        _hand.ResourceDropped -= OnDroppedResource;
    }

    public void BuildBase(BaseFlag baseFlag)
    {
        BaseFlag = baseFlag;
        MoveTargetPosition = BaseFlag.Position;
        _stateMachine.SwitchState(_moveState);
        _hand.BaseBuilded += OnBaseBuilded;
    }

    public void Init(Base unitBase, ResourceCollector resourceCollector)
    {
        _base = unitBase;
        _resourceCollector = resourceCollector;
    }

    public void CollectResource(Resource resource)
    {
        resource.Occupy();
        TargetResource = resource;
        MoveTargetPosition = resource.transform.position;
        _stateMachine.SwitchState(_moveState);
    }

    public void SetBase(Base newBase)
    {
        _base = newBase;
    }

    private void OnBaseBuilded()
    {
        BaseFlag = null;
        _hand.BaseBuilded -= OnBaseBuilded;
    }

    private void OnSelectedResource()
    {
        MoveTargetPosition = _base.Position;
    }

    private void OnDroppedResource()
    {
        _resourceCollector.TakeResource(TargetResource);
        TargetResource = null;
        Freed?.Invoke(this);
    }
}