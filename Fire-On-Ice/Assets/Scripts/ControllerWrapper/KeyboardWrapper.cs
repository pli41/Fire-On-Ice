using UnityEngine;
using System.Collections;
using System;

public class KeyboardWrapper : ControllerInputWrapper {

    public override float GetAxis(Axis axis, int joyNum, bool isRaw = false)
    {
        throw new NotImplementedException();
    }

    public override float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false)
    {
        throw new NotImplementedException();
    }

    public override bool GetButton(Buttons button, int joyNum, bool isDown = false)
    {
        throw new NotImplementedException();
    }

    public override bool GetButtonUp(Buttons button, int joyNum)
    {
        throw new NotImplementedException();
    }
}
