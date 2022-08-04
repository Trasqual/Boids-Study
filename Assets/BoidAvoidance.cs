using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidAvoidance : MonoBehaviour
{
    [SerializeField] LayerMask _mask;
    [SerializeField] float _avoidanceRadius = 1f;
    [SerializeField] float _avoidanceRange = 2f;
    [SerializeField] float _avoidancePower = 0.5f;

    Boid _boid;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void Avoid()
    {
        if (Physics.SphereCast(transform.position + transform.forward, _avoidanceRadius, transform.forward, out RaycastHit hit, _avoidanceRange, _mask))
        {
            var hitPos = new Vector3(hit.transform.position.x, 0f, hit.transform.position.z);
            var thisPos = new Vector3(transform.position.x, 0f, transform.position.z);
            var vec = Vector3.zero;
            if (hit.transform.InverseTransformPoint(thisPos).x > 0)
            {
                vec = new Vector3(_avoidancePower, 0f, 0f);
            }
            else
            {
                vec = new Vector3(-_avoidancePower, 0f, 0f);
            }
            _boid.Steer(vec, _boid.Data.avoidanceWeight);
        }

    }

    private void Update()
    {
        Avoid();
    }
}
