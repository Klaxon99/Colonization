using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed;

    public void Move(Vector3 direction)
    {
        direction.y = transform.forward.y;
        transform.forward = direction;
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }
}