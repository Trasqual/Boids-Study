using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Action OnLanded;

    [SerializeField] LayerMask mask;
    private bool _isGrounded;
    public Vector3 detectedPoint;
    public RaycastHit hit;

    bool _jumping;

    void Update()
    {

        if (Physics.Raycast(transform.position + transform.up, -transform.up, out hit, 1.2f, mask))
        {
            _isGrounded = true;
            detectedPoint = hit.point;
        }
        else
        {
            _jumping = true;
            _isGrounded = false;
        }
    }

    public bool IsGrounded() => _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && _jumping)
        {
            _jumping = false;
            OnLanded?.Invoke();
        }
    }
}
