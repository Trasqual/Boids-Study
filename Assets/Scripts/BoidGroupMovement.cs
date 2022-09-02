using UnityEngine;

public class BoidGroupMovement : MonoBehaviour
{
    [SerializeField] private BoidData _data;
    [SerializeField] private LayerMask _mask;

    private BoidManager _boidManager;
    private Vector3 _position;
    private bool _canMove = true;

    private Camera _cam;

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
                var randXZ = Random.insideUnitCircle * 10f;
                _position = new Vector3(_position.x + randXZ.x, _position.y, _position.z + randXZ.y);
            }
        }

        var boids = _boidManager.GetBoids();
        for (int i = 0; i < boids.Count; i++)
        {
            if (Vector3.Distance(boids[i].Position, _position) > 1f)
                boids[i].Movement.Move(_position - boids[i].Position);
        }
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
