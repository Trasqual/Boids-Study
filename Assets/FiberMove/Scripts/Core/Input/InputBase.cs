using System;
using UnityEngine;

public class InputBase : MonoBehaviour
{
    public static Action OnInputPressed;
    public static Action OnInputReleased;
    public static Action<Vector2> OnInputDrag;
}