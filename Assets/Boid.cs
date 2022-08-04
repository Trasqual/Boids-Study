using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] BoidData _data;
    [SerializeField] BoidMovement _boidMovement;

    private BoidManager _boidManager;

    private Vector3 _velocity;
    private Vector3 _acceleration;

    private Vector3 _previousLookDir;
    private Vector3 _lookDirRef;

    public BoidData Data => _data;
    public BoidManager Manager => _boidManager;
    public BoidMovement Movement => _boidMovement;
    public Vector3 Position { get; private set; }

    public Vector3 GetVelocity() => _velocity;

    public void Initialize(BoidManager boidManager)
    {
        _boidManager = boidManager;
    }

    public void Steer(Vector3 direction, float weight)
    {
        var steer = direction.normalized * _data.maxSpeed - _velocity;
        _acceleration += Vector3.ClampMagnitude(steer, _data.maxSteerForce) * weight;
    }

    public void Steer(Vector3 direction, float weight, float speed)
    {
        var steer = direction.normalized * speed - _velocity;
        _acceleration += Vector3.ClampMagnitude(steer, _data.maxSteerForce) * weight;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void Steer(Vector3 velocity)
    {
        _acceleration += velocity;
    }

    private void Update()
    {
        _velocity += _acceleration;
        //var dir = _velocity.normalized;
        //var speed = _velocity.magnitude;
        //speed = Mathf.Clamp(speed, _data.minSpeed, _data.maxSpeed);
        //_velocity = dir * speed;
        transform.position += _velocity * Time.deltaTime;

        var lookDir = Vector3.zero;
        if (Mathf.Abs(transform.position.x) <= _data.limitX + 1f && Mathf.Abs(transform.position.x) >= _data.limitX - 0.1f)
        {
            lookDir = Vector3.SmoothDamp(_previousLookDir, Vector3.forward, ref _lookDirRef, 0.2f);
        }
        else
        {
            lookDir = Vector3.SmoothDamp(_previousLookDir, new Vector3(_velocity.x, 0f, _velocity.z), ref _lookDirRef, 0.2f);
        }
        transform.LookAt(transform.position + lookDir);
        _previousLookDir = lookDir;

        _velocity = _acceleration = Vector3.zero;

        LimitPosition();

        Position = transform.position;
    }

    private void LimitPosition()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, Random.Range(-_data.limitX + 0.2f, -_data.limitX - 0.2f), Random.Range(_data.limitX - 0.2f, _data.limitX + 0.2f));
        transform.position = pos;
    }
}
