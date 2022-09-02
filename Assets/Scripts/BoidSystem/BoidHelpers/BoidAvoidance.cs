using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidAvoidance : BoidHelper
{
    [SerializeField] LayerMask _mask;
    [SerializeField] float _avoidanceRadius = 1f;
    [SerializeField] float _avoidanceRange = 2f;

    protected override void Perform()
    {
        if (Physics.SphereCast(transform.position - transform.forward * 2f, _avoidanceRadius, transform.forward, out RaycastHit hit, _avoidanceRange, _mask))
        {
            var hitPos = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
            var thisPos = transform.position;
            var vec = Vector3.zero;
            if (hit.transform.InverseTransformPoint(thisPos).x > 0)
            {
                vec = new Vector3(1, 0f, 0f);
            }
            else
            {
                vec = new Vector3(-1, 0f, 0f);
            }
            _boid.Steer(vec, _boid.Data.avoidanceWeight);
        }
    }
}
