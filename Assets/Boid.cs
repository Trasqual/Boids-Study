using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] float posLimitX = 20;
    [SerializeField] float posLimitY = 20;
    [SerializeField] float posLimitZ = 20;
    [SerializeField] BoidData _data;

    private BoidManager _boidManager;

    private Vector3 _velocity;
    private Vector3 _acceleration;

    public BoidData Data => _data;
    public BoidManager Manager => _boidManager;
    public Vector3 Position { get; private set; }

    public Vector3 GetVelocity() => _velocity;

    public void Initialize(BoidManager boidManager)
    {
        _boidManager = boidManager;
        _velocity = Random.insideUnitSphere;
    }

    public void Steer(Vector3 direction, float weight)
    {
        var steer = direction.normalized * _data.maxSpeed - _velocity;
        _acceleration += Vector3.ClampMagnitude(steer, _data.maxSteerForce) * weight;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
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

        LimitPosition();

        Position = transform.position;
    }

    private void LimitPosition()
    {
        if (transform.position.x > posLimitX)
        {
            transform.position = new Vector3(-posLimitX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -posLimitX)
        {
            transform.position = new Vector3(posLimitX, transform.position.y, transform.position.z);
        }
        if (transform.position.y > posLimitY)
        {
            transform.position = new Vector3(transform.position.x, -posLimitY, transform.position.z);
        }
        if (transform.position.y < -posLimitY)
        {
            transform.position = new Vector3(transform.position.x, posLimitY, transform.position.z);
        }
        if (transform.position.z > posLimitZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -posLimitZ);
        }
        if (transform.position.z < -posLimitZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, posLimitZ);
        }
    }
}
