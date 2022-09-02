using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidGrouper : MonoBehaviour
{
    [SerializeField] float _groupingDistance = 8f;
    Boid _boid;

    private void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    private void GroupUp()
    {
        var boids = _boid.Manager.GetBoids();

        Vector3 average = Vector3.zero;
        var count = 0;
        for (int i = 0; i < boids.Count; i++)
        {
            if (boids[i] != _boid)
            {
                average += new Vector3(boids[i].Position.x, 0f, boids[i].Position.z);
                count++;
            }
        }

        if (count > 0)
        {
            average /= count;
        }
        var dist = (average - new Vector3(transform.position.x, 0f, transform.position.z)).magnitude;
        average -= new Vector3(transform.position.x, 0f, transform.position.z);
        if (dist > _groupingDistance)
            _boid.Steer(average, _boid.Data.grouperWeight);
    }

    private void Update()
    {
        GroupUp();
    }
}
