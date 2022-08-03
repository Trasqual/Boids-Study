using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidAvoidance : MonoBehaviour
{
    [SerializeField] LayerMask mask;

    Boid _boid;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void Avoid()
    {
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hit, 2f, mask))
        {
            var thisPos = new Vector3(transform.position.x, 0f, transform.position.z);
            var diff = Vector3.zero;
            if (hit.transform.InverseTransformPoint(thisPos).x > 0)
            {
                diff = new Vector3(1f, 0f, 0f);
            }
            else
            {
                diff = new Vector3(-1f, 0f, 0f);
            }
            _boid.Steer(diff, _boid.Data.avoidanceWeight);
        }

    }

    private void Update()
    {
        Avoid();
    }
}
