using UnityEngine;

[CreateAssetMenu]
public class BoidData : ScriptableObject
{
    public float horizontalMovementSpeed = 10f;
    public float forwardMovementSpeed = 7f;
    public float minSpeed = 3f;
    public float maxSpeed = 8f;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1f;
    public float maxSteerForce = 3f;

    public float alignmentWeight = 2f;
    public float cohesionWeight = 1f;
    public float seperationWeight = 2.5f;
    public float inputWeight = 5f;
}
