using UnityEngine;

public class BoidRepellant : MonoBehaviour
{
    [SerializeField] float _pushForce = 25f;
    [SerializeField] float _pushRange = 2f;
    [SerializeField] float _pushWeigth = 3f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Boid boid))
        {
            var boidPos = new Vector3(boid.transform.position.x, 0f, boid.transform.position.z);
            var pos = new Vector3(transform.position.x, 0f, transform.position.z);
            var diff = boidPos - pos;

            var desiredVelocity = diff.normalized * _pushForce;
            boid.Steer(desiredVelocity, _pushWeigth);
        }
    }
}
