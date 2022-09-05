using UnityEngine;

public abstract class BoidHelper : MonoBehaviour
{
    protected Boid _boid;

    protected virtual void Awake()
    {
        _boid = GetComponent<Boid>();
    }

    public virtual void UpdateHelper()
    {
        Perform();
    }

    protected abstract void Perform();
}
