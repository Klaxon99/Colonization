using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targerState;
    [SerializeField] private Unit _unit;

    public bool IsReady { get; protected set; } = false;

    protected Unit Unit => _unit;

    public State GetState()
    {
        return _targerState;
    }
}