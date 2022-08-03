using UnityEngine;

[CreateAssetMenu]
public class BoidData : ScriptableObject
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1f;
    public float maxSteerForce = 3f;

    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float seperationWeight = 1f;

    public float inputWeight = 1f;
}
