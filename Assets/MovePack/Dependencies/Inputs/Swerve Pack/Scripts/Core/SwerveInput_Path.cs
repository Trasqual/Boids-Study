using UnityEngine;

public class SwerveInput_Path : InputBase
{
    private float _previousHorizontal = 0F;
    private float _swipeDelta = 0F;
    public float _horizontalValue = 0F;

    private float UpdateHorizontalValue()
    {
        var current = _horizontalValue;
        
        var target = (Mathf.Lerp(0, 1, Mathf.Abs(_swipeDelta) / Screen.width)) * Mathf.Sign(_swipeDelta);
        
        _horizontalValue = Vector2.MoveTowards(new Vector2(current, 0), new Vector2(target, 0), Time.deltaTime).x;
        
        return _horizontalValue;
    }
    
    private void Update()
    {
        #if UNITY_EDITOR
        
            if (Input.GetMouseButtonDown(0))
            {
                _previousHorizontal = Input.mousePosition.x;
                
                OnInputPressed?.Invoke();
            }
            
            else if (Input.GetMouseButton(0))
            {
                _swipeDelta = Input.mousePosition.x - _previousHorizontal;
                
                _previousHorizontal = Input.mousePosition.x;
                
                OnInputDrag?.Invoke(new Vector2(UpdateHorizontalValue() , 1));
            }
            
            else if (Input.GetMouseButtonUp(0))
            {
                _swipeDelta = 0f;

                UpdateHorizontalValue();
                
                OnInputReleased?.Invoke();
            }

        #else

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    _previousHorizontal = touch.position.x;
                    
                    OnInputPressed?.Invoke();
                }
                
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    _swipeDelta = touch.position.x - _previousHorizontal;
                    
                    _previousHorizontal = touch.position.x;
                    
                    OnInputDrag?.Invoke(new Vector2(UpdateHorizontalValue() , 1));
                }
                
                else if (touch.phase == TouchPhase.Ended)
                {
                    _swipeDelta = 0f;
                    
                    UpdateHorizontalValue();
                    
                    OnInputReleased?.Invoke();
                }
            }

        #endif
    }
}