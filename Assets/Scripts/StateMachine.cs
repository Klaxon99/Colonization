using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _baseState;

    private State _currentState;

    private void Awake()
    {
        _currentState = _baseState;
    }

    public void SwitchState(State state)
    {
        _currentState?.Exit();
        _currentState = state == null ? _baseState : state;
        _currentState.Enter();
    }
}