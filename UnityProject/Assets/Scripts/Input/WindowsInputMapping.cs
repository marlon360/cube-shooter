using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsInputMapping : IInputMapping
{
    public float GetLeftStickHorizontal() {
        return Input.GetAxisRaw("LS_X_Win");
    }

    public float GetLeftStickVertical() {
        return Input.GetAxisRaw("LS_Y_Win");
    }

    public float GetRightStickHorizontal() {
        return Input.GetAxisRaw("RS_X_Win");
    }

    public float GetRightStickVertical() {
        return Input.GetAxisRaw("RS_Y_Win");
    }

    public float GetRightTrigger() {
        return Input.GetAxisRaw("LT_Win");
    }

    public float GetLeftTrigger() {
        return Input.GetAxisRaw("RT_Win");
    }

    public bool GetStartButton() {
        return Input.GetButtonDown("Start_Win");
    }

    public bool GetBackButton() {
        return Input.GetButtonDown("Back_Win");
    }

    public bool GetAButton() {
        return Input.GetButtonDown("A_Win");
    }
    
    public bool GetBButton() {
        return Input.GetButtonDown("B_Win");
    }

}
