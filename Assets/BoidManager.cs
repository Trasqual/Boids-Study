using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidSpawner))]
public class BoidManager : MonoBehaviour
{
    [SerializeField] int boidAmount = 50;

    BoidSpawner _boidSpawner;

    List<Boid> _boids = new List<Boid>();

    public List<Boid> GetBoids()
    {
        return _boids;
    }

    private void Awake()
    {
        _boidSpawner = GetComponent<BoidSpawner>();
        SetupBoids();
    }

    private void SetupBoids()
    {
        for (int i = 0; i < boidAmount; i++)
        {
            var pos = transform.position + (Random.insideUnitSphere * Random.Range(3, 10));

            var spawnedBoid = _boidSpawner.SpawnBoid(pos);
            spawnedBoid.Initialize(this);
            AddBoid(spawnedBoid);
        }
    }

    public void AddBoid(Boid boid)
    {
        if (!_boids.Contains(boid))
            _boids.Add(boid);
    }

    public void RemoveBoid(Boid boid)
    {
        _boids.Remove(boid);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(40, 40, 40));
    }
}
