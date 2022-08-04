using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidMovement : MonoBehaviour
{
    private Vector3 _jumpVelocity = Vector3.zero;
    private Vector3 _jumpRefVel = Vector3.zero;

    private Boid _boid;
    private Vector2 _direction;

    private bool _canJump;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void Move()
    {
        //if (!_canMove) return;
        var movementVector = new Vector3(_direction.x * _boid.Data.horizontalMovementSpeed, 0f, _boid.Data.forwardMovementSpeed);
        Debug.Log(movementVector);
        _boid.Steer(movementVector * _boid.Data.inputWeight);
    }

    private void Update()
    {
        Move();
        Jump();
        ApplyGravity();
    }

    private void Jump()
    {
        if (_canJump)
        {
            _jumpVelocity = _boid.Data.jumpVelocity;
            _canJump = false;
        }
        else
        {
            _jumpVelocity = Vector3.SmoothDamp(_jumpVelocity, Vector3.zero, ref _jumpRefVel, 0.75f);
        }
        _boid.Steer(_jumpVelocity);
    }

    private void ApplyGravity()
    {
        if (transform.position.y > 0.1f && !_canJump)
            _boid.Steer(Physics.gravity);
        else
        {
            //var pos = transform.position;
            //pos.y = 0f;
            //transform.position = pos;
        }
    }

    private void OnInputRecieved(Vector2 inputVector)
    {
        _direction = inputVector;
    }

    private void OnInputRelease()
    {
        _direction = Vector2.zero;

        _canJump = true;
    }

    private void OnInputPress()
    {

    }

    private void OnEnable()
    {
        InputBase.OnInputPressed += OnInputPress;
        InputBase.OnInputDrag += OnInputRecieved;
        InputBase.OnInputReleased += OnInputRelease;
    }

    private void OnDisable()
    {
        InputBase.OnInputPressed -= OnInputPress;
        InputBase.OnInputDrag -= OnInputRecieved;
        InputBase.OnInputReleased -= OnInputRelease;
    }
}
