using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJump()
    {
        Debug.Log("OnJump called");
        // TODO
    }

    public void OnMove(InputValue input)
    {
        if (input.Get() == null) {
            Debug.Log("Move released");
        } else {
            Debug.Log($"Move triggered, with value {input.Get()}"); // will return null when released
        }
        // TODO
    }

    public void OnJumpHold(InputValue value)
    {
        Debug.Log($"OnJumpHold performed with value {value.Get()}");
        // TODO

    }

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("JumpHold was started");
        else if (context.performed)
        {
            Debug.Log("JumpHold was performed");
        }
        else if (context.canceled)
            Debug.Log("JumpHold was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Jump was started");
        else if (context.performed)
        {
            Debug.Log("Jump was performed");
        }
        else if (context.canceled)
            Debug.Log("Jump was cancelled");

    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("move started");
            float move = context.ReadValue<float>();
            Debug.Log($"move value: {move}"); // will return null when not pressed
        }
        if (context.canceled)
        {
            Debug.Log("move stopped");
        }
    }

}
