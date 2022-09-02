using System.Collections;
using UnityEngine;

public class BoidGroupMovement : MonoBehaviour
{
    [SerializeField] private BoidData _data;
    [SerializeField] private LayerMask _mask;

    private BoidManager _boidManager;
    private Vector3 _position;
    private bool _canMove = true;

    private Camera _cam;

    IEnumerator curMovement;

    private void Awake()
    {
        _boidManager = GetComponent<BoidManager>();
        _cam = Camera.main;
    }

    private void Update()
    {
        if (!_canMove) return;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _mask))
            {
                _position = hit.point;
                var randXZ = Random.insideUnitCircle * 20f;
                _position = new Vector3(_position.x + randXZ.x, _position.y, _position.z + randXZ.y);
                CancelCurrentMovement();
                curMovement = StartMovement();
                StartCoroutine(curMovement);
            }
        }
    }

    private void CancelCurrentMovement()
    {
        if (curMovement != null)
        {
            StopCoroutine(curMovement);
        }
    }

    private IEnumerator StartMovement()
    {
        var boids = _boidManager.GetBoids();
        for (int i = 0; i < boids.Count; i++)
        {
            boids[i].PrepareForMovement();
            boids[i].SetVelocity(_position - boids[i].Position);
        }

        while (!AllBoidsStopped())
        {
            for (int i = 0; i < boids.Count; i++)
            {
                if (Vector3.Distance(boids[i].Position, _position) > 2f)
                    boids[i].Movement.Move(_position - boids[i].Position);
                else
                    boids[i].Stop();
            }
            yield return null;
        }
    }

    private bool AllBoidsStopped()
    {
        foreach (var boid in _boidManager.GetBoids())
        {
            if (!boid.Movement.IsStopped)
            {
                return false;
            }
        }
        return true;
    }

    private void EnableMovement()
    {
        _canMove = true;
    }

    private void DisableMovement()
    {
        _canMove = false;
    }
}
