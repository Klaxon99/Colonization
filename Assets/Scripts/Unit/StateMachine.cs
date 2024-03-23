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
        if (state == null)
        {
            _currentState = _baseState;
        }

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}