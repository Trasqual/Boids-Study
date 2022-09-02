using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidCohesion : MonoBehaviour
{
    Boid _boid;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void Cohesion()
    {
        var boids = _boid.Manager.GetBoids();

        Vector3 average = Vector3.zero;
        var count = 0;
        for (int i = 0; i < boids.Count; i++)
        {
            var dist = (transform.position - boids[i].Position).magnitude;
            if (boids[i] != _boid && dist < _boid.Data.perceptionRadius)
            {
                average += new Vector3(boids[i].Position.x, transform.position.y, boids[i].Position.z);
                count++;
            }
        }

        if (count > 0)
        {
            average /= count;
            average -= new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        _boid.Steer(average, _boid.Data.cohesionWeight);
    }

    private void Update()
    {
        Cohesion();
    }
}
