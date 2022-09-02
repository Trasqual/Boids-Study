using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] BoidData _data;
    [SerializeField] BoidMovement _boidMovement;

    private BoidManager _boidManager;

    private Vector3 _velocity;
    private Vector3 _acceleration;

    public BoidData Data => _data;
    public BoidManager Manager => _boidManager;
    public BoidMovement Movement => _boidMovement;
    public Vector3 Position { get; private set; }

    public Vector3 GetVelocity() => _velocity;

    private BoidHelper[] helpers;

    public void Initialize(BoidManager boidManager)
    {
        _boidManager = boidManager;
        helpers = GetComponents<BoidHelper>();
        Stop();
    }

    public void Steer(Vector3 direction, float weight)
    {
        var steer = direction.normalized * _data.maxSpeed - _velocity;
        _acceleration += Vector3.ClampMagnitude(steer, _data.maxSteerForce) * weight;
    }

    public void Steer(Vector3 velocity)
    {
        _acceleration += velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void PrepareForMovement()
    {
        foreach (var helper in helpers)
        {
            helper.enabled = true;
        }
    }

    public void Stop()
    {
        foreach (var helper in helpers)
        {
            helper.enabled = false;
        }
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        _velocity += _acceleration * Time.deltaTime;
        var dir = _velocity.normalized;
        var speed = _velocity.magnitude;
        speed = Mathf.Clamp(speed, _data.minSpeed, _data.maxSpeed);
        _velocity = dir * speed;

        transform.position += _velocity * Time.deltaTime;
        transform.forward = dir;

        _acceleration = Vector3.zero;

        Position = transform.position;
    }
}
