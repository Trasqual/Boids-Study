using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidNoise : BoidHelper
{
    [SerializeField] float _noisePower = 0.01f;
    [SerializeField] float _noiseTimer = 3f;
    float _noiseCounter = 0f;
    Vector3 _noiseVector;

    protected override void Awake()
    {
        base.Awake();
        _noiseTimer = Random.Range(_noiseTimer * 0.5f, _noiseTimer * 1.5f);
    }

    protected override void Perform()
    {
        _noiseCounter += Time.deltaTime;
        if (_noiseCounter >= _noiseTimer)
        {
            _noiseVector = new Vector3(Random.Range(-_noisePower, _noisePower), 0f, Random.Range(-_noisePower, _noisePower));
            _noiseCounter = 0f;
        }
        _noiseVector = Vector3.Lerp(_noiseVector, Vector3.zero, Time.deltaTime * 30f);
        _boid.Steer(_noiseVector, _boid.Data.noiseWeight);
    }
}
