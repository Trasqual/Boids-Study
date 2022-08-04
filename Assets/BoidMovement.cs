using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidMovement : MonoBehaviour
{
    public Action OnFellToDeath;
    private Vector3 _jumpVelocity = Vector3.zero;
    private Vector3 _jumpRefVel = Vector3.zero;
    private Vector3 _gravity;

    private Boid _boid;
    private GroundChecker _groundChecker;
    private BoidCohesion _boidCohesion;

    private float _dropTimer;
    private bool _jumped;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
        _boidCohesion = GetComponent<BoidCohesion>();
    }

    public void Move(Vector3 movementVector)
    {
        _boid.Steer(new Vector3(_groundChecker.IsGrounded() ? movementVector.x : 0f, movementVector.y, movementVector.z) * _boid.Data.inputWeight);
        _jumpVelocity = Vector3.SmoothDamp(_jumpVelocity, Vector3.zero, ref _jumpRefVel, 0.75f);
        _boid.Steer(_jumpVelocity);

        ApplyGravity();
    }

    private void Jump()
    {
        if (_groundChecker.IsGrounded())
        {
            _jumpVelocity = _boid.Data.jumpVelocity;
        }
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

    public void OnJump()
    {
        Jump();
        _jumped = true;
        if (_boidCohesion != null)
        {
            _boidCohesion.enabled = false;
        }
        DOVirtual.DelayedCall(0.1f, () => _jumped = false);
    }

    private void ReEnableCohAfterJump()
    {
        if (_boidCohesion != null)
        {
            _boidCohesion.enabled = true;
        }
    }

    private void OnEnable()
    {
        _groundChecker.OnLanded += ReEnableCohAfterJump;
    }

    private void OnDisable()
    {
        _groundChecker.OnLanded -= ReEnableCohAfterJump;
    }
}
