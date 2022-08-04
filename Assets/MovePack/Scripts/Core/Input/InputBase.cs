using System;
using UnityEngine;

public class InputBase : MonoBehaviour
{
    public Action OnInputPressed;
    public Action OnInputReleased;
    public Action<Vector2> OnInputDrag;
}