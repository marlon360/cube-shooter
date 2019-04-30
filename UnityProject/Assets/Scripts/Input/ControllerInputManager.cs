using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerInputManager {

    #if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        public static IInputMapping mapping = new WindowsInputMapping();
    #elif (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
        public static IInputMapping mapping = new MacInputMapping();
    #else
        public static IInputMapping mapping = new WindowsInputMapping();
    #endif
    

    public static void UseWindows () {
        mapping = new WindowsInputMapping ();
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