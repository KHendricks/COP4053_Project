using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputAction
{
    Interact,
    Attack,
    Jump,
    FollowToggle,
    Dismiss
}

public static class InputManager {

    public static bool isGamepad;

    public static bool JustPressed(InputAction action)
    {
        switch(action)
        {
            // Pressing 'X' key or 'X' button.
            case InputAction.Interact:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    isGamepad = false;
                    return true;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton18)) 
                {
                    isGamepad = true;
                    return true;
                }
                return false;
            // Pressing left shift key or 'B' button.
            case InputAction.Attack:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isGamepad = false;
                    return true;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton17))
                {
                    isGamepad = true;
                    return true;
                }
                return false;
            // Pressing space bar or 'A' button
            case InputAction.Jump:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isGamepad = false;
                    return true;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton16))
                {
                    isGamepad = true;
                    return true;
                }
                return false;
                // Pressing 'Z' key or 'Y' button.
            case InputAction.FollowToggle:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    isGamepad = false;
                    return true;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton19))
                {
                    isGamepad = true;
                    return true;
                }
                return false;
            // Pressing 'C' key or 'Back' button.
            case InputAction.Dismiss:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    isGamepad = false;
                    return true;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.JoystickButton10))
                {
                    isGamepad = true;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    public static bool JustReleased(InputAction action)
    {
        switch (action)
        {
            // Releasing 'X' key or 'X' button.
            case InputAction.Interact:
                if (Input.GetKeyUp(KeyCode.X))
                {
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton18))
                {
                    return true;
                }
                return false;
            // Releasing left shift key or 'B' button.
            case InputAction.Attack:
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton17))
                {
                    return true;
                }
                return false;
            // Releasing space bar or 'A' button
            case InputAction.Jump:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton16))
                {
                    return true;
                }
                return false;
            // Releasing 'Z' key or 'Y' button.
            case InputAction.FollowToggle:
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton19))
                {
                    return true;
                }
                return false;
            // Releasing 'C' key or 'Back' button.
            case InputAction.Dismiss:
                if (Input.GetKeyUp(KeyCode.C))
                {
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.JoystickButton10))
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }
}
