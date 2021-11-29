using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMove : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = Vector3.zero;
    [SerializeField] private float _speed = 0;
    [SerializeField] private Transform _resetPosition = null;
    [SerializeField] private float _distance = 0;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _resetPosition.position);

        if (distance >= _distance)
        {
            transform.position = _resetPosition.position;
        }

        transform.position += _direction * _speed * Time.deltaTime;
    }
}
