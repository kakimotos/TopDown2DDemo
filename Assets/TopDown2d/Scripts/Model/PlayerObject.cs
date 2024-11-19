using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerObject : MonoBehaviour
{
    
    [SerializeField] private PlayerInput input;

    private Transform _transform;
    private float _speed = 5.0f;
    private Vector3 _direction;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        input.actions["Move"].performed += ChangeDirection;
        input.actions["Move"].canceled += ChangeDirection;
    }

    private void Update()
    {
        var position = _transform.position;
        var distance = _speed * Time.deltaTime;

        _transform.position = position + _direction * distance;
    }

    private void ChangeDirection(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().normalized;
    }
    
    
}
