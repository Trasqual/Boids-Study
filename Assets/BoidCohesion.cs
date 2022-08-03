using UnityEngine;

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
                average += boids[i].Position;
                count++;
            }
        }

        if (count > 0)
        {
            average /= count;
            average -= transform.position;
        }

        _boid.Steer(average, _boid.Data.cohesionWeight);
    }

    private void Update()
    {
        Cohesion();
    }
}
