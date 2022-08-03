using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidMovement : MonoBehaviour
{
    private Boid _boid;
    private Vector2 _direction;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void Move()
    {
        var movementVector = new Vector3(_direction.x * _boid.Data.horizontalMovementSpeed, 0f, _boid.Data.forwardMovementSpeed);
        _boid.Steer(movementVector, _boid.Data.inputWeight);
    }

    private void Update()
    {
        Move();
    }

    private void OnInputRecieved(Vector2 inputVector)
    {
        _direction = inputVector;
    }

    private void OnInputRelease()
    {
        _direction = Vector2.zero;
    }

    private void OnEnable()
    {
        InputBase.OnInputDrag += OnInputRecieved;
        InputBase.OnInputReleased += OnInputRelease;
    }

    private void OnDisable()
    {
        InputBase.OnInputDrag -= OnInputRecieved;
        InputBase.OnInputReleased -= OnInputRelease;
    }
}
