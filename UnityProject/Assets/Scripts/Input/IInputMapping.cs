﻿using System.Collections;
using System.Collections.Generic;

public interface IInputMapping {

    float GetLeftStickHorizontal ();
    float GetLeftStickVertical ();
    float GetRightStickHorizontal ();
    float GetRightStickVertical ();
    float GetRightTrigger ();
    float GetLeftTrigger ();

    bool GetStartButton ();
    bool GetBackButton ();
    bool GetAButton ();
    bool GetBButton ();
}