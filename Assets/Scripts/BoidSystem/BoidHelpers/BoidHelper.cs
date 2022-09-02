using UnityEngine;

public abstract class BoidHelper : MonoBehaviour
{
    protected Boid _boid;

    protected virtual void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    protected virtual void Update()
    {
        Perform();
    }

    protected abstract void Perform();
}
