using System.Linq;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;
    [SerializeField] private StateMachine _stateMachine;

    public void Enter()
    {
        if (enabled == false)
        {
            SwitchActive(true);
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            SwitchActive(false);
        }
    }

    protected void SwitchState()
    {
        _stateMachine.SwitchState(GetNextState());
    }

    private void SwitchActive(bool isActive)
    {
        enabled = isActive;

        foreach (Transition transition in _transitions)
        {
            transition.enabled = enabled;
        }
    }

    private State GetNextState()
    {
        Transition transition = _transitions.FirstOrDefault(transition => transition.IsReady);

        if (transition != null)
        {
            return transition.GetState();
        }

        return null;
    }
}