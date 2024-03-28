using System;
using UnityEngine;

[RequireComponent(typeof(MoveState))]
[RequireComponent (typeof(UnitHand))]
[RequireComponent (typeof(StateMachine))]
[RequireComponent (typeof(IdleState))]
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
    public Resource TargetResource { get; private set; }
    public Vector3 MoveTargetPosition {  get; private set; }

    private void Awake()
    {
        _moveState = GetComponent<MoveState>();
        _stateMachine = GetComponent<StateMachine>();
        _hand = GetComponent<UnitHand>();
    }

    private void OnEnable()
    {
        _hand.ResourceSelected += OnSelected;
        _hand.ResourceDropped += OnDropped;
    }

    private void OnDisable()
    {
        _hand.ResourceSelected -= OnSelected;
        _hand.ResourceDropped -= OnDropped;
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

    private void OnSelected()
    {
        MoveTargetPosition = _base.Position;
    }

    private void OnDropped()
    {
        _resourceCollector.TakeResource(TargetResource);
        Freed?.Invoke(this);
    }
}