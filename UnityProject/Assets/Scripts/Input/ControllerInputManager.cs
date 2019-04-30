using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerInputManager {

    public static IInputMapping mapping = new WindowsInputMapping ();

    public static void UseWindows () {
        mapping = new WindowsInputMapping ();
    }

    public static void UseMac () {
        mapping = new MacInputMapping ();
    }

    public static float GetLeftStickHorizontal () {
        return mapping.GetLeftStickHorizontal ();
    }

    public static float GetLeftStickVertical () {
        return mapping.GetLeftStickVertical ();
    }

    public static float GetRightStickHorizontal () {
        return mapping.GetRightStickHorizontal ();
    }

    public static float GetRightStickVertical () {
        return mapping.GetRightStickVertical ();
    }

    public static float GetRightTrigger () {
        return mapping.GetLeftTrigger ();
    }

    public static float GetLeftTrigger () {
        return mapping.GetRightTrigger ();
    }

    public static bool GetStartButton () {
        return mapping.GetStartButton ();
    }

    public static bool GetBackButton () {
        return mapping.GetBackButton ();
    }

    public static bool GetAButton () {
        return mapping.GetAButton ();
    }

    public static bool GetBButton () {
        return mapping.GetBButton ();
    }

}