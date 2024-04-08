using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _contactDistance;

    public event Action Finished;

    private Vector3 _movePosition;
    private Coroutine _coroutine;

    public void Move(Vector3 position)
    {
        position.y = transform.position.y;
        _movePosition = position;
        _coroutine = StartCoroutine(MoveTowards());
    }

    public void Stop()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator MoveTowards()
    {
        Vector3 direction = (_movePosition - transform.position).normalized;
        direction.y = transform.forward.y;
        transform.forward = direction;


        while (Vector3.Distance(transform.position, _movePosition) > _contactDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _movePosition, _speed * Time.deltaTime);

            yield return null;
        }

        Finished?.Invoke();
    }
}