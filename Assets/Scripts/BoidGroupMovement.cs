using System.Collections;
using System.Linq;
using UnityEngine;

public class BoidGroupMovement : MonoBehaviour
{
    [SerializeField] protected BoidData _data;

    protected InputBase _input;
    protected BoidManager _boidManager;
    protected Vector3 _direction;
    protected bool _canMove = true;

    private void Awake()
    {
        _input = GetComponent<InputBase>();
        _boidManager = GetComponent<BoidManager>();
    }

    private void Update()
    {
        if (!_canMove) return;
        var movementVector = new Vector3(_direction.x * _data.horizontalMovementSpeed, 0f, _data.forwardMovementSpeed);
        var boids = _boidManager.GetBoids();
        for (int i = 0; i < boids.Count; i++)
        {
            boids[i].Movement.Move(movementVector);
        }
    }

    private IEnumerator PerformGroupJump()
    {
        if (_canMove)
        {
            var boids = _boidManager.GetBoids();

            var organizedBoids = boids.OrderBy(x => -transform.InverseTransformPoint(x.transform.position).z).ToList();
            var groupSize = Mathf.FloorToInt(Mathf.Lerp(1, 5, boids.Count / 20));
            for (int i = 0; i < organizedBoids.Count; i++)
            {
                organizedBoids[i].Movement.OnJump();
                if (i % groupSize == 0)
                    yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void OnInputPress()
    {

    }

    private void OnInput(Vector2 input)
    {
        _direction = new Vector3(input.x, 0f, input.y);
    }

    private void OnInputRelease()
    {
        _direction = Vector3.zero;
        StartCoroutine(PerformGroupJump());
    }

    private void EnableMovement()
    {
        _canMove = true;
    }

    private void DisableMovement()
    {
        _canMove = false;
    }

    private void OnEnable()
    {
        _input.OnInputPressed += OnInputPress;
        _input.OnInputDrag += OnInput;
        _input.OnInputReleased += OnInputRelease;

        //GameManager.OnGameStart += EnableMovement;
        //GameManager.OnGameLose += DisableMovement;
        //GameManager.OnGameWin += DisableMovement;
    }

    private void OnDisable()
    {
        _input.OnInputPressed -= OnInputPress;
        _input.OnInputDrag -= OnInput;
        _input.OnInputReleased -= OnInputRelease;

        //GameManager.OnGameStart -= EnableMovement;
        //GameManager.OnGameLose -= DisableMovement;
        //GameManager.OnGameWin -= DisableMovement;
    }
}
