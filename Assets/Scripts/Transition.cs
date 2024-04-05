using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targerState;

    public bool IsReady { get; protected set; } = false;

    public State GetState()
    {
        return _targerState;
    }
}