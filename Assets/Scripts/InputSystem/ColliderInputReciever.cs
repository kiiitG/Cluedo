using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderInputReciever : InputReciever
{
    private Vector2 touchPosition;
    private TouchControl touchControl;
    private bool isTouching;
    private bool touchDone;

    public new void Awake()
    {
        inputHandlers = GetComponents<IInputHandler>();
        touchControl = new TouchControl();
        touchControl.Touch.TouchPress.started += context => OnTouchPressStart(context);
        touchControl.Touch.TouchPress.canceled += context => OnTouchPressEnd(context);
        isTouching = false;
        touchDone = false;
    }

    public void OnEnable()
    {
        touchControl.Enable();
    }

    public void OnDisable()
    {
        touchControl.Disable();
    }

    private void OnTouchPressStart(InputAction.CallbackContext context)
    {
        isTouching = true;
    }

    private void OnTouchPressEnd(InputAction.CallbackContext context)
    {
        isTouching = false;
        touchDone = true;
    }

    public void Update()
    {
        if (isTouching)
        {
            touchPosition = touchControl.Touch.TouchPosition.ReadValue<Vector2>();
            if (touchDone && GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(touchPosition)))
            {
                Debug.Log("touch is caught");
                OnInputRecieved();
                touchDone = false;
            }
        }
    }

    public override void OnInputRecieved()
    {
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(touchPosition, gameObject, null);
        }
    }
}
