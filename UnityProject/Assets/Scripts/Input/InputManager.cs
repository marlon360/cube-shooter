using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType {
    Controller,
    KeyboardMouse,
    Touch
}

public static class InputManager {

    public static InputType inputType = InputType.Controller;

    public static void UseKeyboard () {
        inputType = InputType.KeyboardMouse;
    }

    public static void UseController () {
        inputType = InputType.Controller;
    }

    public static float GetHorizontal () {
        if (inputType == InputType.Controller) {
            return ControllerInputManager.GetLeftStickHorizontal ();
        } else if (inputType == InputType.KeyboardMouse) {
            return Input.GetAxis ("Horizontal");
        } else {
            return 0;
        }
    }

    public static float GetVertical () {
        if (inputType == InputType.Controller) {
            return ControllerInputManager.GetLeftStickVertical ();
        } else if (inputType == InputType.KeyboardMouse) {
            return Input.GetAxis ("Vertical");
        } else {
            return 0;
        }
    }

    public static float GetShoot () {
        if (inputType == InputType.Controller) {
            return ControllerInputManager.GetRightTrigger ();
        } else if (inputType == InputType.KeyboardMouse) {
            return Input.GetMouseButton (0) ? 1 : 0;
        } else {
            return 0;
        }
    }

    public static float GetAim () {
        if (inputType == InputType.Controller) {
            return ControllerInputManager.GetLeftTrigger ();
        } else if (inputType == InputType.KeyboardMouse) {
            return Input.GetMouseButton (1) ? 1 : 0;
        } else {
            return 0;
        }
    }

    public static bool GetStart () {
        if (inputType == InputType.Controller) {
            return ControllerInputManager.GetStartButton ();
        } else if (inputType == InputType.KeyboardMouse) {
            return Input.GetKeyDown(KeyCode.Return);
        } else {
            return false;
        }
    }

    public static bool GetBack () {
        if (inputType == InputType.Controller) {
            return ControllerInputManager.GetBackButton ();
        } else if (inputType == InputType.KeyboardMouse) {
            return Input.GetKeyDown(KeyCode.Escape);
        } else {
            return false;
        }
    }

}