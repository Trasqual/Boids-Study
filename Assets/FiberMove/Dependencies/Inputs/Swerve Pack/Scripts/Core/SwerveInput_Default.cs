using UnityEngine;

public class SwerveInput_Default : InputBase
{
    [SerializeField] private float inputSensitivity = 100;
    
    private float _previousHorizontal = 0;
    private float _swipeDelta = 0;

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
                _swipeDelta = Mathf.Lerp(_swipeDelta, Input.mousePosition.x - _previousHorizontal,
                    Time.deltaTime * inputSensitivity);
                
                _previousHorizontal = Input.mousePosition.x;

                OnInputDrag?.Invoke(new Vector2(_swipeDelta , 1));
            }
            
            else if (Input.GetMouseButtonUp(0))
            {
                _swipeDelta = 0f;
                
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
                    _swipeDelta = Mathf.Lerp(_swipeDelta, Input.mousePosition.x - _previousHorizontal,
                        Time.deltaTime * inputSensitivity);
                    
                    _previousHorizontal = touch.position.x;
                    
                    OnInputDrag?.Invoke(new Vector2(_swipeDelta , 0));
                }
                
                else if (touch.phase == TouchPhase.Ended)
                {
                    _swipeDelta = 0f;
                    
                    OnInputReleased?.Invoke();
                }
            }
        
        #endif
    }
}