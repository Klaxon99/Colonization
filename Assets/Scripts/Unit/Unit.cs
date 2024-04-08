using System;
using UnityEngine;

[RequireComponent (typeof(UnitHand))]
[RequireComponent (typeof(StateMachine))]
[RequireComponent(typeof(TakeResourceState))]
[RequireComponent (typeof(BuildUnitState))]
public class Unit : MonoBehaviour, ISpawnObject
{
    [SerializeField] private Collider _collider;

    public event Action<Unit> Freed;
    public event Action BaseBuilded;

    private Base _base;
    private UnitHand _hand;
    private StateMachine _stateMachine;
    private BuildUnitState _buildUnitState;
    private ResourceCollector _resourceCollector;
    private TakeResourceState _takeResourceState;

    public Collider Collider => _collider;
    public Base Base => _base;
    public Resource TargetResource { get; private set; }
    public BaseFlag BaseFlag { get; private set; }

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _hand = GetComponent<UnitHand>();
        _buildUnitState = GetComponent<BuildUnitState>();
        _takeResourceState = GetComponent<TakeResourceState>();
    }

    private void OnEnable()
    {
        _hand.ResourceDropped += OnDroppedResource;
    }

    private void OnDisable()
    {
        _hand.ResourceDropped -= OnDroppedResource;
    }

    public void BuildBase(BaseFlag baseFlag)
    {
        BaseFlag = baseFlag;
        _stateMachine.SwitchState(_buildUnitState);
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
        _stateMachine.SwitchState(_takeResourceState);
    }

    public void SetBase(Base newBase)
    {
        _base = newBase;
    }

    private void OnBaseBuilded()
    {
        BaseFlag = null;
        _hand.BaseBuilded -= OnBaseBuilded;
        BaseBuilded?.Invoke();
    }

    private void OnDroppedResource()
    {
        _resourceCollector.TakeResource(TargetResource);
        TargetResource = null;
        Freed?.Invoke(this);
    }
}