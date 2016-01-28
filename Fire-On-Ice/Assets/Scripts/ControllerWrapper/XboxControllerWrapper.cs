using UnityEngine;
using System.Collections;
using System;

public class XboxControllerWrapper : ControllerInputWrapper {

    public override float GetAxis(Axis axis, int joyNum, bool isRaw = false)
    {
        string axisName = "";
        float scale = 1;
        switch (axis)
        {
            case Axis.LeftStickX:
                axisName = getAxisName(joyNum, "X", "X", "X");
                break;
            case Axis.LeftStickY:
                scale = -1;
                axisName = getAxisName(joyNum, "Y", "Y", "Y");
                break;
            case Axis.RightStickX:
                axisName = getAxisName(joyNum, "4", "4", "3");
                break;
            case Axis.RightStickY:
                scale = -1;
                axisName = getAxisName(joyNum, "5", "5", "4");
                break;
        }
        if (isRaw)
        {
            return Input.GetAxisRaw(axisName) * scale;
        }
        else
        {
            return Input.GetAxis(axisName) * scale;
        }
    }

    public override bool GetButton(Buttons button, int joyNum, bool isDown = false)
    {
        string buttonName = "";
        switch (button)
        {
            case Buttons.A:
                buttonName = getButtonName(joyNum, "0", "0", "16");
                break;
            case Buttons.B:
                buttonName = getButtonName(joyNum, "1", "1", "17");
                break;
            case Buttons.X:
                buttonName = getButtonName(joyNum, "2", "2", "18");
                break;
            case Buttons.Y:
                buttonName = getButtonName(joyNum, "3", "3", "19");
                break;
            case Buttons.RightBumper:
                buttonName = getButtonName(joyNum, "5", "5", "14");
                break;
            case Buttons.LeftBumper:
                buttonName = getButtonName(joyNum, "4", "4", "13");
                break;
            case Buttons.Start:
                buttonName = getButtonName(joyNum, "7", "7", "9");
                break;
            case Buttons.RightStickClick:
                buttonName = getButtonName(joyNum, "9", "10", "12");
                break;
            case Buttons.LeftStickClick:
                buttonName = getButtonName(joyNum, "8", "9", "11");
                break;

        }
        if (isDown)
        {
            return Input.GetButtonDown(buttonName);
        }
        else
        {
            return Input.GetButton(buttonName);
        }
    }

    

    public override bool GetButtonUp(Buttons button, int joyNum)
    {
        string buttonName = "";
        switch (button)
        {
            case Buttons.A:
                buttonName = getButtonName(joyNum, "0", "0", "16");
                break;
            case Buttons.B:
                buttonName = getButtonName(joyNum, "1", "1", "17");
                break;
            case Buttons.X:
                buttonName = getButtonName(joyNum, "2", "2", "18");
                break;
            case Buttons.Y:
                buttonName = getButtonName(joyNum, "3", "3", "19");
                break;
            case Buttons.RightBumper:
                buttonName = getButtonName(joyNum, "5", "5", "14");
                break;
            case Buttons.LeftBumper:
                buttonName = getButtonName(joyNum, "4", "4", "13");
                break;
            case Buttons.Start:
                buttonName = getButtonName(joyNum, "7", "7", "9");
                break;
            case Buttons.RightStickClick:
                buttonName = getButtonName(joyNum, "9", "10", "12");
                break;
            case Buttons.LeftStickClick:
                buttonName = getButtonName(joyNum, "8", "9", "11");
                break;
        }
        return Input.GetButtonUp(buttonName);
    }

    public override float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false)
    {
        string axisName = "";
        switch (trigger)
        {
            case Triggers.LeftTrigger:
                axisName = getAxisName(joyNum, "9", "3", "5");
                break;
            case Triggers.RightTrigger:
                axisName = getAxisName(joyNum, "10", "6", "6");
                break;
        }
        if (isRaw)
        {
            return Input.GetAxisRaw(axisName);
        }
        else
        {
            return Input.GetAxis(axisName);
        }
    }
}
