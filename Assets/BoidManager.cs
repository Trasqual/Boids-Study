using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidSpawner))]
public class BoidManager : MonoBehaviour
{
    [SerializeField] int boidAmount = 50;

    BoidSpawner _boidSpawner;

    List<Boid> _boids = new List<Boid>();
    List<BoidMovement> _boidMovements = new List<BoidMovement>();

    public List<Boid> GetBoids()
    {
        return _boids;
    }

    public List<BoidMovement> GetBoidMovements()
    {
        return _boidMovements;
    }

    private void Awake()
    {
        _boidSpawner = GetComponent<BoidSpawner>();
        SetupBoids();
    }

    private void Update()
    {
        MoveWithGroup();
    }

    private void MoveWithGroup()
    {
        if (_boids.Count <= 0) return;

        var center = Vector3.zero;
        for (int i = 0; i < _boids.Count; i++)
        {
            center += _boids[i].Position;
        }
        center /= _boids.Count;
        transform.position = Vector3.Lerp(transform.position, center + transform.up * 3f, Time.deltaTime * 10f);
    }

    private void SetupBoids()
    {
        for (int i = 0; i < boidAmount; i++)
        {
            var pos = Random.insideUnitCircle;
            var spawnPos = new Vector3(pos.x * Random.Range(0f, 5f), 0.5f, pos.y * Random.Range(0f, 5f));

            var spawnedBoid = _boidSpawner.SpawnBoid(spawnPos);
            spawnedBoid.Initialize(this);
            AddBoid(spawnedBoid);
        }
    }

    public void AddBoid(Boid boid)
    {
        if (!_boids.Contains(boid))
            _boids.Add(boid);

        if (boid.TryGetComponent(out BoidMovement boidMovement))
        {
            if (!_boidMovements.Contains(boidMovement))
            {
                _boidMovements.Add(boidMovement);
            }
        }
    }

    public void RemoveBoid(Boid boid)
    {
        if (_boids.Contains(boid))
            _boids.Remove(boid);
        if (boid.TryGetComponent(out BoidMovement boidMovement))
        {
            if (_boidMovements.Contains(boidMovement))
            {
                _boidMovements.Remove(boidMovement);
            }
        }
    }
}
