using UnityEngine;

[CreateAssetMenu]
public class BoidData : ScriptableObject
{
    public float horizontalMovementSpeed = 10f;
    public float forwardMovementSpeed = 7f;

    public float minSpeed = 5f;
    public float maxSpeed = 8f;
    public float perceptionRadius = 10f;
    public float seperationRadius = 2f;
    public float maxSteerForce = 8f;

    public float alignmentWeight = 2f;
    public float cohesionWeight = 1f;
    public float seperationWeight = 2.5f;
    public float inputWeight = 5f;
    public float avoidanceWeight = 20f;
    public float noiseWeight = 0.05f;
    public float grouperWeight = 0.05f;

    public float limitX = 5f;
}
