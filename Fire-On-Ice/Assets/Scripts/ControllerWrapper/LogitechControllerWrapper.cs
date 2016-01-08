using UnityEngine;
using System.Collections;
using System;

public class LogitechControllerWrapper : ControllerInputWrapper {

    public override float GetAxis(Axis axis, int joyNum, bool isRaw = false)
    {
        string axisName = "";

        switch (axis)
        {
            case Axis.LeftStickX:
                axisName = getAxisName(joyNum, "X", "X", "X");
                break;
            case Axis.LeftStickY:
                axisName = getAxisName(joyNum, "Y", "Y", "Y");
                break;
            case Axis.RightStickX:
                axisName = getAxisName(joyNum, "3", "3", "3");
                break;
            case Axis.RightStickY:
                axisName = getAxisName(joyNum, "4", "4", "4");
                break;
        }

        if (isRaw)
        {
            return Input.GetAxisRaw(axisName);
        }
        return Input.GetAxis(axisName);
    }

    public override float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false)
    {
        string triggerName = "";
        switch (trigger)
        {
            case Triggers.LeftTrigger:
                triggerName = getButtonName(joyNum, "6", "6", "6");
                break;
            case Triggers.RightTrigger:
                triggerName = getButtonName(joyNum, "7", "7", "7");
                break;
        }

        if (Input.GetButton(triggerName))
        {
            return 1;
        }
        return 0;
    }

    public override bool GetButton(Buttons button, int joyNum, bool isDown = false)
    {
        string buttonName = getButtonHelper(button, joyNum);
        if (isDown)
        {
            return Input.GetButtonDown(buttonName);
        }
        return Input.GetButton(buttonName);
    }

    public override bool GetButtonUp(Buttons button, int joyNum)
    {
        string buttonName = getButtonHelper(button, joyNum);
        return Input.GetButtonUp(buttonName);
    }

    string getButtonHelper(Buttons button, int joyNum)
    {
        switch (button)
        {
            case Buttons.A:
                return getButtonName(joyNum, "1", "1", "1");
            case Buttons.B:
                return getButtonName(joyNum, "2", "2", "2");
            case Buttons.X:
                return getButtonName(joyNum, "0", "0", "0");
            case Buttons.Y:
                return getButtonName(joyNum, "3", "3", "3");
            case Buttons.RightBumper:
                return getButtonName(joyNum, "5", "5", "5");
            case Buttons.LeftBumper:
                return getButtonName(joyNum, "4", "4", "4");
            case Buttons.Start:
                return getButtonName(joyNum, "9", "9", "9");
        }
        return "";
    }
}
