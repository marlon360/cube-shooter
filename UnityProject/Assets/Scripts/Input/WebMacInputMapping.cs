using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebMacInputMapping : MacInputMapping
{
    public override bool GetStartButton() {
        return Input.GetButtonDown("Start_WebMac");
    }

    public override bool GetBackButton() {
        return Input.GetButtonDown("Back_WebMac");
    }

    public override bool GetAButton() {
        return Input.GetButtonDown("A_WebMac");
    }
    
    public override bool GetBButton() {
        return Input.GetButtonDown("B_WebMac");
    }

}
