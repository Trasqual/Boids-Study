using UnityEngine;

public class BoidSeperation : MonoBehaviour
{
    Boid _boid;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void Seperate()
    {
        var boids = _boid.Manager.GetBoids();

        Vector3 average = Vector3.zero;
        var count = 0;
        for (int i = 0; i < boids.Count; i++)
        {
            var dist = (transform.position - boids[i].Position).magnitude;
            if (boids[i] != _boid && dist < _boid.Data.perceptionRadius)
            {
                var diff = transform.position - boids[i].Position;
                diff /= diff.magnitude * diff.magnitude;
                average += diff;
                count++;
            }
        }

        if (count > 0)
        {
            average /= count;
        }

        _boid.Steer(average, _boid.Data.seperationWeight);
    }

    private void Update()
    {
        Seperate();
    }
}
