using System;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidMovement : MonoBehaviour
{
    public Action OnFellToDeath;
    private Vector3 _gravity;

    private Boid _boid;
    private GroundChecker _groundChecker;

    private float _dropTimer;
    private bool _jumped;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
    }

    public void Move(Vector3 movementVector)
    {
        _boid.Steer(movementVector, _boid.Data.inputWeight);

        //ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_groundChecker.hit.transform != null && !_jumped)
        {
            _dropTimer = 0f;

            _gravity = Vector3.zero;
            _boid.Steer(_gravity);
            var pos = transform.position;
            pos.y = _groundChecker.detectedPoint.y;
            transform.position = pos;
        }
        else
        {
            _gravity = Physics.gravity;
            _boid.Steer(_gravity);
            _dropTimer += Time.deltaTime;
            if (_dropTimer >= _boid.Data.deathAfterDropTime)
            {
                OnFellToDeath?.Invoke();
            }
        }
    }
}