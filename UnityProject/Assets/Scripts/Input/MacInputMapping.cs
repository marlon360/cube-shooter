using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacInputMapping : IInputMapping
{
    public float GetLeftStickHorizontal() {
        return Input.GetAxisRaw("LS_X_Mac");
    }

    public float GetLeftStickVertical() {
        return Input.GetAxisRaw("LS_Y_Mac");
    }

    public float GetRightStickHorizontal() {
        return Input.GetAxisRaw("RS_X_Mac");
    }

    public float GetRightStickVertical() {
        return Input.GetAxisRaw("RS_Y_Mac");
    }

    public float GetRightTrigger() {
        return Input.GetAxisRaw("LT_Mac");
    }

    public float GetLeftTrigger() {
        return Input.GetAxisRaw("RT_Mac");
    }

    public bool GetStartButton() {
        return Input.GetButtonDown("Start_Mac");
    }

    public bool GetBackButton() {
        return Input.GetButtonDown("Back_Mac");
    }

    public bool GetAButton() {
        return Input.GetButtonDown("A_Mac");
    }
    
    public bool GetBButton() {
        return Input.GetButtonDown("B_Mac");
    }

}
