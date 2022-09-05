using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidGroupMovement : MonoBehaviour
{
    [SerializeField] private BoidData _data;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _mousePosVisual;
    List<GameObject> _prevMousePosVis = new List<GameObject>();

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
                if (_prevMousePosVis.Count > 0)
                {
                    foreach (var vis in _prevMousePosVis)
                    {
                        Destroy(vis);
                    }
                }
                _position = hit.point;
                var positions = new Vector3[_boidManager.GetBoids().Count];
                var girdSize = Mathf.CeilToInt(Mathf.Sqrt(positions.Length));
                var grid = new Grid(girdSize, girdSize, 2f);
                var gridPositions = grid.GridPositions();
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[i] = _position + gridPositions[i];
                }
                CancelCurrentMovement();
                curMovement = StartMovement(positions);
                StartCoroutine(curMovement);
                for (int i = 0; i < positions.Length; i++)
                {
                    _prevMousePosVis.Add(Instantiate(_mousePosVisual, positions[i], Quaternion.identity));
                }
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

    private IEnumerator StartMovement(Vector3[] endPositions)
    {
        var boids = _boidManager.GetBoids();
        for (int i = 0; i < boids.Count; i++)
        {
            boids[i].PrepareForMovement();
            boids[i].SetVelocity(endPositions[i] - boids[i].Position);
            boids[i].Movement.IsStopped = false;
        }

        while (!AllBoidsStopped())
        {
            for (int i = 0; i < boids.Count; i++)
            {
                if (Vector3.Distance(boids[i].Position, endPositions[i]) > .5f)
                    boids[i].Movement.Move((endPositions[i] - boids[i].Position).normalized);
                else
                    boids[i].Movement.Stop();
            }
            yield return null;
        }

        if (_prevMousePosVis.Count > 0)
        {
            foreach (var vis in _prevMousePosVis)
            {
                Destroy(vis);
            }
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

    private struct Grid
    {
        private int _width;
        private int _height;
        private float _cellSize;

        public Grid(int width, int height, float cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
        }

        public Vector3[] GridPositions()
        {
            var positions = new Vector3[_width * _height];

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    positions[i * _width + j] = new Vector3(-(((_width - 2) * _cellSize) / 2f) + i * _cellSize, 0f, -(((_height - 1) * _cellSize) / 2f) + j * _cellSize);
                }
            }
            return positions;
        }
    }
}
