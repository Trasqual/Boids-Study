using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] Boid boidPrefab;

    public Boid SpawnBoid(Vector3 position)
    {
        return Instantiate(boidPrefab, position, Quaternion.identity);
    }
}
