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

    public bool IsStopped { get; private set; }

    private void Awake()
    {
        _boid = GetComponent<Boid>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
    }

    public void Move(Vector3 movementVector)
    {
        IsStopped = false;
        _boid.Steer(movementVector, _boid.Data.inputWeight);
        ApplyGravity();
    }

    public void Stop()
    {
        IsStopped = true;
        _boid.Stop();
    }

    private void ApplyGravity()
    {
        if (_groundChecker.hit.transform != null && !_jumped)
        {
            _dropTimer = 0f;

            _gravity = Vector3.zero;
            _boid.transform.position += _gravity * Time.deltaTime;
            var pos = transform.position;
            pos.y = _groundChecker.detectedPoint.y;
            transform.position = pos;
        }
        else
        {
            _gravity = Physics.gravity;
            _boid.transform.position += _gravity * Time.deltaTime;
            _dropTimer += Time.deltaTime;
            if (_dropTimer >= _boid.Data.deathAfterDropTime)
            {
                OnFellToDeath?.Invoke();
            }
        }
    }
}