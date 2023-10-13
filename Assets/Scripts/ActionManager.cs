using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

public class ActionManager : Singleton<ActionManager> 
{
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("JumpHold was started");
        else if (context.performed) {
            Debug.Log("JumpHold was performed");
            Debug.Log(context.duration);
            jumpHold.Invoke();
        } else if (context.canceled)
            Debug.Log("JumpHold was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started) {}
        else if (context.performed) {
            jump.Invoke();
        } else if (context.canceled) {}
    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started) {
            // Debug.Log("move started");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            Debug.Log("faceRight:" + faceRight.ToString());
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled) {
            // Debug.Log("move stopped");
            moveCheck.Invoke(0);
        }
    }

    public void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("mouse click started");
        else if (context.performed) {
            Debug.Log("mouse click performed");
        } else if (context.canceled)
            Debug.Log("mouse click cancelled");
    }

    public void OnPointAction(InputAction.CallbackContext context)
    {
        if (context.performed) {
            Vector2 point = context.ReadValue<Vector2>();
            Debug.Log($"Point detected: {point}");
        }
    }


}
